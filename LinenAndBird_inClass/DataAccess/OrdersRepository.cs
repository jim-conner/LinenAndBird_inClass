using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LinenAndBird_inClass.DataAccess
{
    public class OrdersRepository
    {
        const string _connectionString = "Server =localhost;Database=LinenAndBird;Trusted_Connection=True;";

        internal IEnumerable<Order> GetAll()
        {
            //create a connection
            using var db = new SqlConnection(_connectionString);

            //got our sql with all the data
            var sql = @"select *
                        from Orders o
	                        join Birds b
		                        on b.Id = o.BirdId
	                        join Hats h
		                        on h.Id = o.BirdId";

            var results = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            }, splitOn:"Id"); //tells you when next object starts, checking per column

            return results;
        }

        internal void Add(Order order)
        {
            //create connection
            using var db = new SqlConnection(_connectionString);

            var sql = @"Insert into [dbo].[Orders]
                            ([BirdId], [HatId], [Price])
                        Output inserted.Id
                        Values
                            (@BirdId, @HatId, @Price)";

            var parameters = new 
            { 
                BirdId = order.Bird.Id,
                HatId = order.Hat.Id,
                Price = order.Price
            };

            var id = db.ExecuteScalar<Guid>(sql, parameters);


            order.Id = id;

            //_orders.Add(order);
        }

        internal Order Get(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"select *
                        from Orders o
	                        join Birds b
		                        on b.Id = o.BirdId
	                        join Hats h
		                        on h.Id = o.HatId
                        where o.id = @id";

            //multi-mapping doesn't work for anyother kind of dapper call, 
            //so we take the collection and turn it into one item ourselves
            var results = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            }, new { id }, splitOn: "Id");

            return results.FirstOrDefault();
        }
    }
}
