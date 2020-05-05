using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExampleConsole
{
    public class Livro
    {
        public Livro()
        {

        }

        public Livro(string titulo, string autor, int ano, int paginas, string[] assuntos)
        {
            Titulo = titulo;
            Autor = autor;
            Ano = ano;
            Paginas = paginas;
            Assunto = assuntos.ToList();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Paginas { get; set; }
        public List<string> Assunto { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


    }
}
