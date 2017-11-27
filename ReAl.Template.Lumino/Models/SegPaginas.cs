using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegPaginas
    {
        public SegPaginas()
        {
            SegRolesPagina = new HashSet<SegRolesPagina>();
        }

        public long Idspg { get; set; }
        public long Idsap { get; set; }
        public decimal? Paginapadre { get; set; }
        public string Nombremenu { get; set; }
        public string Sigla { get; set; }
        public int Nivel { get; set; }
        public string Icono { get; set; }
        public string Metodo { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public decimal? Prioridad { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegAplicaciones IdsapNavigation { get; set; }
        public ICollection<SegRolesPagina> SegRolesPagina { get; set; }
    }
}
