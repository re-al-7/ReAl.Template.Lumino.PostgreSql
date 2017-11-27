using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegAplicaciones
    {
        public SegAplicaciones()
        {
            SegMensajes = new HashSet<SegMensajes>();
            SegPaginas = new HashSet<SegPaginas>();
            SegTablas = new HashSet<SegTablas>();
        }

        public long Idsap { get; set; }
        public string Sigla { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public ICollection<SegMensajes> SegMensajes { get; set; }
        public ICollection<SegPaginas> SegPaginas { get; set; }
        public ICollection<SegTablas> SegTablas { get; set; }
    }
}
