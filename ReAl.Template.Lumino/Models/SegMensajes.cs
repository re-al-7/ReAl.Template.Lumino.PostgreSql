using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegMensajes
    {
        public long Idsme { get; set; }
        public long Idsap { get; set; }
        public string Aplicacionerror { get; set; }
        public string Texto { get; set; }
        public string Origen { get; set; }
        public string Causa { get; set; }
        public string Accion { get; set; }
        public string Comentario { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegAplicaciones IdsapNavigation { get; set; }
    }
}
