using System;
using System.Collections.Generic;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Core.Entity;
using CrashCourseVideoMenu.Infrastructure.Static.Data.Repositories;

namespace CrashCourseVideoMenu
{

    public class Printer
    {
        ICustomerRepository customerRepository;
     readonly List<Video> Videos = new List<Video>();
     string[] CurrentMenu;

        string[] MenuItems =
        {
            "Video",
            "Customer",
            "Search",
            "Exit"
        };

        string[] VideoMenuItems =
        {
            "Add video", 
            "Delete video", 
            "List all videos", 
            "Search for a video", 
            "Search for a genre", 
            "Go back",
        };

        string[] CustomerMenuItems =
        {
            "Add customer",
            "Delete costumer",
            "List all customers",
            "Search for a costumer",
            "Go back"
        };

        string[] SearchMenuItems =
        {
            "Search for a video",
            "Search for a customer",
            "Go back"
        };

        public Printer(){
            customerRepository = new CustomerRepository();
            CurrentMenu = MenuItems;
            AddVideo1();
            AddVideo2();
            AddCustomer1();
            AddCustomer2();
            ShowMenu(CurrentMenu); 
        }

        void ShowMenu(string[] menu)
        {
            var shouldContinue = true;
            while (shouldContinue)
            {
                Console.Clear();
                PrintCurrentMenu(menu);
                var enteredNumber = EnteredNumber(CurrentMenu);
                var shouldContinueSubMenu = true;
                if (menu == MenuItems && enteredNumber == menu.Length)
                {
                    shouldContinue = false;
                    Console.WriteLine(Constants.Bye);
                    Console.ReadLine();
                }
                else
                {
                    switch (enteredNumber)
                    {
                        case 1:
                            CurrentMenu = VideoMenuItems;
                            PrintCurrentMenu(CurrentMenu);
                            break;
                        case 2:
                            CurrentMenu = CustomerMenuItems;
                            PrintCurrentMenu(CurrentMenu);
                            break;
                        case 3:
                            CurrentMenu = SearchMenuItems;
                            PrintCurrentMenu(CurrentMenu);
                            break;
                    }

                    while (shouldContinueSubMenu)
                    {
                        var enteredSubMenuNumber = EnteredNumber(CurrentMenu);
                        if (CurrentMenu == VideoMenuItems)
                        {
                            if (enteredSubMenuNumber == VideoMenuItems.Length)
                            {
                                shouldContinueSubMenu = false;
                            }
                            else
                            {
                                VideoMenu(enteredSubMenuNumber);
                            }
                        }

                        if (CurrentMenu == CustomerMenuItems)
                        {
                            if (enteredSubMenuNumber == CustomerMenuItems.Length)
                            {
                                shouldContinueSubMenu = false;
                            }
                            else
                            {
                                CustomerMenu(enteredSubMenuNumber);
                            }
                        }

                        if (CurrentMenu == SearchMenuItems)
                        {
                            if (enteredSubMenuNumber == SearchMenuItems.Length)
                            {
                                shouldContinueSubMenu = false;
                            }
                            else
                            {
                                SearchMenu(enteredSubMenuNumber);
                            }
                        }
                    }
                }
            }
        }

        int EnteredNumber(string[] menu)
        {
            var selection = NumberOrNot();
            var numberInRange = CheckSelectionRange(selection);
            return numberInRange;
        }
        
        void SearchMenu(int number)
        {
            switch (number)
            {
                case 1 :
                    SearchForAVideo();
                    break;
                case 2 :
                    SearchForACustomerById();
                    break;
            }
        }
        
        void CustomerMenu(int number)
        {
            switch (number)
            {
                case 1 :
                    AddCustomer();
                    break;
                case 2 :
                    DeleteCustomer();
                    break;
                case 3 :
                    ListAllCustomers();
                    break;
                case 4 :
                    SearchForACustomerById();
                    break;
            }
        }

        void VideoMenu(int number)
        {
            switch (number)
            {
                case 1 :
                    AddVideo();
                    break;
                case 2 :
                    DeleteVideo();
                    break;
                case 3 :
                    ListAllVideos();
                    break;
                case 4 :
                    SearchForAVideo();
                    break;
                case 5 :
                    SearchForAGenre();
                    break;
            }
        }
        
        
        Customer SearchForACustomerById()
        {
            Console.WriteLine("Enter customer id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }
            return customerRepository.ReadById(id);
        }

        void ListAllCustomers()
        {
            PrintAllCustomers();
        }

        void DeleteCustomer()
        {
            var customerFound = SearchForACustomerById();
                if (customerFound != null)
                {
                    customerRepository.Delete(customerFound.Id);
                }
        }

        void PrintAllCustomers()
        {
            foreach (var customer in customerRepository.ReadAll())
            {
                Console.WriteLine($"Id: {customer.Id} Name: {customer.Name} Surname: {customer.Surname} Date of Birth: {customer.DateOfBirth}");
            }
        }

        void AddCustomer()
        {
            Console.WriteLine($"Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"SurName: ");
            string surName = Console.ReadLine();
            Console.WriteLine($"Date Of Birth: ");
            string dateOfBirth = Console.ReadLine();
            var cust = new Customer()
            {
                Name = name,
                Surname = surName,
                DateOfBirth = dateOfBirth,
            };
            customerRepository.Create(cust);
        }
        
        void SearchForAGenre()
        {
            Console.WriteLine("Enter Genre for search:");
            string genre = Console.ReadLine();
            foreach (var video in Videos)
            {
                if (video.Genre == genre.ToLower())
                {
                    Console.WriteLine($"Id: {video.Id} Title: {video.Title} Genre: {video.Genre} Release Date: {video.ReleaseDate} Story Line: {video.StoryLine}");
                }
            }
        }
        
        void SearchForAVideo()
        {
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out int number))
            {
                foreach (var video in Videos)
                {
                    if (video.Id == number)
                    {
                        Console.WriteLine($"Id: {video.Id} Title: {video.Title}");
                    }
                }
            }
            else
            {
                foreach (var video in Videos)
                {
                    if (video.Title.Contains(enteredValue))
                    {
                        Console.WriteLine($"Id: {video.Id} Title: {video.Title}");
                    }
                    else if (video.ReleaseDate.Equals(enteredValue))
                    {
                        Console.WriteLine($"Id: {video.Id} Title: {video.Title}");
                    }
                    else if (video.StoryLine.Contains(enteredValue))
                    {
                        Console.WriteLine($"Id: {video.Id} Title: {video.Title}");
                    }
                }
            }
        }

        void ListAllVideos()
        {
            PrintAllVideos();
        }
        
        void DeleteVideo()
        {
            PrintAllVideos();
            Console.WriteLine($"Please select video to delete by id: ");
            var idVideoToDelete = NumberOrNot();
            foreach (var video in Videos)
            {
                if (video.Id == idVideoToDelete)
                {
                    Videos.Remove(video);
                }
            }
        }

        void PrintAllVideos()
        {
            foreach (var video in Videos)
            {
                Console.WriteLine($"Id: {video.Id} Title: {video.Title} Release Date: {video.ReleaseDate} Genre: {video.Genre} Story Line: {video.StoryLine}");
            }
        }

        void AddVideo()
        {
            Console.WriteLine($"Enter Title: ");
            string title = Console.ReadLine();
            Console.WriteLine($"Enter Release Date: ");
            string releaseDate = Console.ReadLine();
            Console.WriteLine($"Enter Genre: ");
            string genre = Console.ReadLine();
            Console.WriteLine($"Enter Story Line: ");
            string storyLine = Console.ReadLine();
            Videos.Add(new Video()
            {
                Title = title,
                ReleaseDate = Convert.ToDateTime(releaseDate),
                Genre = genre,
                StoryLine = storyLine
            });
        }


        int CheckSelectionRange(int selection)
        {
            while (selection < 1 || selection > CurrentMenu.Length)
            {
                Console.WriteLine($"{Constants.EnterValueBetween} 1 and {CurrentMenu.Length}");
                return NumberOrNot();
            }

            return selection;
        }
        
        void PrintCurrentMenu(string[] menu)
        {
            for (var i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i+1}. {menu[i]}");
            }
            Console.WriteLine($"{Constants.EnterValueBetween} 1 and {menu.Length}");
        }
        
        int NumberOrNot()
        {
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }

            return selection;
        }

        void AddCustomer1()
        {
            var cust1 = new Customer()
            {
                Name = "John",
                Surname = "Johanson",
                DateOfBirth = "1998.20.12"
            };
            customerRepository.Create(cust1);
        }

        void AddCustomer2()
        {
            var cust2 = new Customer()
            {
                Name = "Peter",
                Surname = "Johanson",
                DateOfBirth = "1998.18.01"
            };
            customerRepository.Create(cust2);
        }

        void AddVideo1()
        {
            Videos.Add(new Video()
            {
                Title = "Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Fantasy",
                StoryLine = "Bam, Bam"
            });
        }
        
        void AddVideo2()
        {
            Videos.Add(new Video()
            {
                Title = "Not Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Not Fantasy",
                StoryLine = "Bam, Bam, Bam"
            });
        }
    }
}