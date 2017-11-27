using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegPersonas
    {
        public SegPersonas()
        {
            SegUsuarios = new HashSet<SegUsuarios>();
        }

        public long Idspe { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Ci { get; set; }
        public string Correo { get; set; }
        
        [ScaffoldColumn(false)]
        public string Apiestado { get; set; }
        
        [ScaffoldColumn(false)]
        public string Apitransaccion { get; set; }
        
        [ScaffoldColumn(false)]
        public string Usucre { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime Feccre { get; set; }
        
        [ScaffoldColumn(false)]
        public string Usumod { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime? Fecmod { get; set; }

        public ICollection<SegUsuarios> SegUsuarios { get; set; }
    }
}
