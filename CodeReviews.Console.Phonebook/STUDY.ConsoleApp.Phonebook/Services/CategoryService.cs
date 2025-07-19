using Spectre.Console;
using STUDY.ConsoleApp.Phonebook.Controllers;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Services;

public static class CategoryService
{
    public static void AddCategory()
    {
        var category = new Category
        {
            Name = AnsiConsole.Ask<string>("Enter the category name: ")
        };

        AnsiConsole.MarkupLine(CategoryController.AddCategory(category)
            ? "[green]Category added successfully![/]"
            : "[red]Category already exists![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void ListAllCategories()
    {
        var categories = CategoryController.ListAllCategories();
        UserInterface.ShowCategoriesTable(categories);
    }

    private static Category? GetCategoryOptionInput()
    {
        var categories = CategoryController.ListAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found.[/]");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return null;
        }

        var categoriesNames = categories.Select(x => x.Name);

        var selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a category:")
            .AddChoices(categoriesNames)
        );
        var category = categories.Single(x => x.Name == selectedOption);
        return category;
    }

    public static List<Category>? GetMultipleCategoriesInput()
    {
        var categories = CategoryController.ListAllCategories();
        if (categories.Count == 0)
            return null;

        var categoriesNames = categories.Select(x => x.Name);
        var selectedOptions = AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
            .Title("Select one or more categories:")
            .AddChoices(categoriesNames)
        );
        var categoriesList = categories.Where(x => selectedOptions.Contains(x.Name)).ToList();
        return categoriesList;
    }

    public static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        if (category == null) return;

        category.Name = AnsiConsole.Ask<string>("Enter the new category's name: ");
        CategoryController.UpdateCategory(category);
        AnsiConsole.MarkupLine("[green]Category updated successfully![/]");

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        if (category == null) return;

        if (!AnsiConsole.Confirm("Are you sure you want to delete this category?")) return;
        CategoryController.DeleteCategory(category);
        AnsiConsole.MarkupLine("[green]Category deleted successfully![/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}