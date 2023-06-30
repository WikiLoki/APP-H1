using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Dominio.Entidades
{
    public class Solicitacao
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("tema")]
        [BsonRepresentation(BsonType.String)]
        public string Tema { get; set; }

        [BsonElement("descricao")]
        [BsonRepresentation(BsonType.String)]
        public string Descricao { get; set; }

        [BsonElement("nomeAluno")]
        [BsonRepresentation(BsonType.String)]
        public string NomeAluno { get; set; }

        [BsonElement("nomeProfessor")]
        [BsonRepresentation(BsonType.String)]
        public string NomeProfessor { get; set; }

        [BsonElement("solicitacaoAceita")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool SolicitacaoAceita { get; set; }
    }
}
