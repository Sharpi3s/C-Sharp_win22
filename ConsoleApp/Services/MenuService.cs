using ConsoleApp.Models;
using Newtonsoft.Json;

namespace ConsoleApp.Services;
public class MenuService
{
    private static FileService file = new FileService();

    private AddressBook contacts = new AddressBook();

    public string FilePath { get; set; } = null!;

    public void WelcomeMenu()
    {

        try
        {
            var json = JsonConvert.DeserializeObject<Dictionary<string, List<Contact>>>(file.Read(FilePath))!;
            if (json != null)
            {
                contacts.ContactList = json["ContactList"];
            }
            
        }
        catch
        {

        }

        string prompt = @"


 █████  ██████  ██████  ██████  ███████ ███████ ███████     ██████   ██████   ██████  ██   ██ 
██   ██ ██   ██ ██   ██ ██   ██ ██      ██      ██          ██   ██ ██    ██ ██    ██ ██  ██  
███████ ██   ██ ██   ██ ██████  █████   ███████ ███████     ██████  ██    ██ ██    ██ █████   
██   ██ ██   ██ ██   ██ ██   ██ ██           ██      ██     ██   ██ ██    ██ ██    ██ ██  ██  
██   ██ ██████  ██████  ██   ██ ███████ ███████ ███████     ██████   ██████   ██████  ██   ██ 
                                                                                                                                                                                                                                                                                                                                                                                                                                 
Welcome to your Address Book. What would you like to do?
(Use the arrow keys to choose your option and press enter to select an option)

";

        string[] options = { "1. Create new contact", "2. Delete contact", "3. Search contact", "4. Show all contacts", "Exit" };

        ArrowMenuService arrowMenu = new ArrowMenuService(prompt, options);

        int selectedIndex = arrowMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                OptionOne();
                break;
            case 1:
                OptionTwo();
                break;
            case 2:
                OptionThree();
                break;
            case 3:
                OptionFour();
                break;
            case 4:
                Exit();
                break;
        }
    }

    private void OptionOne()
    {
        Console.Clear();
        Console.WriteLine("Create new contact");

        Contact contact = new Contact();

        Console.Write("Enter first name: ");
        contact.FirstName = Console.ReadLine() ?? "";
        Console.Write("Enter last name: ");
        contact.LastName = Console.ReadLine() ?? "";
        Console.Write("Enter email: ");
        contact.Email = Console.ReadLine() ?? "";
        Console.Write("Enter phone number: ");
        contact.PhoneNumber = Console.ReadLine() ?? "";
        Console.Write("Enter address street: ");
        contact.Street = Console.ReadLine() ?? "";
        Console.Write("Enter zip code: ");
        contact.ZipCode = Console.ReadLine() ?? "";
        Console.Write("Enter city: ");
        contact.City = Console.ReadLine() ?? "";

        contacts?.ContactList?.Add(contact);

        file.Save(FilePath, JsonConvert.SerializeObject(new { contacts?.ContactList }));

        Console.WriteLine("");
        Console.WriteLine("Press any key to return to the home page.");
        Console.ReadKey();
    }
    private void OptionTwo()
    {
        Console.Clear();
        Console.WriteLine("Delete a contact");
        Console.WriteLine();

        if (contacts?.ContactList?.Count > 0)
        {
            Console.Write("Enter the name of the contact you want to delete: ");

            var name = Console.ReadLine();

            var response = contacts?.ContactList?.Find(contact => contact.FirstName == name);

            while (response == null)
            {
                Console.Clear();
                Console.Write("There is no contact with that name in you address book. \nPlease try again: ");
                name = Console.ReadLine();
                response = contacts?.ContactList?.Find(x => x.FirstName == name);
            }

            Console.Clear();
            string prompt = "Are you sure you want to delete " + response.FirstName + " from your address book?";
            string[] options = { "Yes", "No" };
            ArrowMenuService contactsMenu = new ArrowMenuService(prompt, options);
            int selectedIndex = contactsMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    contacts?.ContactList?.RemoveAll(contact => contact.FirstName! == name);
                    file.Save(FilePath, JsonConvert.SerializeObject(new { contacts }));
                    Console.WriteLine(response.FirstName + " has been deleted.");
                    Console.WriteLine("Press any key to return to the home page.");
                    Console.ReadKey();
                    break;
                case 1:
                    WelcomeMenu();
                    break;
            }
            /*
             Version på hur man kan hantera det med att användaren får svara med y eller n istället. 
             Men valde att forstätta med pilmenyn eftersom huvudmenyn använder sig av det och det såg estiskt bättre ut.
             */

            //Console.Clear();
            //Console.WriteLine("Are you sure you want to delete " + response.FirstName + " from your address book?");
            //Console.Write("Enter y for yes if you're sure and n for no: ");
            //var answer = Console.ReadLine();
            //while (answer != "y" && answer != "n")
            //{
            //    Console.Clear();
            //    Console.WriteLine("You can only enter y or n. Please try again.");
            //    Console.Write("Enter y for yes if you're sure and n for no: ");
            //    answer = Console.ReadLine();
            //}
            //if (answer == "y")
            //{
            //    Console.Clear();
            //    contacts.RemoveAll(contact => contact.FirstName! == response.FirstName);
            //    FileService.Save(FilePath, JsonConvert.SerializeObject(new { contacts }));
            //    Console.WriteLine(response.FirstName + " has been deleted.");
            //    Console.WriteLine("Press any key to return to the home page.");
            //    Console.ReadKey();
            //}
            //else if (answer == "n")
            //{
            //    WelcomeMenu();
            //}

        }
        else
        {
            Console.WriteLine("No contacts can be deleted because your contact list is empty.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the home page.");
            Console.ReadKey();
        }
    }

    private void OptionThree()
    {
        Console.Clear();
        Console.WriteLine("Search for a contact");
        Console.WriteLine();
        Console.Write("Enter the name of the contact you're looking for: ");

        var name = Console.ReadLine();

        if (name != null)
        {
            var response = contacts?.ContactList?.Find(contact => contact.FirstName == name);

            while (response == null)
            {
                Console.Clear();   
                Console.Write("There is no contact with that name in you address book. \nPlease try again: ");
                name = Console.ReadLine();
                response = contacts?.ContactList?.Find(x => x.FirstName == name);
            }

            Console.Clear();
            Console.WriteLine("First name: " + response.FirstName! +
                  "\nLast name: " + response.LastName +
                  "\nEmail: " + response.Email +
                  "\nPhone number: " + response.PhoneNumber +
                  "\nAddress: " + response.Street + ", " + response.ZipCode + " " + response.City 
                );
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the home page.");
            Console.ReadKey();
        } 
    }

    private void OptionFour()
    {
        Console.Clear();
        Console.WriteLine("Show all contacts");
        Console.WriteLine();

        //contacts?.ContactList.ForEach(contact => Console.WriteLine("Name: " + contact.FirstName + " " + contact.LastName + "  " + "Email: " + contact.Email + "\n"));


        if (contacts != null)
        {
            contacts!.ContactList.ForEach(contact => Console.WriteLine("Name: " + contact.FirstName + " " + contact.LastName + "  " + "Email: " + contact.Email + "\n"));
        }
        else
        {
            Console.WriteLine("Your contact list is empty.");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to return to the home page.");
        Console.ReadKey();
    }

    private static void Exit() 
    {
        Console.WriteLine("\nPress any key to exit..");
        Console.ReadKey(true);
        Environment.Exit(0);
    }
}