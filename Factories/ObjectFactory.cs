using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using ExcitedEmu.Models;
using Dapper;
using System.Linq;
namespace ExcitedEmu.Factories {
    public class ObjectFactory : IFactory<Object>{
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public ObjectFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        public List<Object> GetAllObjects() {
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = "SELECT * FROM objects";
                    dbConnection.Open();
                    return dbConnection.Query<Object>(query).ToList();
                }
            }
        }
        public string AddObject(Object Object){
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"INSERT INTO objects (name, description, image, quantity) VALUES (@name,@description,@image, @quantity)";
                dbConnection.Open();
                try
                {
                    dbConnection.Execute(query, Object);
                    return "Object added to database";
                }
                catch (MySqlException)
                {
                    return "Object already in database";
                }

            }
        }
        public List<JoinResult> GetOrders() {
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = $"SELECT objects.name, users.username, orders.created_at, orders.quantity FROM objects LEFT OUTER JOIN orders ON objects.idobjects = orders.objects_idobjects right outer Join users on users.idusers = orders.users_idusers where idorders > 0";
                    dbConnection.Open();
                    return dbConnection.Query<JoinResult>(query).ToList();
                }
            }
        }
        public void AddOrder(Order Order){
            using (IDbConnection dbConnection = Connection){
                string query = $"INSERT INTO orders (objects_idobjects, users_idusers, quantity, created_at) VALUES (@objects_idobjects, @users_idusers, @quantity, @created_at)";
                dbConnection.Execute(query, Order);
                query = $"UPDATE objects SET quantity = quantity - @quantity WHERE idobjects = @objects_idobjects";
                dbConnection.Execute(query,Order);
            }
        }
    }
}