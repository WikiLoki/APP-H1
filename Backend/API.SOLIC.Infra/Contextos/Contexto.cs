using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using API.SOLIC.Utilitario;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Infra.Contextos
{
    internal class Contexto : IContexto
    {
        public IMongoCollection<Solicitacao> Solicitacoes { get; }
        public IMongoCollection<Usuario> Usuarios { get; }

        public Contexto()
        {
            var cliente = new MongoClient(ConfiguracoesDaAplicacao.ObterStringConexaoMongo());
            var bancoDeDados = cliente.GetDatabase("Solic");
            Usuarios = bancoDeDados.GetCollection<Usuario>("Usuario");
            Solicitacoes = bancoDeDados.GetCollection<Solicitacao>("Solicitacao");
        }
    }
}
