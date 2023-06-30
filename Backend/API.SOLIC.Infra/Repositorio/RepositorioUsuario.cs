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
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly IContexto _contexto;

        public RepositorioUsuario()
        {
            _contexto = new Contexto();
        }

        public async Task<List<Usuario>> BuscarTodosOsUsuarioAsync()
        {
            var usuarios = await _contexto.Usuarios.FindAsync(_ => true);
            return usuarios.ToList();
        }

        public async Task<Usuario> BuscarUsuarioPorIdAsync(string id)
        {
            var usuario = await _contexto.Usuarios.Find(x => x.Id == id).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuario> AutenticarNoSistemaAsync(string login, string senha)
        {
            var autenticado = await _contexto.Usuarios.Find(x => x.Email == login && x.Senha == senha).FirstOrDefaultAsync();
            return autenticado;
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.InsertOneAsync(usuario);
        }

        [Obsolete]
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.ReplaceOneAsync(x => x.Id == usuario.Id, usuario, new UpdateOptions { IsUpsert = true });  
        }

        public async Task DeletarUsuarioAsync(string id)
        {
            await _contexto.Usuarios.DeleteOneAsync(x => x.Id == id);
        }
    }
}
