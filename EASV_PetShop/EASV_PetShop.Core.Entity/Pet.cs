using System;

namespace EASV_PetShop.Core.Entity
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type PetType { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }
}