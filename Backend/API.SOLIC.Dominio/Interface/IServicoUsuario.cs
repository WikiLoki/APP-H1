using API.SOLIC.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Dominio.Interface
{
    public interface IServicoUsuario
    {
        Task<List<Usuario>> BuscarTodosOsUsuarioAsync();
        Task<Usuario> BuscarUsuarioPorIdAsync(string id);
        Task<Autenticado> AutenticarNoSistemaAsync(string login, string senha);
        Task<RetornoDto> CriarUsuarioAsync(Usuario usuario);
        Task<RetornoDto> AtualizarUsuarioAsync(Usuario usuario);
        Task<RetornoDto> DeletarUsuarioAsync(string id);
    }
}
