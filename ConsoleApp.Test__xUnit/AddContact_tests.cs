using ConsoleApp.Models;

namespace ConsoleApp.Test__xUnit;
public class AddContact_tests
{
    private AddressBook addressBook;
    Contact contact;
    public AddContact_tests()
    {
        addressBook = new AddressBook();
        contact = new Contact();
    }
    [Fact]
    public void Should_Add_Contact_To_List()
    {
        addressBook.ContactList.Add(contact);
        addressBook.ContactList.Add(contact);

        Assert.Equal(2, addressBook.ContactList.Count);
    }

    [Fact]
    public void Should_Remove_Contact_From_List()
    {
        addressBook.ContactList.Add(contact);
        addressBook.ContactList.Add(contact);

        addressBook.ContactList.Remove(contact);

        Assert.Single(addressBook.ContactList);
    }
}