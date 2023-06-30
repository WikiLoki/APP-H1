using API.SOLIC.Dominio.Entidades;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Dominio.Interface
{
    public interface IContexto
    {
        IMongoCollection<Solicitacao> Solicitacoes { get; }
        IMongoCollection<Usuario> Usuarios { get; }
    }
}
