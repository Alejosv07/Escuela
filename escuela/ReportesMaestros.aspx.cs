using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class ReportesMaestros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
        }

        protected void btnImprimirClick(object sender, EventArgs e)
        {

        }
        
        protected void btnImprimirNominaClick(object sender, EventArgs e)
        {

        }
        
        protected void btnImprimirPromedioClick(object sender, EventArgs e)
        {

        }


        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }
    }
}