using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegTablas
    {
        public SegTablas()
        {
            SegConfiguracion = new HashSet<SegConfiguracion>();
            SegEstados = new HashSet<SegEstados>();
            SegGlosas = new HashSet<SegGlosas>();
            SegTransacciones = new HashSet<SegTransacciones>();
        }

        public long Idsta { get; set; }
        public long Idsap { get; set; }
        public string Nombretabla { get; set; }
        public string Alias { get; set; }
        public string Descripcion { get; set; }
        public bool? Beforestatement { get; set; }
        public bool? Beforerow { get; set; }
        public bool? Afterstatement { get; set; }
        public bool? Afterrow { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegAplicaciones IdsapNavigation { get; set; }
        public ICollection<SegConfiguracion> SegConfiguracion { get; set; }
        public ICollection<SegEstados> SegEstados { get; set; }
        public ICollection<SegGlosas> SegGlosas { get; set; }
        public ICollection<SegTransacciones> SegTransacciones { get; set; }
    }
}
