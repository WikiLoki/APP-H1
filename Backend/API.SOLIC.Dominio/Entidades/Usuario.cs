using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.SOLIC.Dominio.Entidades
{
    public class Usuario
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("nome")]
        [BsonRepresentation(BsonType.String)]
        public string Nome { get; set; }

        [BsonElement("senha")]
        [BsonRepresentation(BsonType.String)]
        public string Senha { get; set; }

        [BsonElement("documento")]
        [BsonRepresentation(BsonType.String)]
        public string Documento { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("professor")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Professor { get; set; }

        [BsonElement("aluno")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Aluno { get; set; }
    }
}
