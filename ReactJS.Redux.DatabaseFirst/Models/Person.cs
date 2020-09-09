using System;
using System.Collections.Generic;

#nullable disable

namespace ReactJS.Redux.DatabaseFirst.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
