using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegUsuarios
    {
        public SegUsuarios()
        {
            SegUsuariosRestriccion = new HashSet<SegUsuariosRestriccion>();
        }

        public long Idsus { get; set; }
        public long Idspe { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Vigente { get; set; }
        public string TokenPassword { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegPersonas IdspeNavigation { get; set; }
        public ICollection<SegUsuariosRestriccion> SegUsuariosRestriccion { get; set; }
    }
}
