namespace ConsoleApp.Interfaces;
public interface IContact
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    string Street { get; set; }
    string ZipCode { get; set; }
    string City { get; set; }
}