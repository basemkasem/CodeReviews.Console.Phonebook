using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Controllers;

public class CategoryController
{
    public static void AddCategory(Category category)
    {
        using var context = new PhonebookContext();
        context.Add(category);
        context.SaveChanges();
    }
    
    public static List<Category> ListAllCategories()
    {
        using var context = new PhonebookContext();
        var categories = context.Categories.ToList();
        return categories;
    }
    
    public static void UpdateCategory(Category category)
    {
        using var context = new PhonebookContext();
        context.Update(category);
        context.SaveChanges();
    }
    
    public static void DeleteCategory(Category category)
    {
        using var context = new PhonebookContext();
        context.Remove(category);
        context.SaveChanges();
    }
}