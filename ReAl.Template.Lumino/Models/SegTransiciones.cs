using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegTransiciones
    {
        public long Idsts { get; set; }
        public long Idsta { get; set; }
        public long Idsesini { get; set; }
        public long Idstr { get; set; }
        public long Idsesfin { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegTransacciones Idst { get; set; }
    }
}
