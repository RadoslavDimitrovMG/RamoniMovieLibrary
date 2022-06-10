using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Age must be between {0} and {1}.")]
        public int Age { get; set; }

        [Required, MaxLength(64)]
        public string Country { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

        private User()
        {

        }

        public User(string name, int age, string country)
        {
            Name = name;
            Age = age;
            Country = country;
        }
    }
}

