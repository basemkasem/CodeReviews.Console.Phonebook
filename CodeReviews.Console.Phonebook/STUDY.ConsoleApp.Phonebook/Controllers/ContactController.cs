using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Controllers;

public static class ContactController
{
    public static void AddContact(Contact contact, List<Category>? categories = null)
    {
        using var context = new PhonebookContext();
        
        context.Contacts.Add(contact);
        context.SaveChanges();

        if (categories.IsNullOrEmpty()) return;
        
        foreach (var category in categories!)
        {
            var contactCategory = new ContactCategory
            {
                ContactId = contact.ContactId,
                CategoryId = category.CategoryId
            };
            context.Add(contactCategory);
        }
        context.SaveChanges();
    }
    
    public static Contact GetContactById(int id)
    {
        using var context = new PhonebookContext();
        var contact = context.Contacts
            .Include(c => c.ContactCategories)
            .Single(x => x.ContactId == id);
        
        var allCategories = context.Categories.ToList();
        
        contact.Categories = contact.ContactCategories
            .Join(allCategories,
                cc => cc.CategoryId,
                cat => cat.CategoryId,
                (cc, cat) => cat.Name)
            .ToList();
        return contact;
    }

    public static List<Contact> ListAllContacts()
    {
        using var context = new PhonebookContext();
        var contacts = context.Contacts
            .Include(c => c.ContactCategories)
            .ToList();
        
        var allCategories = context.Categories.ToList();
        foreach (var contact in contacts)
        {
            contact.Categories = contact.ContactCategories
                .Join(allCategories,
                    cc => cc.CategoryId,
                    cat => cat.CategoryId,
                    (cc, cat) => cat.Name)
                .ToList();
        }
        
        return contacts;
    }

    public static List<Contact>? GetContactsByCategory(Category category)
    {
        using var context =  new PhonebookContext();
        var contacts = context.Contacts
            .Include(c => c.ContactCategories)
            .Where(c => c.ContactCategories.Any(cc => cc.CategoryId == category.CategoryId))
            .ToList();

        return contacts;
    }

    public static void UpdateContact(Contact contact, List<Category>? categories = null)
    {
        using var context = new PhonebookContext();
        
        context.ContactCategories
            .Where(cc => cc.ContactId == contact.ContactId)
            .ExecuteDelete();
        
        if (!categories.IsNullOrEmpty())
        {
            foreach (var category in categories!)
            {
                var contactCategory = new ContactCategory
                {
                    ContactId = contact.ContactId,
                    CategoryId = category.CategoryId
                };
                context.Add(contactCategory);
            }
        }
        context.Update(contact);
        context.SaveChanges();
    }
    public static void DeleteContact(Contact contact)
    {
        using var context = new PhonebookContext();
        context.Remove(contact);
        
        context.SaveChanges();
    }
}