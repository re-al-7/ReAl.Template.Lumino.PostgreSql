using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegEstados
    {
        public long Idses { get; set; }
        public long Idsta { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegTablas IdstaNavigation { get; set; }
    }
}
