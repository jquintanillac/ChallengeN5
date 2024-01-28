using ChallengeN5.Api.Controllers;
using ChallengeN5.Api.Data.Entity;
using ChallengeN5.Api.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace challengen5.test
{
    public class ObtenerPermisosTests
    {
        [Fact]
        public async Task ObtenerPermisos_DebeRetornarOk()
        {
            // Configurar datos de prueba
            var mockLogger = new Mock<ILogger<ChallengerN5Controller>>();
            var mockChallengerServices = new Mock<IChallengerServices>();
            var listaPermisos = new List<Permisos>();
            mockChallengerServices.Setup(s => s.ObtenerPermisos()).ReturnsAsync(listaPermisos);

            // Configurar el controlador con las dependencias simuladas
            var controller = new ChallengerN5Controller(mockChallengerServices.Object, mockLogger.Object);

            // Actuar: llamar al método que se va a probar
            var resultado = await controller.ObtenerPermisos();

            // Assert: verificar si el resultado es el esperado
            var okResult = resultado as OkObjectResult;
            Assert.IsNotNull(okResult); // Verificar que el resultado no sea nulo
            Assert.Equals(StatusCodes.Status200OK, okResult.StatusCode); // Verificar que el código de estado sea 200 OK
                                                                        // Si hay alguna otra verificación específica del contenido del resultado, agrégala aquí
        }

    }
}

