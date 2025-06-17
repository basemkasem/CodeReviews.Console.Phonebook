namespace STUDY.ConsoleApp.Phonebook.Models;

public class Category
{
    public int CategoryId { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<ContactCategory> ContactCategories { get; set; }
}