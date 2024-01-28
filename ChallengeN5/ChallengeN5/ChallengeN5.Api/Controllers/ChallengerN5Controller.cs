using ChallengeN5.Api.Data;
using ChallengeN5.Api.Data.Entity;
using ChallengeN5.Api.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;

namespace ChallengeN5.Api.Controllers
{
    [Route("apin5/[controller]")]
    [ApiController]
    public class ChallengerN5Controller : ControllerBase
    {
        private readonly IChallengerServices _challengern5;
        private readonly ILogger<ChallengerN5Controller> _logger;

        public ChallengerN5Controller(IChallengerServices challengern5, ILogger<ChallengerN5Controller> logger)
        {
            _challengern5 = challengern5;
            _logger = logger;
        }

        [HttpPost("SolicitarPermiso")]
        public async Task<IActionResult> SolicitarPermiso([FromBody] Permisos permiso)
        {
            try
            {
                _logger.LogInformation("Solicitando permiso inicio");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error al enviar el modelo");
                }


                var respuesta = await _challengern5.SolicitarPermiso(permiso);

                if(respuesta == "ok")
                {
                    _logger.LogInformation("Permiso solicitado correctamente");
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    _logger.LogError("Error al solicitar permiso");
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al solicitar permiso");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al solicitar permiso: {ex.Message}");
            }
        }

        [HttpPost("ModificarPermiso/{id}")]
        public async Task<IActionResult> ModificarPermiso(int id, [FromBody] Permisos permiso)
        {
            try
            {
                _logger.LogInformation("Modificar permiso inicio");
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error al enviar el modelo");
                }

                var respuesta = await _challengern5.ModificarPermiso(id, permiso);

                if(respuesta =="ok")
                {
                    _logger.LogInformation("Permiso modificado correctamente");
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    _logger.LogError("Error al modificar permiso");
                    return BadRequest(respuesta);
                }

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar permiso");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al modificar el permiso: {ex.Message}");
            }
        }

        [HttpGet("ObtenerPermisos")]
        public async Task<IActionResult> ObtenerPermisos()
        {
            try
            {
                _logger.LogInformation("Obtener permiso inicio");
                return Ok(await _challengern5.ObtenerPermisos());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obetner permiso");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los permisos: {ex.Message}");
            }
        }
    }
}
