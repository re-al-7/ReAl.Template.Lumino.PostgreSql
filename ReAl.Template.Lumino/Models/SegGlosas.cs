using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegGlosas
    {
        public long Idsgl { get; set; }
        public long Iddoc { get; set; }
        public string Nombretabla { get; set; }
        public string Transaccion { get; set; }
        public string Glosa { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegTablas NombretablaNavigation { get; set; }
    }
}
