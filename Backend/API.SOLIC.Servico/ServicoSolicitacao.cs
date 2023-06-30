using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using API.SOLIC.Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Servico
{
    public class ServicoSolicitacao : IServicoSolicitacao
    {
        private readonly IRepositorioSolicitacao _repositorioSolicitacao;

        public ServicoSolicitacao()
        {
            _repositorioSolicitacao = new RepositorioSolicitacao();
        }

        public async Task<List<Solicitacao>> BuscarTodasSolicitacaoAsync()
        {
            var solicitacoes = await _repositorioSolicitacao.BuscarTodasSolicitacaoAsync();
            return solicitacoes;
        }

        public async Task<Solicitacao> BuscarSolicitacaoPorIdAsync(string id)
        {
            var solicitacao = await _repositorioSolicitacao.BuscarSolicitacaoPorIdAsync(id);
            return solicitacao;
        }

        public async Task<RetornoDto> CriarSolicitacaoAsync(Solicitacao solicitacao)
        {
            var retornoDto = new RetornoDto();

            var ret = _repositorioSolicitacao.CriarSolicitacaoAsync(solicitacao);

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Gravar Solicitação";
                retornoDto.MensagemDeErro = "Erro no processo de gravar no Servidor";
            }

            return await Task.FromResult(retornoDto);
        }

        public async Task<RetornoDto> AutorizarSolicitacaoAsync(Solicitacao solicitacao)
        {
            var retornoDto = new RetornoDto();

            var ret = _repositorioSolicitacao.AutorizarSolicitacaoAsync(solicitacao);

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Autorizar Solicitacao";
                retornoDto.MensagemDeErro = "Erro no processo de autorizar solicitação no Servidor";
            }

            return await Task.FromResult(retornoDto);
        }

        public async Task<RetornoDto> DeletarSolicitacaoAsync(string id)
        {
            var retornoDto = new RetornoDto();

            var ret = _repositorioSolicitacao.DeletarSolicitacaoAsync($"{id}");

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Deletar Solicitação";
                retornoDto.MensagemDeErro = "Erro no processo de deletar no Servidor";
            }

            return await Task.FromResult(retornoDto);
        }
    }
}
