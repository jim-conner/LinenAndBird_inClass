using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace LinenAndBird_inClass.DataAccess
{
    public class BadgeRepository
    {
        readonly string _connectionString;

        public BadgeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LinenAndBird");
        }

        internal IEnumerable<Badge> GetByUserId(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = $@"Select * 
                        From Badges b
                        Where b.userId = @userId
                        Order By b.DateAcheived Desc";

            var badges = db.Query<Badge>(sql, new { id = userId });

            return badges;
        }
    }
}
