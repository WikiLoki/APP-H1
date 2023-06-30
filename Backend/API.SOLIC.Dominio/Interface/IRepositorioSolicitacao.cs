using API.SOLIC.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Dominio.Interface
{
    public interface IRepositorioSolicitacao
    {
        Task<List<Solicitacao>> BuscarTodasSolicitacaoAsync();
        Task<Solicitacao> BuscarSolicitacaoPorIdAsync(string id);
        Task<RetornoDto> CriarSolicitacaoAsync(Solicitacao solicitacao);
        Task AutorizarSolicitacaoAsync(Solicitacao solicitacao);
        Task DeletarSolicitacaoAsync(string id);
    }
}
