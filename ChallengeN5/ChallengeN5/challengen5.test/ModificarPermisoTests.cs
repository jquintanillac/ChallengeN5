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
    public class ModificarPermisoTests
    {
        [Fact]
        public async Task ModificarPermiso_DebeRetornarOk()
        {
            // Configurar datos de prueba
            var id = 1; // Id de permiso
            var permiso = new Permisos
            {
                PermisoID = id,
                EmpleadoID = 1,
                TipoPermisoID = 1,
                PermisoDescripcion = "Descripción del permiso",
                PermisoActivo = "Activo",
                PermisoFechaInicio = DateTime.Now,
                PermisoFechaFin = DateTime.Now.AddDays(7),
                Audit_fec_creacion = DateTime.Now,
                Audit_fec_modificacion = DateTime.Now,
                Audit_user_creacion = "UsuarioCreacion",
                Audit_user_modificacion = "UsuarioModificacion",
                // Si Empleado y TipoPermiso son objetos
            };
            var mockLogger = new Mock<ILogger<ChallengerN5Controller>>();
            var mockChallengerServices = new Mock<IChallengerServices>();
            mockChallengerServices.Setup(s => s.ModificarPermiso(id, It.IsAny<Permisos>())).ReturnsAsync("ok");

            // Crear instancia del controlador
            var controller = new ChallengerN5Controller(mockChallengerServices.Object, mockLogger.Object);

            // Ejecutar acción
            var result = await controller.ModificarPermiso(id, permiso) as ObjectResult;

            // Verificar resultado
            Assert.IsNotNull(result);
            Assert.Equals(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
