using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Controllers;

public static class CategoryController
{
    public static bool AddCategory(Category category)
    {
        using var context = new PhonebookContext();
        
        if (context.Categories.Any(x => x.Name == category.Name))
            return false;
        
        context.Add(category);
        context.SaveChanges();
        return true;
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