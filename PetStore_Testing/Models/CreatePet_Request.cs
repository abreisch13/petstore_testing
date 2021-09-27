using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore_Testing.Models
{
    public class CreatePet_Request
    {
        public CreatePet_RequestObject createPetRequestObjectProperty { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }

    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CreatePet_RequestObject
    {
        public int id { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }

        public string status { get; set; }
    }

}
