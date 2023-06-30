using API.SOLIC.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Dominio.Interface
{
    public interface IRepositorioUsuario
    {
        Task<List<Usuario>> BuscarTodosOsUsuarioAsync();
        Task<Usuario> BuscarUsuarioPorIdAsync(string id);
        Task<Usuario> AutenticarNoSistemaAsync(string login, string senha);
        Task CriarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task DeletarUsuarioAsync(string id);
    }
}
