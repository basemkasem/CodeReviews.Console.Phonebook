using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook.Controllers;

public static class ContactController
{
    public static void AddContact(Contact contact)
    {
        using var context = new PhonebookContext();
        context.Add(contact);
        context.SaveChanges();
    }
    
    public static Contact GetContactById(int id)
    {
        using var context = new PhonebookContext();
        var contact = context.Contacts.Single(x => x.Id == id);
        return contact;
    }

    public static List<Contact> ListAllContacts()
    {
        using var context = new PhonebookContext();
        var contacts = context.Contacts.ToList();
        return contacts;
    }

    public static void UpdateContact(Contact contact)
    {
        using var context = new PhonebookContext();
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