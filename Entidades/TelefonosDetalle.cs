using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RegistroDetalle.Entidades
{
    public class TelefonosDetalle
    {
        [Key]
        public int Id { get; set; }
        public String TipoTelefono { get; set; }
        public String Telefono { get; set; }

        public TelefonosDetalle()
        {
            Id = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }

        public TelefonosDetalle( string tipoTelefono, string telefono)
        {
            Id = 0;
            TipoTelefono = tipoTelefono;
            Telefono = telefono;
        }
    }
}
