using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegUsuariosRestriccion
    {
        public long Idsur { get; set; }
        public long Idsus { get; set; }
        public long Idsro { get; set; }
        public int Rolactivo { get; set; }
        public string Restriccion { get; set; }
        public int Vigente { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegRoles IdsroNavigation { get; set; }
        public SegUsuarios IdsusNavigation { get; set; }
    }
}
