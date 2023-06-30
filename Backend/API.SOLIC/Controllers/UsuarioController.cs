using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace API.SOLIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _servicoUsuario;

        public UsuarioController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HealthCheck()
        {
            StringBuilder informacoes = new StringBuilder();
            informacoes.AppendLine($"API SOLIC = API.SOLIC");
            informacoes.AppendLine($"Situação = Saudável");

            return Ok(informacoes.ToString());
        }

        [HttpGet]
        [Route("/usuarios")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarTodosOsUsuarios()
        {
            var usuarios = await _servicoUsuario.BuscarTodosOsUsuarioAsync();
            if (usuarios.Count == 0)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status404NotFound;
                detalhesDoProblema.Type = "NotFound";
                detalhesDoProblema.Title = "Registro não Encontrado";
                detalhesDoProblema.Detail = $"Não foram encontrados registros. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return NotFound(detalhesDoProblema);
            }
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("/usuarios/{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarUsuarioPorId(string id)
        {
            var usuario = await _servicoUsuario.BuscarUsuarioPorIdAsync(id);
            if (usuario is null)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status404NotFound;
                detalhesDoProblema.Type = "NotFound";
                detalhesDoProblema.Title = "Registro não Encontrado";
                detalhesDoProblema.Detail = $"Não foram encontrados registros. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return NotFound(detalhesDoProblema);
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("/usuarios/autenticar/{login}/{senha}")]
        [ProducesResponseType(typeof(Autenticado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AutenticarNoSistemaAsync(string login, string senha)
        {
            var autenticado = await _servicoUsuario.AutenticarNoSistemaAsync(login, senha);
            if (autenticado is null)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status404NotFound;
                detalhesDoProblema.Type = "NotFound";
                detalhesDoProblema.Title = "Registro não Encontrado";
                detalhesDoProblema.Detail = $"Não foram encontrados registros. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return NotFound(detalhesDoProblema);
            }
            return Ok(autenticado);
        }

        [HttpPost]
        [Route("/usuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            var usuarioExiste = await _servicoUsuario.BuscarUsuarioPorIdAsync(usuario.Id);

            if (usuarioExiste is not null)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status400BadRequest;
                detalhesDoProblema.Type = "BadRequest";
                detalhesDoProblema.Title = "Registros Duplicados";
                detalhesDoProblema.Detail = $"Já existe um usuario cadastrado com esse id {usuario.Id}. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return BadRequest(detalhesDoProblema);
            }

            var retornoDto = await _servicoUsuario.CriarUsuarioAsync(usuario);

            if (retornoDto.HouveErro == true)
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            else
            {
                return StatusCode((int)HttpStatusCode.Created, usuario);
            }

        }

        [HttpPut]
        [Route("/usuarios")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarUsuarioAsync(Usuario usuario)
        {
            var retornoDto = await _servicoUsuario.AtualizarUsuarioAsync(usuario);

            if (retornoDto.HouveErro == true)
            {
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.OK, usuario);
            }
        }

        [HttpDelete]
        [Route("/usuario/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarUsuarioAsync(string id)
        {
            var retornoDto = await _servicoUsuario.DeletarUsuarioAsync(id);

            if (retornoDto.HouveErro == true)
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            else
            {
                return StatusCode((int)HttpStatusCode.OK, id);
            }
        }
    }
}
