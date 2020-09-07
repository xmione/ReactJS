using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ReactJS.Redux.CodeFirst;
using ReactJS.Redux.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;

namespace ReactJS.Redux.Web.API.Controllers
{
    [Produces("application/json")]
    [Route("api/people")]
    public class PeopleController
    {
        Person[] people = new Person[]
        {
            new Person {ID = 1, FirstName = "Cynthia", MiddleName = "KungFu", LastName = "Luster", Email = "cynthialuster@test.com" },
            new Person {ID = 2, FirstName = "Bat", MiddleName = "Na", LastName = "Man", Email = "batman@test.com" },
            new Person {ID = 3, FirstName = "Spider", MiddleName = "Web", LastName = "Man", Email = "spiderman@test.com" },
            new Person {ID = 4, FirstName = "Super", MiddleName = "Dooper", LastName = "Man", Email = "superman@test.com" }
        };

        [HttpGet]
        public IEnumerable<Person> ListAllPeople()
        {
            return people;
        }

        [HttpGet("id/{idart}")]
        public IEnumerable<Person> ListPeopleByID(string idart)
        {
             IEnumerable<Person> retVal =
                from g in people 
                where g.ID.Equals(idart) 
                select g;

            return retVal;

        }

        [HttpGet("firstName/{firstart}")]
        public IEnumerable<Person> ListPeopleByFirstName(string firstart)
        {
            IEnumerable<Person> retVal = 
                from g in people
                where g.FirstName.ToUpper().Contains(firstart.ToUpper())
                orderby g.FirstName
                select g;
                
            return retVal;
            
        }
        [HttpPost("dummy")]
        public IEnumerable<Person> PopulateWithDummyData()
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["RRCConnectionString"].ConnectionString;
                var builder = new DbContextOptionsBuilder<RRCContext>();
                builder.UseSqlServer(connString, null);
                using (var context = new RRCContext())
                {
                    var person = new Person(){ ID = 1, FirstName = "Cynthia", MiddleName = "KungFu", LastName = "Luster", Email = "cynthialuster@test.com" };
                    context.People.Add(person);
                    context.SaveChanges();
                    if (context.Database.CanConnect())
                    {
                        // all good
                        var p = (from pl in context.People select pl).ToList();

                        p.AddRange(people);
                        context.SaveChanges();
                        people = p.ToArray();
                    }
                    else
                    {
                        throw new Exception("Failure connecting to database server.");
                    }
                    
                }

            }
            catch (Exception ex)
            {
                var exceptionTypeName = ex.GetType().Name;
            }            
            return people;

        }
    }
}