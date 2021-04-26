using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class Restablecer_codigo : System.Web.UI.Page
    {
        string code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cuenta"] == null)
            {
                Server.Transfer("Login.aspx");
            }
            else
            {
                code = (string)Session["codeG"];
            }
        }

        protected void btnValidarCode_Click(object sender, EventArgs e)
        {
            if (code.Equals(this.txtcodigo.Text.Trim()))
            {
                Server.Transfer("Restablecer_contrasena.aspx");
            }
            else {
                Response.Write("<script>alert('" + "Código invalido" + "');</script>");
            }
        }
    }
}