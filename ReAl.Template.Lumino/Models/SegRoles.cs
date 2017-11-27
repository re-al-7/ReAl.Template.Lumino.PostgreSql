using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegRoles
    {
        public SegRoles()
        {
            SegRolesPagina = new HashSet<SegRolesPagina>();
            SegRolesTablaTransaccion = new HashSet<SegRolesTablaTransaccion>();
            SegUsuariosRestriccion = new HashSet<SegUsuariosRestriccion>();
        }

        public long Idsro { get; set; }
        public string Sigla { get; set; }
        public string Descripcion { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public ICollection<SegRolesPagina> SegRolesPagina { get; set; }
        public ICollection<SegRolesTablaTransaccion> SegRolesTablaTransaccion { get; set; }
        public ICollection<SegUsuariosRestriccion> SegUsuariosRestriccion { get; set; }
    }
}
