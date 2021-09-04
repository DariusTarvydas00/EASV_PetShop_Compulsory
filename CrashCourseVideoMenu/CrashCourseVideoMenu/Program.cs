using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
using System.Xml;

namespace CrashCourseVideoMenu
{
    static class Program
    {
        private static int _id = 0;
        private static List<Customer> _customers = new List<Customer>();
        private static readonly List<Video> Videos = new List<Video>();
        private static string[] CurrentMenu;

        private static string[] MenuItems =
        {
            "Video",
            "Customer",
            "Search",
            "Exit"
        };

        private static string[] VideoMenuItems =
        {
            "Add video", 
            "Delete video", 
            "List all videos", 
            "Search for a video", 
            "Search for a genre", 
            "Go back",
        };

        private static string[] CustomerMenuItems =
        {
            "Add customer",
            "Delete costumer",
            "List all customers",
            "Search for a costumer",
            "Go back"
        };

        private static string[] SearchMenuItems =
        {
            "Search for a video",
            "Search for a customer",
            "Go back"
        };


        private static void Main(string[] args)
        {
            CurrentMenu = MenuItems;
            AddVideo1();
            AddVideo2();
            AddCustomer1();
            AddCustomer2();
            ShowMenu(CurrentMenu);
        }

        private static void ShowMenu(string[] menu)
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

        private static int EnteredNumber(string[] menu)
        {
            var selection = NumberOrNot();
            var numberInRange = CheckSelectionRange(selection);
            return numberInRange;
        }
        
        private static void SearchMenu(int number)
        {
            switch (number)
            {
                case 1 :
                    SearchForAVideo();
                    break;
                case 2 :
                    SearchForACustomer();
                    break;
            }
        }
        
        private static void CustomerMenu(int number)
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
                    SearchForACustomer();
                    break;
            }
        }

        private static void VideoMenu(int number)
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
        
        
        private static void SearchForACustomer()
        {
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out int number))
            {
                foreach (var customer in _customers)
                {
                    if (customer.Id == number)
                    {
                        Console.WriteLine($"Id: {customer.Id} Title: {customer.Name}");
                    }
                }
            }
            else
            {
                foreach (var customer in _customers)
                {
                    if (customer.Name.Contains(enteredValue))
                    {
                        Console.WriteLine($"Id: {customer.Id} Name: {customer.Name}");
                    }
                    else if (customer.Surname.Contains(enteredValue))
                    {
                        Console.WriteLine($"Id: {customer.Id} Name: {customer.Name}");
                    }
                    else if (customer.DateOfBirth.Contains(enteredValue))
                    {
                        Console.WriteLine($"Id: {customer.Id} Name: {customer.Name}");
                    }
                }
            }
        }

        private static void ListAllCustomers()
        {
            PrintAllCustomers();
        }

        private static void DeleteCustomer()
        {
            PrintAllCustomers();
            Console.WriteLine($"Please select customer to delete by id: ");
            var idCustomerToDelete = NumberOrNot();
            foreach (var customer in _customers)
            {
                if (customer.Id == idCustomerToDelete)
                {
                    _customers.Remove(customer);
                }
            }
        }

        private static void PrintAllCustomers()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine($"Id: {customer.Id} Name: {customer.Name} Surname: {customer.Surname} Date of Birth: {customer.DateOfBirth}");
            }
        }

        private static void AddCustomer()
        {
            Console.WriteLine($"Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"SurName: ");
            string surName = Console.ReadLine();
            Console.WriteLine($"Date Of Birth: ");
            string dateOfBirth = Console.ReadLine();
            _customers.Add(new Customer()
            {
                Id = _id++,
                Name = name,
                Surname = surName,
                DateOfBirth = dateOfBirth,
            });
        }
        
        private static void SearchForAGenre()
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
        
        private static void SearchForAVideo()
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

        private static void ListAllVideos()
        {
            PrintAllVideos();
        }
        
        private static void DeleteVideo()
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

        private static void PrintAllVideos()
        {
            foreach (var video in Videos)
            {
                Console.WriteLine($"Id: {video.Id} Title: {video.Title} Release Date: {video.ReleaseDate} Genre: {video.Genre} Story Line: {video.StoryLine}");
            }
        }

        private static void AddVideo()
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
                Id = _id++,
                Title = title,
                ReleaseDate = Convert.ToDateTime(releaseDate),
                Genre = genre,
                StoryLine = storyLine
            });
        }


        private static int CheckSelectionRange(int selection)
        {
            while (selection < 1 || selection > CurrentMenu.Length)
            {
                Console.WriteLine($"{Constants.EnterValueBetween} 1 and {CurrentMenu.Length}");
                return NumberOrNot();
            }

            return selection;
        }
        
        private static void PrintCurrentMenu(string[] menu)
        {
            for (var i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i+1}. {menu[i]}");
            }
            Console.WriteLine($"{Constants.EnterValueBetween} 1 and {menu.Length}");
        }
        
        private static int NumberOrNot()
        {
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }

            return selection;
        }

        private static void AddCustomer1()
        {
            _customers.Add(new Customer()
            {
                Id = _id++,
                Name = "John",
                Surname = "Johanson",
                DateOfBirth = "1998.20.12"
            });
        }
        
        private static void AddCustomer2()
        {
            _customers.Add(new Customer()
            {
                Id = _id++,
                Name = "Peter",
                Surname = "Johanson",
                DateOfBirth = "1998.18.01"
            });
        }

        private static void AddVideo1()
        {
            Videos.Add(new Video()
            {
                Id = _id++,
                Title = "Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Fantasy",
                StoryLine = "Bam, Bam"
            });
        }
        
        private static void AddVideo2()
        {
            Videos.Add(new Video()
            {
                Id = _id++,
                Title = "Not Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Not Fantasy",
                StoryLine = "Bam, Bam, Bam"
            });
        }
    }
}