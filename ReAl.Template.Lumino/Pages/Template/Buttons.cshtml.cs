using System.Collections.Generic;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Template
{
    public class ButtonsModel: BasePageModel
    {
        public ButtonsModel(db_seguridadContext context) : base(context)
        {
        }
        
        public List<SegAplicaciones> ListApp { get; private set; }
        public List<SegPaginas> ListPages { get; private set; }
        public string Usuario { get; private set; }
        public string CurrentApp { get; private set; }

        public void OnGet()
        {
            ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            CurrentApp = GetCurrentApp();
        }
    }
}