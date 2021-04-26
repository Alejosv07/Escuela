using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class Restablecer_contrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cuenta"] == null)
            {
                Server.Transfer("Login.aspx");
            }
        }

        protected void btnPassNew_Click(object sender, EventArgs e)
        {
            Estudiante estudiante = (Estudiante)Session["Cuenta"];
            if (estudiante != null && !String.IsNullOrEmpty(this.txtPassNew.Text))
            {
                estudiante.Contra = this.txtPassNew.Text.Trim();
                EstudianteImp estudianteImp = new EstudianteImp();
                estudianteImp.Modificar(estudiante);
                Response.Write("<script>alert('" + "Contraseña actualizada" + "');</script>");
                Session["Cuenta"] = null;
                Server.Transfer("Login.aspx");
            }
            Profesores profesores = (Profesores)Session["Cuenta"];
            if (profesores != null && !String.IsNullOrEmpty(this.txtPassNew.Text))
            {
                profesores.Contra = this.txtPassNew.Text.Trim();
                ProfesoresImpt profesoresImpt = new ProfesoresImpt();
                profesoresImpt.Modificar(profesores);
                Response.Write("<script>alert('" + "Contraseña actualizada" + "');</script>");
                Session["Cuenta"] = null;
                Server.Transfer("Login.aspx");
            }
            Response.Write("<script>alert('" + "Campo contraseña vacio" + "');</script>");
        }
    }
}