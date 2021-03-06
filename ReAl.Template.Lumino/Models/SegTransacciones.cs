﻿using System;
using System.Collections.Generic;

namespace ReAl.Template.Lumino.Models
{
    public partial class SegTransacciones
    {
        public SegTransacciones()
        {
            SegRolesTablaTransaccion = new HashSet<SegRolesTablaTransaccion>();
            SegTransiciones = new HashSet<SegTransiciones>();
        }

        public long Idstr { get; set; }
        public long Idsta { get; set; }
        public string Transaccion { get; set; }
        public string Sentencia { get; set; }
        public string Descripcion { get; set; }
        public string Apiestado { get; set; }
        public string Apitransaccion { get; set; }
        public string Usucre { get; set; }
        public DateTime Feccre { get; set; }
        public string Usumod { get; set; }
        public DateTime? Fecmod { get; set; }

        public SegTablas IdstaNavigation { get; set; }
        public ICollection<SegRolesTablaTransaccion> SegRolesTablaTransaccion { get; set; }
        public ICollection<SegTransiciones> SegTransiciones { get; set; }
    }
}
