using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using STUDY.ConsoleApp.Phonebook.Models;
using STUDY.ConsoleApp.Phonebook.Services;

namespace STUDY.ConsoleApp.Phonebook;

public static class UserInterface
{
    public static void MainMenu()
    {
        AnsiConsole.MarkupLine("[bold green]Welcome to the Phonebook[/]");

        var options = Enum.GetValues<MainMenuOptions>();
        
        bool isRunning = true;
        while (isRunning)
        {
            var option = AnsiConsole.Prompt(new SelectionPrompt<MainMenuOptions>()
                .AddChoices(options)
                .UseConverter(x => x.ToString().SeparateString())
                .Title("What do you want to do?"));
            
            Console.Clear();

            switch (option)
            {
                case MainMenuOptions.ManageContacts:
                    ContactsMenu();
                    break;
                case MainMenuOptions.ManageCategories:
                    CategoriesMenu();
                    break;
                case MainMenuOptions.Exit:
                    isRunning = false;
                    break;
            }
        }
        Console.WriteLine("Good bye!");
    }

    private static void CategoriesMenu()
    {
        var options = Enum.GetValues<CategoryMenuOptions>();
        
        bool isRunning = true;
        while (isRunning)
        {
            var option = AnsiConsole.Prompt(new SelectionPrompt<CategoryMenuOptions>()
                .AddChoices(options)
                .UseConverter(option => option.ToString().SeparateString())
                .Title("Choose an option:"));

            switch (option)
            {
                case CategoryMenuOptions.AddACategory:
                    CategoryService.AddCategory();
                    break;
                case CategoryMenuOptions.GetContactsUnderACategory:
                    CategoryService.GetContactsByCategory();
                    break;
                case CategoryMenuOptions.ListAllCategories:
                    CategoryService.ListAllCategories();
                    break;
                case CategoryMenuOptions.UpdateACategory:
                    CategoryService.UpdateCategory();
                    break;
                case CategoryMenuOptions.DeleteACategory:
                    CategoryService.DeleteCategory();
                    break;
                case CategoryMenuOptions.GoBack:
                    isRunning = false;
                    break;
            }
            Console.Clear();
        }
    }

    private static void ContactsMenu()
    {
        var options = Enum.GetValues<ContactMenuOptions>();
        
        bool isRunning = true;
        while (isRunning)
        {
            var option = AnsiConsole.Prompt(new SelectionPrompt<ContactMenuOptions>()
                .AddChoices(options)
                .UseConverter(option => option.ToString().SeparateString())
                .Title("Choose an option:"));

            switch (option)
            {
                case ContactMenuOptions.AddAContact:
                    ContactService.AddContact();
                    break;
                case ContactMenuOptions.ListAllContacts:
                    ContactService.ListAllContacts();
                    break;
                case ContactMenuOptions.SearchForAContact:
                    ContactService.GetContact();
                    break;
                case ContactMenuOptions.UpdateAContact:
                    ContactService.UpdateContact();
                    break;
                case ContactMenuOptions.DeleteAContact:
                    ContactService.DeleteContact();
                    break;
                case ContactMenuOptions.GoBack:
                    isRunning = false;
                    break;
            }
            Console.Clear();
        }
    }
    
    public static void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");
        table.AddColumn("Categories");
        
        foreach (var contact in contacts)
        {
            var categories = string.Join(", ", contact.Categories!) ?? "";

            table.AddRow(contact.ContactId.ToString(), contact.Name, contact.Email, contact.PhoneNumber, categories); 
            
        }
        
        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    
    public static void ShowContactsUnderCategoryTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");
        
        foreach (var contact in contacts)
        {
            table.AddRow(contact.ContactId.ToString(), contact.Name, contact.Email, contact.PhoneNumber); 
        }
        
        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    
    public static void ShowCategoriesTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        
        foreach (var category in categories)
            table.AddRow(category.CategoryId.ToString(), category.Name);
        
        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void ShowContactPanel(Contact contact)
    {
        var categories = contact.Categories.IsNullOrEmpty() ? "[yellow]No categories[/]" : string.Join(", ", contact.Categories!) ;
        
        var panel = new Panel($"Id: {contact.ContactId}\nName: {contact.Name}\nEmail: {contact.Email}\nPhone number: {contact.PhoneNumber} \nCategories: {categories}")
        {
            Header = new PanelHeader("Contact Details"),
            Padding = new Padding(3, 1, 3, 1),
        };
        AnsiConsole.Write(panel);
    }
}