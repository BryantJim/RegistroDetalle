using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDetalle.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public String Nombre { get; set; }
        public String Cedula { get; set; }
        public String Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey("PersonaId")]
        public virtual List<TelefonosDetalle> Telefonos { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            FechaNacimiento = DateTime.Now;
            Telefonos = new List<TelefonosDetalle>();
        }
    }
}
