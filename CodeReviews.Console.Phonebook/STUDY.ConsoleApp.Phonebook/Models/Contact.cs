﻿namespace STUDY.ConsoleApp.Phonebook.Models;

public class Contact
{
    public int ContactId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }
    
    public ICollection<ContactCategory> ContactCategories { get; set; }
}