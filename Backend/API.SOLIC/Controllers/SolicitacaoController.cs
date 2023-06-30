using API.SOLIC.Dominio.Entidades;
using API.SOLIC.Dominio.Interface;
using API.SOLIC.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace API.SOLIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoController : ControllerBase
    {
        private readonly IServicoSolicitacao _servicoSolicitacao;

        public SolicitacaoController(IServicoSolicitacao servicoSolicitacao)
        {
            _servicoSolicitacao = servicoSolicitacao;
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
        [Route("/solicitacao")]
        [ProducesResponseType(typeof(Solicitacao), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarTodasSolicitacaoAsync()
        {
            var solicitacoes = await _servicoSolicitacao.BuscarTodasSolicitacaoAsync();
            if (solicitacoes.Count == 0)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status404NotFound;
                detalhesDoProblema.Type = "NotFound";
                detalhesDoProblema.Title = "Registro não Encontrado";
                detalhesDoProblema.Detail = $"Não foram encontrados registros. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return NotFound(detalhesDoProblema);
            }
            return Ok(solicitacoes);
        }

        [HttpGet]
        [Route("/solicitacao/{id}")]
        [ProducesResponseType(typeof(Solicitacao), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarSolicitacaoPorIdAsync(string id)
        {
            var solicitacao = await _servicoSolicitacao.BuscarSolicitacaoPorIdAsync(id);
            if (solicitacao is null)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status404NotFound;
                detalhesDoProblema.Type = "NotFound";
                detalhesDoProblema.Title = "Registro não Encontrado";
                detalhesDoProblema.Detail = $"Não foram encontrados registros. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return NotFound(detalhesDoProblema);
            }
            return Ok(solicitacao);
        }

        [HttpPost]
        [Route("/solicitacao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarSolicitacaoAsync([FromBody] Solicitacao solicitacao)
        {
            var solicitacaoExistente = await _servicoSolicitacao.BuscarSolicitacaoPorIdAsync(solicitacao.Id);

            if (solicitacaoExistente is not null)
            {
                ProblemDetails detalhesDoProblema = new ProblemDetails();
                detalhesDoProblema.Status = StatusCodes.Status400BadRequest;
                detalhesDoProblema.Type = "BadRequest";
                detalhesDoProblema.Title = "Registros Duplicados";
                detalhesDoProblema.Detail = $"Já existe uma solicitação cadastrada com esse id {solicitacao.Id}. ";
                detalhesDoProblema.Instance = HttpContext.Request.Path;
                return BadRequest(detalhesDoProblema);
            }

            var retornoDto = await _servicoSolicitacao.CriarSolicitacaoAsync(solicitacao);

            if (retornoDto.HouveErro == true)
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            else
            {
                return StatusCode((int)HttpStatusCode.Created, solicitacao);
            }

        }

        [HttpPut]
        [Route("/solicitacao")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AutorizarSolicitacaoAsync(Solicitacao solicitacao)
        {
            var retornoDto = await _servicoSolicitacao.AutorizarSolicitacaoAsync(solicitacao);

            if (retornoDto.HouveErro == true)
            {
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.OK, solicitacao);
            }
        }

        [HttpDelete]
        [Route("/solicitacao/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarSolicitacaoAsync(string id)
        {
            var retornoDto = await _servicoSolicitacao.DeletarSolicitacaoAsync(id);

            if (retornoDto.HouveErro == true)
                return retornoDto.RetornarResultado(HttpContext.Request.Path);
            else
            {
                return StatusCode((int)HttpStatusCode.OK, id);
            }
        }
    }
}
