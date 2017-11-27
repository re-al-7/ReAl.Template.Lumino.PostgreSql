using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegRolesPagina
    {
        public long Idsrp { get; set; }
        public long Idsro { get; set; }
        public long Idspg { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegPaginas IdspgNavigation { get; set; }
        public SegRoles IdsroNavigation { get; set; }
    }
}
