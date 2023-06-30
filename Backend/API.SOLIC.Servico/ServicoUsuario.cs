using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using API.SOLIC.Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Servico
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario()
        {
            _repositorioUsuario = new RepositorioUsuario();
        }

        public async Task<List<Usuario>> BuscarTodosOsUsuarioAsync()
        {
            var usuarios = await _repositorioUsuario.BuscarTodosOsUsuarioAsync();
            return usuarios;
        }

        public async Task<Usuario> BuscarUsuarioPorIdAsync(string id)
        {
            var usuario = await _repositorioUsuario.BuscarUsuarioPorIdAsync(id);
            return usuario;
        }

        public async Task<Autenticado> AutenticarNoSistemaAsync(string login, string senha)
        {
            var autenticado = new Autenticado();
            var usuarioAutenticado = await _repositorioUsuario.AutenticarNoSistemaAsync(login, senha);
            if (usuarioAutenticado != null)
            {
                autenticado.Nome = usuarioAutenticado.Nome;
                autenticado.Mensagem = "Autenticado com Sucesso";
            }
            else
            {
                autenticado.Nome = "Usuario Incorreto";
                autenticado.Mensagem = "Erro ao autenticar";
            }
            return autenticado;
        }

        public async Task<RetornoDto> CriarUsuarioAsync(Usuario usuario)
        {
            var retornoDto = new RetornoDto();
            
            var ret = _repositorioUsuario.CriarUsuarioAsync(usuario);

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Gravar Usuario";
                retornoDto.MensagemDeErro = "Erro no processo de gravar o Servidor";
            }

            return await Task.FromResult(retornoDto);
        }

        public async Task<RetornoDto> AtualizarUsuarioAsync(Usuario usuario)
        {
            var retornoDto = new RetornoDto();

            var ret = _repositorioUsuario.AtualizarUsuarioAsync(usuario);

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Atualizar Usuario";
                retornoDto.MensagemDeErro = "Erro no processo de atualizar no Servidor";
            }

            return await Task.FromResult(retornoDto);
        }

        public async Task<RetornoDto> DeletarUsuarioAsync(string id)
        {
            var retornoDto = new RetornoDto();
            
            var ret = _repositorioUsuario.DeletarUsuarioAsync($"{id}");

            if (ret.Exception != null)
            {
                retornoDto.HouveErro = true;
                retornoDto.CodigoErro = "400";
                retornoDto.TituloErro = "Deletar Usuario";
                retornoDto.MensagemDeErro = "Erro no processo de deletar no Servidor";
            }

            return await Task.FromResult(retornoDto);
        }
    }
}
