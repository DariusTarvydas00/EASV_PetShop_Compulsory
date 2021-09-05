using System;
using EASV_PetShop.Core.ApplicationService;
using EASV_PetShop.Core.Entity;

namespace EASV_PetShop.UI
{
    public class Printer: IPrinter
    {

        private readonly ICustomerService _customerService;
        private string[] _currentMenu;
        //private List<Pet> _pets = new List<Pet>();
        //private List<PetType> _petTypes = new List<PetType>();
        
        #region stringMenuItems
        
        private readonly string[] _mainMenu =
        {
            "Customer Menu:",
            "Pet Menu:",
            "Exit"
        };
        
        private readonly string[] _customerMenuItems =
        {
            "Show list of all Customers:",
            "Search Customer by Id:",
            "Create new Customer:",
            "Remove Customer:",
            "Update Customer information:",
            "Sort Customers by Name:",
            "Go Back"
        };

        private readonly string[] _petMenuItems =
        {
            "Show list of all Pets:",
            "Search Pets by Type:",
            "Add a new Pet:",
            "Remove Pet:",
            "Update Pet information:",
            "Sort Pets by Price:",
            "Get 5 cheapest available Pets:",
            "Go Back"
        };

        #endregion
        
        public Printer(ICustomerService customerService)
        {
            _customerService = customerService;
            _currentMenu = _mainMenu;
            InitData();
        }
        
        #region MenuUINavigation
        public void StartUi()
        {
            var selection = ShowMenu(_currentMenu);

            while (selection != 3 && _currentMenu == _mainMenu)
            {
                Console.Clear();
                switch (selection)
                {
                    case 1 :
                        _currentMenu = _customerMenuItems;
                        if (_currentMenu == _customerMenuItems)
                        {
                            selection = ShowMenu(_currentMenu);
                            while (selection != 7)
                            {
                                CustomerMenu(selection);
                                break;
                            }
                        }
                        break;
                    case 2 :
                        _currentMenu = _petMenuItems;
                        if (_currentMenu == _petMenuItems)
                        {
                            selection = ShowMenu(_currentMenu);
                            while (selection != 8)
                            {
                                PetMenu(selection);
                                break;
                            }
                        }
                        break;
                }

                _currentMenu = _mainMenu;
                selection = ShowMenu(_currentMenu);
            }
        }

        private int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select what you want to do:\n ");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{i+1}. {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection > menuItems.Length || selection < 1)
            {
                Console.WriteLine("You need to select a number!");
            }
            return selection;
        }

        private void CustomerMenu(int selection)
        {
            Console.Clear();
            _currentMenu = _customerMenuItems;
            switch (@selection)
            {
                case 1 :
                    ListCustomers();
                    break;
                case 2 :
                    var foundCustomer = _customerService.FindCustomerById(PrintFindCustomerById());
                    Console.WriteLine(foundCustomer.FirstName);
                    break;
                case 3 :
                    var firsName = AskQuestion("First Name:");
                    var lastName = AskQuestion("Last Name:");
                    var address = AskQuestion("Address:");
                    var phoneNumber = AskQuestion("Phone Number:");
                    var email = AskQuestion("Email:");
                    var customer = _customerService.NewCustomer(firsName, lastName, address, phoneNumber, email);
                    _customerService.CreateCustomer(customer);
                    break;
                case 4 :
                    var id = PrintFindCustomerById();
                    _customerService.DeleteCustomer(id);
                    break;
                case 5 :
                    var idForEdit = PrintFindCustomerById();
                    var customerToEdit = _customerService.FindCustomerById(idForEdit);
                    Console.WriteLine("Updating " + customerToEdit.FirstName);
                    var newFirsName = AskQuestion("First Name:");
                    var newLastName = AskQuestion("Last Name:");
                    var newAddress = AskQuestion("Address:");
                    var newPhoneNumber = AskQuestion("Phone Number:");
                    var newEmail = AskQuestion("Email:");
                    _customerService.UpdateCustomer(new Customer()
                    {
                        Id = idForEdit,
                        FirstName = newFirsName,
                        LastName = newLastName,
                        Address = newAddress,
                        PhoneNumber = newPhoneNumber,
                        Email = newEmail
                    });
                    break;
                case 6 :
                    break;
            }
        }
        
        private void PetMenu(int selection)
        {
            Console.Clear();
            _currentMenu = _petMenuItems;
            switch (selection)
            {
                case 1 :
                    break;
                case 2 :
                    break;
                case 3 :
                    break;
                case 4 :
                    break;
                case 5 :
                    break;
                case 6 :
                    break;
                case 7 :
                    break;
                        
            }
        }
        
        #endregion

        private void ListCustomers()
        {
            Console.WriteLine("\nList Of Customers:");
            var customers = _customerService.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id:{customer.Id} First Name:{customer.FirstName} Last Name:{customer.LastName} "+
                                  $"Customer address:{customer.Address} Email:{customer.Email} Phone number:{customer.PhoneNumber}");
            }
        }
        
        private int PrintFindCustomerById()
        {
            Console.WriteLine("Insert Customer Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }

            return id;
        }

        static string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        #region DataInitialization

        private void InitData()
        {
            _customerService.CreateCustomer( new Customer
            {
                FirstName = "Bob",
                LastName = "Dylan",
                Address = "Bongo street 22",
                Email = "Bob@Dylan.com",
                PhoneNumber = "123456789"
            });

            _customerService.CreateCustomer( new Customer
            {
                FirstName = "Ding",
                LastName = "Kong",
                Address = "Chris Cross street 41",
                Email = "Donk@Kong.com",
                PhoneNumber = "987654321"
            });
        }
        
        #endregion
    }
}