using System.Text.RegularExpressions;
using Spectre.Console;
using STUDY.ConsoleApp.Phonebook.Controllers;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Services;

public static class ContactService
{
    public static void AddContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the contact's name: ")
                    .Validate(x => x.Length < 50, "[red]Name cannot be longer than 50 characters.[/]")
            ),
            Email = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the contact's email:")
                    .Validate(x => x == Regex.Match(x, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Value,
                        "Invalid email address.")
            ),
            PhoneNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("""

                                       Please enter your phone number in one of the valid formats below:
                                       [yellow]
                                       > 01012345678
                                       > 011-12345678
                                       > +201012345678
                                       > +20 100 123 4567
                                       > +44 20 7946 0958
                                       > +1 123-456-7890
                                       > +971 50 123 4567
                                       > +912345678900[/]

                                       [cyan]Enter your phone number:[/]
                                       """)
                    .Validate(x => x == Regex.Match(x, @"^(?:(?:\+|00)[1-9]\d{0,2}[-\s]?)?(?:\(?\d{2,4}\)?[-\s]?)?\d{6,10}$").Value, "[red]Invalid phone number! Please try again.[/]")
            )
        };
        
        var categories = AnsiConsole.Confirm("Do you want to add your contact to one or more categories?") 
            ? CategoryService.GetMultipleCategoriesInput() 
            : null;

        ContactController.AddContact(contact, categories);

        AnsiConsole.MarkupLine("[green]Contact added successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void ListAllContacts()
    {
        var contacts = ContactController.ListAllContacts();
        UserInterface.ShowContactsTable(contacts);
    }

    private static Contact? GetContactOptionInput()
    {
        var contacts = ContactController.ListAllContacts();
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts found.[/]");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return null;
        }
        var contactsNames = contacts.Select(x => x.Name);

        var selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a contact:")
            .AddChoices(contactsNames)
        );
        var contact = contacts.Single(x => x.Name == selectedOption);
        return contact;
    }

    public static void GetContact()
    {
        var contact = GetContactOptionInput();
        if (contact == null) return;
        
        UserInterface.ShowContactPanel(contact);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void UpdateContact()
    {
        var contact = GetContactOptionInput();
        if (contact == null) return;
        
        UserInterface.ShowContactPanel(contact);
        contact.Name = AnsiConsole.Confirm("Do you want to update the contact's name?")
            ? AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the new contact's name: ")
                    .Validate(x => x.Length < 50, "Name cannot be longer than 50 characters."))
            : contact.Name;
        contact.Email = AnsiConsole.Confirm("Do you want to update the contact's email?")
            ? AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the new contact's email:")
                    .Validate(x => x == Regex.Match(x, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Value,
                        "[red]Invalid email address.[/]"))
            : contact.Email;
        contact.PhoneNumber = AnsiConsole.Confirm("Do you want to update the contact's phone number?")
            ? AnsiConsole.Prompt(
                new TextPrompt<string>("""
                                       Please enter your phone number in one of the valid formats below:
                                       [yellow]
                                       > 01012345678
                                       > 011-12345678
                                       > +201012345678
                                       > +20 100 123 4567
                                       > +44 20 7946 0958
                                       > +1 123-456-7890
                                       > +971 50 123 4567
                                       > +912345678900[/]

                                       [cyan]Enter the new contact's phone number:[/] 
                                       """)
                    .Validate(x => x == Regex.Match(x, @"^(?:(?:\+|00)[1-9]\d{0,2}[-\s]?)?(?:\(?\d{2,4}\)?[-\s]?)?\d{6,10}$").Value, "[red]Invalid phone number! Please try again.[/]"))
            : contact.PhoneNumber;
        
        var categories = AnsiConsole.Confirm("Do you want to update contact categories?") 
            ? CategoryService.GetMultipleCategoriesInput() 
            : null;
        
        ContactController.UpdateContact(contact, categories);
        AnsiConsole.MarkupLine("[green]Contact updated successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        if (contact == null) return;
        
        ContactController.DeleteContact(contact);
        AnsiConsole.MarkupLine("[green]Contact deleted successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}