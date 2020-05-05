using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ExampleConsole
{
    public class ConexaoMongoDB<T>
    {
        public const string ConnectionString = "mongodb://localhost:27017/";
        public const string MongoDB = "Biblioteca";
        public string CollectionName { get; set; }

        private static IMongoClient MCCliente;
        private static IMongoDatabase MDDatabase;
        public ConexaoMongoDB()
        {
            CollectionName = typeof(T).Name;

            MCCliente = new MongoClient(ConnectionString);
            MDDatabase = MCCliente.GetDatabase(MongoDB);

        }
        public IMongoClient Client
        {
            get { return MCCliente; }
        }
        public IMongoCollection<T> Collection
        {
            get { return MDDatabase.GetCollection<T>(CollectionName); }
        }
    }
}
