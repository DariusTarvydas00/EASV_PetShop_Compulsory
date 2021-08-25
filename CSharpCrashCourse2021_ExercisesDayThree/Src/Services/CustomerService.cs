using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrashCourse2021ExercisesDayThree.DB.Impl;
using CrashCourse2021ExercisesDayThree.Models;

namespace CrashCourse2021ExercisesDayThree.Services
{
    public class CustomerService
    {
        CustomerTable db; 
        public CustomerService()
        {
            this.db = new CustomerTable();
        }

        //Create and return a Customer Object with all incoming properties (no ID)
        internal Customer Create(string firstName, string lastName, DateTime birthDate)
        {
            Customer customer = new Customer();
            if (firstName.Length < 2)
            {
                throw new ArgumentException(Constants.FirstNameException);
            }
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.BirthDate = birthDate;
            return customer;
        }

        //db has an Add function to add a new customer!! :D
        //We can reuse the Create function above..
        internal Customer CreateAndAdd(string firstName, string lastName, DateTime birthDate)
        {
            return db.AddCustomer(Create(firstName, lastName, birthDate));
        }

        //Simple enough, Get the customers from the "Database" (db)
        internal List<Customer> GetCustomers()
        {
            return db.GetCustomers();
        }

        //Maybe Check out how to find in a LIST in c# Maybe there is a Find function?
        public Customer FindCustomer(int customerId)
        {
            if (customerId < 0)
            {
                throw new InvalidDataException(Constants.CustomerIdMustBeAboveZero);
            }

            return GetCustomers().Find(customer => customer.Id == customerId);
        }
        
        /*So many things can go wrong here...
          You need lots of exceptions handling in case of failure and
          a switch statement that decides what property of customer to use
          depending on the searchField. (ex. case searchfield = "id" we should look in customer.Id 
          Maybe add searchField.ToLower() to avoid upper/lowercase letters)
          Another thing is you should use FindAll here to get all that matches searchfield/searchvalue
          You could also make another search Method that only return One Customer
           and uses Find to get that customer and maybe even test it.
        */
        public List<Customer> SearchCustomer(string searchField, string searchValue)
        {
            switch (searchField)
            {
                case null:
                {
                    throw new InvalidDataException(Constants.CustomerSearchFieldCannotBeNull);
                }
                case "id":
                {
                    if (!int.TryParse(searchValue, out _))
                    {
                        throw new InvalidDataException(Constants.CustomerSearchValueWithFieldTypeIdMustBeANumber);
                    }
                    throw new InvalidDataException(Constants.CustomerIdMustBeAboveZero);

                }


                default:
                    if (int.TryParse(searchValue, out var number))
                    {
                        if (number < 0)
                        {
                            throw new InvalidDataException(Constants.CustomerIdMustBeAboveZero);
                        }
                    }

                    switch (searchValue)
                    {
                        case null:
                        {
                            throw new InvalidDataException(Constants.CustomerSearchValueCannotBeNull);
                        }
                        default:
                            var result = new List<Customer>();
                            if (int.TryParse(searchValue, out _))
                            {
                                result.AddRange(GetCustomers().FindAll(customer => customer.Id == int.Parse(searchValue)));
                                return result;
                            }

                            result.AddRange(GetCustomers().FindAll(customer => (customer.LastName == searchValue && 
                                customer.FirstName == searchValue) || customer.LastName == searchValue || customer.FirstName == searchValue));
                            //------OR------//
                            // foreach (var match in GetCustomers().FindAll(customer => (customer.LastName == searchValue && 
                            //     customer.FirstName == searchValue) || customer.LastName == searchValue || customer.FirstName == searchValue)) result.Add(match);

                            if (!result.Any())
                            {
                                throw new InvalidDataException(Constants.CustomerSearchFieldNotFound);
                            }

                            return result.Distinct().ToList();
                    }
            }
        }

    }
}
