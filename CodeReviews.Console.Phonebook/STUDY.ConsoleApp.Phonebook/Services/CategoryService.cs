using Spectre.Console;
using STUDY.ConsoleApp.Phonebook.Controllers;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Services;

public class CategoryService
{
    public static void AddCategory()
    {
        var category = new Category
        {
            Name = AnsiConsole.Ask<string>("Enter the category name: ")
        };
        AnsiConsole.MarkupLine("[green]Category added successfully![/]");
        CategoryController.AddCategory(category);
    }

    public static void ListAllCategories()
    {
        var categories = CategoryController.ListAllCategories();
        UserInterface.ShowCategoriesTable(categories);
    }
    
    public static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.ListAllCategories();
        var categoriesNames = categories.Select(x => x.Name);

        var selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a category:")
            .AddChoices(categoriesNames)
        );
        var category = categories.Single(x => x.Name == selectedOption);
        return category;
    }

    public static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        if (AnsiConsole.Confirm("Do you want to update the category's name?"))
        {
            category.Name = AnsiConsole.Ask<string>("Enter the new category's name: ");
            CategoryController.UpdateCategory(category);
            AnsiConsole.MarkupLine("[green]Category updated successfully![/]");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        CategoryController.DeleteCategory(category);
        AnsiConsole.MarkupLine("[green]Category deleted successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}