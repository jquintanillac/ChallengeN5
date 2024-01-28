using ChallengeN5.Api.Data;
using ChallengeN5.Api.Data.Entity;
using ChallengeN5.Api.IServices;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace ChallengeN5.Api.Services
{
    public class ChallengerServices : IChallengerServices
    {
        private readonly IElasticClient _elasticClient;
        private readonly Datacontext _context;
        private readonly KafkaProducer _kafkaProducer;        
        
        public ChallengerServices(IElasticClient elasticClient, Datacontext context, KafkaProducer kafkaProducer)
        {            
            _elasticClient = elasticClient;
            _context = context;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<string> SolicitarPermiso(Permisos permiso)
        {
            string result = "ok";
            try
            {
                              
                Permisos oPemisos = new Permisos
                {
                    EmpleadoID = permiso.EmpleadoID,
                    TipoPermisoID = permiso.TipoPermisoID,
                    PermisoDescripcion = permiso.PermisoDescripcion,
                    PermisoActivo = permiso.PermisoActivo,
                    PermisoFechaInicio = permiso.PermisoFechaInicio,
                    PermisoFechaFin = permiso.PermisoFechaFin,
                    Audit_fec_creacion = permiso.Audit_fec_creacion,
                    Audit_fec_modificacion = permiso.Audit_fec_modificacion,
                    Audit_user_creacion = permiso.Audit_user_creacion,
                    Audit_user_modificacion = permiso.Audit_user_modificacion
                };

                _context.Permisos.Add(oPemisos);
                await _context.SaveChangesAsync();
                var response = await _elasticClient.IndexDocumentAsync(permiso);                

                // Envío de mensaje a Kafka
                var mensaje = new
                {
                    ID = Guid.NewGuid().ToString(),
                    Operacion = "solicitar"
                };
                _kafkaProducer.ProduceMessages("operaciones", mensaje);


            }
            catch (Exception ex)
            {
                return result = ex.Message;
            }
        
            return result;
        }

        public async Task<string> ModificarPermiso(int id, Permisos permiso)
        {
            string result = "ok";
            try
            {
                // Verificar si el permiso con el ID dado existe en la base de datos
                var existingPermiso = await _context.Permisos.FindAsync(id);
                if (existingPermiso == null)
                {
                    return result = "no"; 
                }

                // Actualizar el permiso con los datos proporcionados
                existingPermiso.PermisoDescripcion = permiso.PermisoDescripcion;
                existingPermiso.PermisoActivo = permiso.PermisoActivo;
                existingPermiso.PermisoFechaInicio = permiso.PermisoFechaInicio;
                existingPermiso.PermisoFechaFin = permiso.PermisoFechaFin;

                _context.Permisos.Update(existingPermiso);
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                // Actualizar el documento en Elasticsearch
                var response = await _elasticClient.UpdateAsync<Permisos>(id, u => u.Doc(permiso));

                // Envío de mensaje a Kafka
                var mensaje = new
                {
                    ID = Guid.NewGuid().ToString(),
                    Operacion = "modificar"
                };
                _kafkaProducer.ProduceMessages("operaciones", mensaje);

                return result;

            }
            catch (Exception ex)
            {
                return result = ex.Message;
            }
        }

        public async Task<IEnumerable<Permisos>> ObtenerPermisos()
        {
            // Consulta la base de datos para obtener todos los permisos
            var permisosDesdeDB = await _context.Permisos.ToListAsync();

            // Consulta Elasticsearch para obtener todos los permisos
            var searchResponse = await _elasticClient.SearchAsync<Permisos>(s => s.MatchAll());

            // Combina los documentos de Elasticsearch con los permisos de la base de datos
            var permisosDesdeElasticsearch = searchResponse.Documents.ToList();

            // Combina ambas listas en una sola lista
            var todosLosPermisos = permisosDesdeDB.Concat(permisosDesdeElasticsearch);

            return todosLosPermisos;
        }

    }
}
