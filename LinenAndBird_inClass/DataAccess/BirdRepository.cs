using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;
using Microsoft.Data.SqlClient;

namespace LinenAndBird_inClass.DataAccess
{
    public class BirdRepository
    {
        static List<Bird> _birds = new List<Bird>
        {
            new Bird
            {
                Id = Guid.NewGuid(),
                Name = "Jimmy",
                Color = "Red",
                Size = "Small",
                Type = BirdType.Dead,
                Accessories = new List<string>{"Beanie", "Gold wing tips"}
            }
        };

        internal IEnumerable<Bird> GetAll()
        {
            //if you add 'using' before 'var'
            //using = when I'm done with what's in {} close this shit/lexical scope
            using var connection = new SqlConnection(
                // Your App and also SQLServer can only have so many connections 
                //connnections as tunnel btw app & DB
                "Server = localhost; Database = LinenAndBird; Trusted_Connection = True;"
                );
            //connections aren't open by dafult; we've gotta do that ourselves
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"Select * From Birds";
            //executeReader is for when we care about getting all the results of query
            var reader = command.ExecuteReader();

            var birds = new List<Bird>();

            //data readers are weird... only get one row from results at a time
            while (reader.Read()) 
            {
                //Mapping data from the relational model to the object model
                var bird = new Bird();
                bird.Id = reader.GetGuid(0);
                bird.Size = reader["Size"].ToString();
                bird.Type = (BirdType)reader["Type"];
                bird.Color = reader["Color"].ToString();
                bird.Name = reader["Name"].ToString();

                //each bird goes in the list to return later
                birds.Add(bird);
            }

            return birds;
            //return _birds;
        }

        internal void Add(Bird newBird)
        {
            newBird.Id = Guid.NewGuid();
            _birds.Add(newBird);
        }

        internal Bird GetById(Guid birdId)
        {
            using var connection = new SqlConnection(
                "Server = localhost; Database = LinenAndBird; Trusted_Connection = True;"
                );

            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"Select * 
                                   From Birds
                                   Where id = @id";

            //using Paramterization prevents sql injections
            command.Parameters.AddWithValue("id", birdId);

            var reader = command.ExecuteReader();

            var birds = new List<Bird>();

            // GO BACK AND REVIEW Nathan's CODE after it's pushed up 
        }
    }
}
