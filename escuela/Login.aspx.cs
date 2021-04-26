using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIdentificarme_Click(object sender, EventArgs e)
        {
            EstudianteImp estudianteImp = new EstudianteImp();
            Estudiante estudiante = estudianteImp.listaLogin(this.txtEmail.Text, this.txtPassword.Text);
            if (estudiante != null)
            {
                Session["Cuenta"] = estudiante;
                Server.Transfer("IndexEstudiante.aspx");
            }
            ProfesoresImpt profesoresImpt = new ProfesoresImpt();
            Profesores profesores = profesoresImpt.listaLogin(this.txtEmail.Text, this.txtPassword.Text);
            if (profesores != null)
            {
                Session["Cuenta"] = profesores;
                if (profesores.Nivel == 0)
                {
                    Server.Transfer("IndexProfesores.aspx");
                }
                else { 
                    Server.Transfer("IndexAdmi.aspx");
                }
            }

            //Abajo iran otros if de profesores o admin

            //Si todos son nulos quiere decir que no existen, entonces mostramos un mensaje.
            Response.Write("<script>alert('" + "Credenciales invalidas" + "');</script>");
        }
    }
}