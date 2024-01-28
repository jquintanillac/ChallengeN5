using ChallengeN5.Api.Data.Entity;

namespace ChallengeN5.Api.IServices
{
    public interface IChallengerServices
    {
        Task<string> SolicitarPermiso(Permisos permiso);
        Task<string> ModificarPermiso(int id, Permisos permiso);
        Task<IEnumerable<Permisos>> ObtenerPermisos();
    }
}
