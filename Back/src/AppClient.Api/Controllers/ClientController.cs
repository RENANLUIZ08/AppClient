using AppClient.Domain.DTO;
using AppClient.Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AppClient.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase<ClientDto>
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Novo cadastro de Cliente", Description = "EndPoint de Cadastro de Cliente")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Cadastro realizado com sucesso!")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ocorreu um erro durante a transação, verifique as informações e tente novamente")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Verifique os dados enviados na requisicao e tente novamente.")]

        public override IActionResult Create([FromBody] ClientDto entity)
        {
            try
            {
                return Ok(_clientService.Create(entity));
            }
            catch (OperationCanceledException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno de cadastro, detalhes:{ex.Message}");
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualizar cadastro de Cliente", Description = "EndPoint de Atualização de Cliente")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Cadastro atualizado com sucesso!")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ocorreu um erro durante a transação, verifique as informações e tente novamente")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Verifique os dados enviados na requisicao e tente novamente.")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Não foram localizados dados com os as informacoes enviadas.")]

        public override IActionResult Update(int id, [FromBody] ClientDto entity)
        {
            try
            {
                return Ok(_clientService.Update(id, entity));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (OperationCanceledException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno de atualização, detalhes:{ex.Message}");
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Excluir cadastro de Cliente", Description = "EndPoint de Exclusão de Cliente")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Cadastro deletado com sucesso!")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ocorreu um erro durante a transação, verifique as informações e tente novamente")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Verifique os dados enviados na requisicao e tente novamente.")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Não foram localizados dados com os as informacoes enviadas.")]

        public override IActionResult Delete(int id)
        {
            try
            {
                _clientService.Delete(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (OperationCanceledException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno de exclusão, detalhes:{ex.Message}");
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter cadastro de Cliente pelo Id", Description = "EndPoint de Obter cliente pelo identificador")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Dados obtidos com sucesso!")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ocorreu um erro durante a transação, verifique as informações e tente novamente")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Verifique os dados enviados na requisicao e tente novamente.")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Não foram localizados dados com os as informacoes enviadas.")]

        public override IActionResult Get(int id)
        {
            try
            {
                return Ok(_clientService.GetById(id));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno ao selecionar por identificador, detalhes:{ex.Message}");
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter lista de cadastro de clientes", Description = "EndPoint de Obter os clientes cadastrados")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Dados obtidos com sucesso!")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ocorreu um erro durante a transação, verifique as informações e tente novamente")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Verifique os dados enviados na requisicao e tente novamente.")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Não foram localizados dados com os as informacoes enviadas.")]

        public override IActionResult GetAll()
        {
            try
            {
                return Ok(_clientService.GetAll());
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno ao selecionar lista, detalhes:{ex.Message}");
            }
        }


    }
}
