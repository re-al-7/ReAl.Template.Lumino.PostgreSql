using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegConfiguracion
    {
        public long Idscf { get; set; }
        public long Idsta { get; set; }
        public string Configuracion { get; set; }
        public string Funcion { get; set; }
        public string Parametrosentrada { get; set; }
        public string Parametrossalida { get; set; }
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
