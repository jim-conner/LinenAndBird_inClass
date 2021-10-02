using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LinenAndBird_inClass.Models;
using Microsoft.Data.SqlClient;

namespace LinenAndBird_inClass.DataAccess
{
    public class HatRepository
    {
        static List<Hat> _hats = new List<Hat>
        {
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Blue",
                    Designer = "Jimbo",
                    Style = HatStyle.OpenBack
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Black",
                    Designer = "Charlie",
                    Style = HatStyle.WideBrim
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Turqoise",
                    Designer = "Nathan",
                    Style = HatStyle.Normal
                }
        };

        const string _connectionString = "Server =localhost;Database=LinenAndBird;Trusted_Connection=True;";


        internal Hat GetById(Guid hatId)
        {
            using var db = new SqlConnection(_connectionString);

            var hat = db.QueryFirstOrDefault<Hat>("Select * From Hats Where Id = @id", new { id = hatId});
            //for parameters this is what dapper is doing internally:
            //for reach prop on the parameter object 
            //add a parameter w value to sql cmd
            //end for each
            // execute the cmd
            return hat;
            //return _hats.FirstOrDefault(hat => hat.Id == hatId); 
        }

        internal List<Hat> GetAll()
        {
            return _hats;
        }

        internal IEnumerable<Hat> GetHatsByStyle(HatStyle style)
        {
            return _hats.Where(hat => hat.Style == style);
        }

        internal void Add(Hat newHat)
        {
            newHat.Id = Guid.NewGuid();

            _hats.Add(newHat);
        }

    }
}
