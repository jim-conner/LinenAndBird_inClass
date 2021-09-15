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
        const string _connectionString = "Server = localhost; Database = LinenAndBird; Trusted_Connection = True;";

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

        internal Bird Update(Guid id, Bird bird)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"update Birds
                                Set Color = @color,
                                Set Name = @name,
                                Set Type = @type,
                                Set Size = @size,
                            output inserted.*
                            Where id = @id";

            cmd.Parameters.AddWithValue("Type", bird.Type);
            cmd.Parameters.AddWithValue("Color", bird.Color);
            cmd.Parameters.AddWithValue("Size", bird.Size);
            cmd.Parameters.AddWithValue("Name", bird.Name);
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapFromReader(reader);
            }

        }

        internal void Remove(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Delete 
                                From Birds 
                                Where Id = @Id";

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        internal void Add(Bird newBird)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"insert into birds(Type,Color,Size,Name)
                                out inserted.Id
                                values (@Type,@Color, @Size, @Name)";

            cmd.Parameters.AddWithValue("Type", newBird.Type);
            cmd.Parameters.AddWithValue("Color", newBird.Color);
            cmd.Parameters.AddWithValue("Size", newBird.Size);
            cmd.Parameters.AddWithValue("Name", newBird.Name);

            //execute the query, but don't care about the results, just the # of rows
            //var NumOfRowsAffected = cmd.ExecuteNonQuery();

            //execute the query and only get the id of the new row
            var newId = (Guid)cmd.ExecuteScalar();

            newBird.Id = newId;
            //newBird.Id = Guid.NewGuid();
            //_birds.Add(newBird);
        }

        internal Bird GetById(Guid birdId)
        {
            using var connection = new SqlConnection(_connectionString);

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

        Bird MapFromReader (SqlDataReader reader)
        {
            var bird = new Bird();
            bird.Id = reader.GetGuid(0);
            bird.Size = reader["Size"].ToString();
            bird.Type = (BirdType)reader["Type"];
            bird.Color = reader["Color"].ToString();
            bird.Name = reader["Name"].ToString();
        }
    }
}
