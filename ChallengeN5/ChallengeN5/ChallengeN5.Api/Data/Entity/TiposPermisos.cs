using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChallengeN5.Api.Data.Entity
{
    [Table("TipoPermisos")]
    public class TiposPermisos
    {
        [Key]
        public int TipoPermisoID { get; set; }
        [Required(ErrorMessage = "La descripcion del tipo de permiso es obligatorio.")]
        [MaxLength(50, ErrorMessage = "La descripcion del tipo de permiso no puede tener más de 50 caracteres.")]
        public required string TipoPermisoDescripcion { get; set; }
       
        [MaxLength(50, ErrorMessage = "la observacion no puede tener más de 50 caracteres.")]
        public string? TipoPermisoObservacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Audit_fec_creacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Audit_fec_modificacion { get; set; }
        public string? Audit_user_creacion { get; set; }
        public string? Audit_user_modificacion { get; set; }
        [JsonIgnore]
        public ICollection<Permisos>? Permisos { get; set; }

    }
}
