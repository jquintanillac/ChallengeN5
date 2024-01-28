using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChallengeN5.Api.Data.Entity
{
    [Table("Permisos")]
    public class Permisos
    {
        [Key]
        public int PermisoID { get; set; }
        [JsonIgnore]
        public int EmpleadoID { get; set; }
        [JsonIgnore]
        public int TipoPermisoID { get; set; }
        [Required(ErrorMessage = "La descripcion del permiso es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El permiso no puede tener más de 50 caracteres.")]
        public required string PermisoDescripcion { get; set; }
        [Required(ErrorMessage = "La descripcion del permiso es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El permiso no puede tener más de 50 caracteres.")]
        public required string PermisoActivo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PermisoFechaInicio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PermisoFechaFin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Audit_fec_creacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Audit_fec_modificacion { get; set; }
        public string? Audit_user_creacion { get; set; }
        public string? Audit_user_modificacion { get; set; }
        public virtual Empleados? Empleado { get; set; } 
        public virtual TiposPermisos? TipoPermiso { get; set; } 
    }
}
