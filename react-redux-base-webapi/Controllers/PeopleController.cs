using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models;

namespace Controllers
{
    [Produces("application/json")]
    [Route("api/people")]
    public class PeopleController
    {
        People[] people = new People[]
        {
            new People {ID = 1, FirstName = "Cynthia", MiddleName = "KungFu", LastName = "Luster" },
            new People {ID = 2, FirstName = "Bat", MiddleName = "Na", LastName = "Man" },
            new People {ID = 3, FirstName = "Spider", MiddleName = "Web", LastName = "Man" },
            new People {ID = 4, FirstName = "Super", MiddleName = "Dooper", LastName = "Man" }
        };

        [HttpGet]
        public IEnumerable<People> ListAllPeople()
        {
            return people;
        }

        [HttpGet("id/{idart}")]
        public IEnumerable<People> ListPeopleByID(string idart)
        {
             IEnumerable<People> retVal =
                from g in people 
                where g.ID.Equals(idart) 
                select g;

            return retVal;

        }

        [HttpGet("firstName/{firstart}")]
        public IEnumerable<People> ListPeopleByFirstName(string firstart)
        {
            IEnumerable<People> retVal = 
                from g in people
                where g.FirstName.ToUpper().Contains(firstart.ToUpper())
                orderby g.firstName
                select g;
                
            return retVal;
            
        }
    }
}