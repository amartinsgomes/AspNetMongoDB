using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task T = CreateBsonDocument();


            //Task T = ManipulandoClasses();

            //Task T = ManipulandoClassesEncapsulado();

            //Task T = InsertMany();

            //Task T = LendoLivros();
            Task T = BuscarLivros("Dom Casmurro 2");

            //MainSync(args);



            Console.ReadLine();
        }

        static void MainSync(string[] args)
        {
            Console.WriteLine("Esperando 10 segundos");
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Esperei 10 segundos");

        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Esperando 10 segundos no assincrono");
            await Task.Delay(10000);
            Console.WriteLine("Esperei 10 segundos no assincrono");

        }

        static async Task CreateBsonDocument()
        {
            var doc = new BsonDocument {
                { "titulo", "Arca de Noé" }
            };

            doc.Add("autor", "Robinson Crusoe");
            doc.Add("Ano", 1999);
            doc.Add("Paginas", 856);

            var assuntoArray = new BsonArray();
            assuntoArray.Add("Fantasia");
            assuntoArray.Add("Ação");

            doc.Add("Assunto", assuntoArray);

            Console.WriteLine(doc);

            var stringConexao = "mongodb://localhost:27017/";

            var dbClient = new MongoClient(stringConexao);
            IMongoDatabase db = dbClient.GetDatabase("Biblioteca");

            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("Livros");

            await collection.InsertOneAsync(doc);

            //var users = db.GetCollection<BsonDocument>("user");


            Console.WriteLine("Documento Incluído");
            /*

            var dbClient = new MongoClient("mongodb+srv://maxtemporada:jyYOUAO4yIANEFlT@cluster0-b0kcg.mongodb.net");
            IMongoDatabase db = dbClient.GetDatabase("betscore");
            var users = db.GetCollection<BsonDocument>("user");
            */

            /*foreach (var item in users.Find(new BsonDocument()).ToList())
            {
                Console.WriteLine($"{item.ToString()}");
            }
            Console.ReadLine();
            */
        }

        static async Task ManipulandoClasses()
        {
            Livro livro = new Livro()
            {
                Titulo = "Harry Potter",
                Autor = "Sandra de Sá",
                Ano = 1992,
                Paginas = 123,
                Assunto = new List<string>() { "Terror", "Aventura" }
            };

            var stringConexao = "mongodb://localhost:27017/";

            var dbClient = new MongoClient(stringConexao);
            IMongoDatabase db = dbClient.GetDatabase("Biblioteca");

            IMongoCollection<Livro> collection = db.GetCollection<Livro>("Livros");

            await collection.InsertOneAsync(livro);
            Console.WriteLine("Livro inserido através de classe");

        }

        static async Task ManipulandoClassesEncapsulado()
        {
            Livro livro = new Livro()
            {
                Titulo = "Noite sem fim",
                Autor = "Chupa Cabra",
                Ano = 1998,
                Paginas = 540,
                Assunto = new List<string>() { "Policial", "Romance", "Aventura" }
            };

            var livroConnection = new ConexaoMongoDB<Livro>();

            await livroConnection.Collection.InsertOneAsync(livro);

            Console.WriteLine("Livro inserido através de classe encapsulada");

        }

        static async Task InsertMany()
        {
            List<Livro> livros = new List<Livro>();

            livros.Add(new Livro("Dom Casmurro", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 2", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 4", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 10", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 20", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 30", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 32", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 33", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 34", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 35", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 36", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 37", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 38", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));
            livros.Add(new Livro("Dom Casmurro 39", "José Aldo", 1888, 120, new string[] { "Ficção", "Aventura" }));

            
            var livroConnection = new ConexaoMongoDB<Livro>();

            await livroConnection.Collection.InsertManyAsync(livros);

            Console.WriteLine("Livros inseridos através de classe encapsulada");

        }

        private static async Task LendoLivros()
        {
            var livroConnection = new ConexaoMongoDB<Livro>();

            var livros = await livroConnection.Collection.Find(new BsonDocument()).ToListAsync();

            Console.WriteLine($"Livros encontrados: {livros.Count}");

            foreach (var item in livros)
            {
                Console.WriteLine($"{item.ToString()}");
            }
            Console.ReadLine();

            

        }

        private static async Task BuscarLivros(string palavraChave)
        {
            var livroConnection = new ConexaoMongoDB<Livro>();

            var livroCriterios = new BsonDocument()
            {
                {"Titulo", palavraChave }
            };

            //var searchBuilder = new Builders<Livro>.Filter.Text()

            

            //livroConnection.Collection.Indexes.CreateOne(new CreateIndexModel<Livro>(Builders<Livro>.IndexKeys.Text("$**")));

            var livros = await livroConnection.Collection.Find(Builders<Livro>.Filter.Text(palavraChave)).ToListAsync();
            Console.WriteLine($"Livros encontrados: {livros.Count}");

            foreach (var item in livros)
            {
                Console.WriteLine($"{item.ToString()}");
            }
            Console.ReadLine();



        }
    }
}
