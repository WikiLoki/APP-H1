using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using API.SOLIC.Infra.Contextos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Infra.Repositorio
{
    public class RepositorioSolicitacao : IRepositorioSolicitacao
    {
        private readonly IContexto _contexto;

        public RepositorioSolicitacao()
        {
            _contexto = new Contexto();
        }

        public async Task<List<Solicitacao>> BuscarTodasSolicitacaoAsync()
        {
            var solicitacoes = await _contexto.Solicitacoes.FindAsync(_ => true);
            return solicitacoes.ToList();
        }

        public async Task<Solicitacao> BuscarSolicitacaoPorIdAsync(string id)
        {
            var solicitacao = await _contexto.Solicitacoes.Find(x => x.Id == id).FirstOrDefaultAsync();
            return solicitacao;
        }

        public async Task<RetornoDto> CriarSolicitacaoAsync(Solicitacao solicitacao)
        {
            RetornoDto retornoDto = new RetornoDto();
            var usuario = await _contexto.Usuarios.Find(x => x.Nome == solicitacao.NomeAluno).FirstOrDefaultAsync();
            if (usuario != null)
            {
                await _contexto.Solicitacoes.InsertOneAsync(solicitacao);
                var retorno = retornoDto.ObjetoRetorno = solicitacao;
                return (RetornoDto)retorno;
            }
            else
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "500";
                retornoDto.TituloErro = "Erro";
                retornoDto.MensagemDeErro = $"Erro ao criar solicitação.";
                return retornoDto;
            }
        }

        public async Task AutorizarSolicitacaoAsync(Solicitacao solicitacao)
        {
            await _contexto.Solicitacoes.ReplaceOneAsync(x => x.Id == solicitacao.Id, solicitacao, new UpdateOptions { IsUpsert = true });
        }

        public async Task DeletarSolicitacaoAsync(string id)
        {
            await _contexto.Solicitacoes.DeleteOneAsync(x => x.Id == id);
        }
    }
}
