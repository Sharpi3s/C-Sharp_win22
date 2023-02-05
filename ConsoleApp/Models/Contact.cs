using ConsoleApp.Interfaces;

namespace ConsoleApp.Models;
public class Contact : IContact
{
    public Contact()
    {
        Id = Guid.NewGuid();
        FirstName = null!;
        LastName = null!;
        Email = null!;
        PhoneNumber = null!;
        Street = null!;
        ZipCode = null!;
        City = null!;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
}