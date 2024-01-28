using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChallengeN5.Api.Data.Entity
{
    [Table("Empleados")]
    public class Empleados
    {
        [Key]
        public int EmpleadoID { get; set; }

        [Required(ErrorMessage = "El nombre del empleado es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre del empleado no puede tener más de 50 caracteres.")]
        public required string EmpleadoNombre { get; set; }
        [Required(ErrorMessage = "El apellido del empleado es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El apellido del empleado no puede tener más de 50 caracteres.")]
        public required string EmpleadoApellido { get; set; }
        [Required(ErrorMessage = "El documento de identidad del empleado es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El documeto de identidad del empleado no puede tener más de 12 caracteres.")]
        public required string EmepleadoDocidentidad { get; set; }
        [Required(ErrorMessage = "El puesto del empleado es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El puesto del empleado no puede tener más de 30 caracteres.")]
        public required string EmpleadoPuesto { get; set; }
        [Required(ErrorMessage = "El departamento del empleado es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El departamento del empleado no puede tener más de 30 caracteres.")]
        public required string EmpleadoDepartamento { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EmpleadoFechaInicio { get; set; }
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
