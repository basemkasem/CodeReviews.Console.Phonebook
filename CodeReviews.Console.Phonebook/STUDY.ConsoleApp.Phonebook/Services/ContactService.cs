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

                                       [Yellow]Notice: Number must be between 10 and 15 digits.[/] 
                                       These formats are supported: 
                                       - 1234567890 (10 digits) 
                                       - +1234567890 (with country code) 
                                       - 123456789012345 (15 digits) 

                                       [cyan]Enter the new contact's phone number:[/] 
                                       """)
                    .Validate(x => x == Regex.Match(x, @"^\+?\d{10,15}$").Value, "[red]Invalid phone number.[/]")
            )
        };
        ContactController.AddContact(contact);
        AnsiConsole.MarkupLine("[green]Contact added successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void ListAllContacts()
    {
        var contacts = ContactController.ListAllContacts();
        UserInterface.ShowContactsTable(contacts);
    }

    public static Contact GetContactOptionInput()
    {
        var contacts = ContactController.ListAllContacts();
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
        UserInterface.ShowContactPanel(contact);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void UpdateContact()
    {
        var contact = GetContactOptionInput();
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

                                       [Yellow]Notice: Number must be between 10 and 15 digits.[/] 
                                       These formats are supported: 
                                       - 1234567890 (10 digits) 
                                       - +1234567890 (with country code) 
                                       - 123456789012345 (15 digits) 

                                       [cyan]Enter the new contact's phone number:[/] 
                                       """)
                    .Validate(x => x == Regex.Match(x, @"^\+?\d{10,15}$").Value, "[red]Invalid phone number.[/]"))
            : contact.PhoneNumber;
        ContactController.UpdateContact(contact);
        AnsiConsole.MarkupLine("[green]Contact updated successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        ContactController.DeleteContact(contact);
        AnsiConsole.MarkupLine("[green]Contact deleted successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}