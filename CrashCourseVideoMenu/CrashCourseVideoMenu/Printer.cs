using System;
using System.Collections.Generic;
using System.Reflection;
using CrashCourseVideoMenu.Core.ApplicationService;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Core.Entity;
using CrashCourseVideoMenu.Infrastructure.Static.Data.Repositories;

namespace CrashCourseVideoMenu
{

    public class Printer: IPrinter
    {
        
        #region Repository area
        ICustomerService _customerService;
        IVideoRepository videoRepository;
        #endregion

        #region Allmenus

     string[] CurrentMenu;

     public void StartUI()
     {
            ShowMenu(CurrentMenu);
         
     }

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
        
        #endregion

        public Printer(ICustomerService customerService)
        {
            _customerService = customerService;
            CurrentMenu = MenuItems;
        }

        #region Menu
            
            void PrintCurrentMenu(string[] menu)
            {
                for (var i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine($"{i+1}. {menu[i]}");
                }
                Console.WriteLine($"{Constants.EnterValueBetween} 1 and {menu.Length}");
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
                    var title = AskQuestion("Title: ");
                    var releaseDate = AskQuestion("Release Date: ");
                    var storyLine = AskQuestion("Story Line: ");
                    var genre = AskQuestion("Genre: ");
                    break;
                case 2 :
                    PrintFindCutomerById();
                    break;
            }
        }
        
        void CustomerMenu(int number)
        {
            switch (number)
            {
                case 1 :
                    var firstName = AskQuestion("First name: ");
                    var lastName = AskQuestion("Last name: ");
                    var birthOfDate = AskQuestion("Date of birth: ");
                    var customer = _customerService.NewCustomer(firstName, lastName, birthOfDate);
                    _customerService.CreateCustomer(customer);
                    break;
                case 2 :
                    var idForDelete = PrintFindCutomerById();
                    _customerService.DeleteCustomer(idForDelete);
                    break;
                case 3 :
                    var idForEdit = PrintFindCutomerById();
                    var customerToEdit = _customerService.FindCustomerById(idForEdit);
                    var newFirstName = AskQuestion("First name: ");
                    var newLastName = AskQuestion("Last name: ");
                    var newBirthOfDate = AskQuestion("Date of birth: ");
                    _customerService.UpdateCustomer(new Customer()
                    {
                        Id = idForEdit,
                        Name = newFirstName,
                        Surname = newLastName,
                        DateOfBirth = newBirthOfDate
                    });
                    break;
                case 4 :
                    _customerService.GetAllCustomers();
                    break;
                case 5 :
                    PrintFindCutomerById();
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
                    DeleteVideo(int.Parse(Console.ReadLine()));
                    break;
                case 3 :
                    ListAllVideos();
                    break;
                case 4 :
                    PrintFindVideoById();
                    break;
                case 5 :
                    SearchForAGenre();
                    break;
            }
        }
        
        #endregion
        
        //UI
        int PrintFindCutomerById()
        {
            Console.WriteLine("Enter customer id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }

            return id;
        }

        string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
        
        #region Video
        
        void SearchForAGenre()
        {
            Console.WriteLine("Enter Genre for search:");
            string genre = Console.ReadLine();
            foreach (var video in videoRepository.ReadAll())
            {
                if (video.Genre == genre.ToLower())
                {
                    Console.WriteLine($"Id: {video.Id} Title: {video.Title} Genre: {video.Genre} Release Date: {video.ReleaseDate} Story Line: {video.StoryLine}");
                }
            }
        }
        
        Video FindVideoById(int id)
        {
            return videoRepository.ReadById(id);
        }

        int PrintFindVideoById()
        {
            Console.WriteLine("Enter customer id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }

            return id;
        }

        void ListAllVideos()
        {
            PrintAllVideos();
        }
        
        Video DeleteVideo(int id)
        {
            return videoRepository.Delete(id);
        }

        void PrintAllVideos()
        {
            foreach (var video in videoRepository.ReadAll())
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
            videoRepository.Create(new Video()
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

        int NumberOrNot()
        {
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine(Constants.InvalidNumber);
            }

            return selection;
        }
        
        #endregion

        #region Samples

        void AddCustomer1()
        {
            var cust1 = new Customer()
            {
                Name = "John",
                Surname = "Johanson",
                DateOfBirth = "1998.20.12"
            };
            _customerService.CreateCustomer(cust1);
        }

        void AddCustomer2()
        {
            var cust2 = new Customer()
            {
                Name = "Peter",
                Surname = "Johanson",
                DateOfBirth = "1998.18.01"
            };
            _customerService.CreateCustomer(cust2);
        }

        void AddVideo1()
        {
            videoRepository.Create(new Video()
            {
                Title = "Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Fantasy",
                StoryLine = "Bam, Bam"
            });
        }
        
        void AddVideo2()
        {
            videoRepository.Create(new Video()
            {
                Title = "Not Star Wars",
                ReleaseDate = Convert.ToDateTime("01.20.1992"),
                Genre = "Not Fantasy",
                StoryLine = "Bam, Bam, Bam"
            });
        }

        void addCustVid()
        {
            AddVideo1();
            AddVideo2();
            AddCustomer1();
            AddCustomer2();
        }
        
        #endregion
    }
}