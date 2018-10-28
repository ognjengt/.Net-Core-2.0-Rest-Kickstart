using MongoDB.Driver;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    // Singleton class

    public class MongoDriver
    {
        private static MongoDriver mongoDriverInstance = null;
        private static readonly object padlock = new object();
        private MongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }

        // Collections
        public IMongoCollection<User> UsersCollection { get; set; }

        MongoDriver() { }

        // Returns a singleton instance
        public static MongoDriver MongoDriverInstance
        {
            get
            {
                lock (padlock)
                {
                    if (mongoDriverInstance == null)
                    {
                        mongoDriverInstance = new MongoDriver();
                    }
                    return mongoDriverInstance;
                }
            }
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        /// <param name="address">Database connection string</param>
        /// <param name="databaseName">Database name</param>
        public void ConnectToDB(string address, string databaseName)
        {
            this.Client = new MongoClient(MongoUrl.Create(address));
            this.Database = Client.GetDatabase(databaseName);
            this.UsersCollection = this.Database.GetCollection<User>("Users");
        }

    }
}
