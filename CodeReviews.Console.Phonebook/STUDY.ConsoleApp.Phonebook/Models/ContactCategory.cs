﻿namespace STUDY.ConsoleApp.Phonebook.Models;

public class ContactCategory
{
    public int ContactId { get; set; }
    
    public Contact Contact { get; set; }
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
}