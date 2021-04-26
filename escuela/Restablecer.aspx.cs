using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace escuela
{
    public partial class Restablecer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarCode_Click(object sender, EventArgs e)
        {
            EstudianteImp estudianteImp = new EstudianteImp();
            Estudiante estudiante = estudianteImp.listaLogin(this.txtEmailPerdido.Text);
            if (estudiante != null)
            {
                Session["Cuenta"] = estudiante;
                CodeGenerate cg = new CodeGenerate();
                string code = cg.generarCode(8);
                Session["codeG"] = code;
                enviarCorreo(estudiante.Email,code,estudiante.Nombre+" "+estudiante.Apellido);
                Server.Transfer("Restablecer_codigo.aspx");
            }
            //Abajo iran otros if de profesores o admin
            ProfesoresImpt profesoresImpt = new ProfesoresImpt();
            if (string.IsNullOrEmpty(this.txtEmailPerdido.Text.Trim()))
            {
                Response.Write("<script>alert('" + "Campo Email vacio" + "');</script>");
                return;
            }
            Profesores profesores = profesoresImpt.listaLogin(this.txtEmailPerdido.Text.Trim());
            if (profesores != null)
            {
                Session["Cuenta"] = profesores;
                CodeGenerate cg = new CodeGenerate();
                string code = cg.generarCode(8);
                Session["codeG"] = code;
                enviarCorreo(profesores.Email, code, profesores.Nombre + " " + profesores.Apellido);
                Server.Transfer("Restablecer_codigo.aspx");
            }

            //Si todos son nulos quiere decir que no existen, entonces mostramos un mensaje.
            Response.Write("<script>alert('" + "Email no encontrado" + "');</script>");
        }

        public void enviarCorreo(string receptor,string code, string nameReceptor) {
            string cEmisor = "snotas93@gmail.com";
            string passEmisor = "01SistemaNotas";
            //const string quote = "\"";
            string body = "<body><h1>Restablece tú contraseña</h1><h3>¡No te quedes sin acceso " + nameReceptor + "!</h3><br><br><span>Código: "+code+ "</span><br><br><p>Si no quieres cambiar la contraseña solo ignora este mensaje.</p></body>";
            //string body = "<body>" +
            //    "< h1 style = "+quote+"background-color: #FF8772; padding: 10%; color: white; text-align: center;"+quote+"> Restablece tú contraseña</ h1 >" +
            //    "< h3 style = "+quote+"font-weight: bold; margin-top: 5%;" +quote+">¡No te quedes sin acceso "+nameReceptor+"!</ h3 >              " +
            //    "< br >      " +
            //    "< br >" +
            //    "< span > Código: < span style = "+quote+"padding: 1%; background-color: #F8F9FA; font-weight: bold; box-shadow: 0px 0px 5px 0px;"+quote+" >"+code+"</ span ></ span >" +
            //    "< br >" +
            //    " < br >" +
            //    "< p > Si no quieres cambiar la contraseña solo ignora este mensaje.</ p >" +
            //    "< br >" +
            //    " < p > Te deseamos un feliz día de parte de < span style = "+quote+" font-weight: bold;"+quote+" > BALR </ span ></ p >" +
            //    "< br >< small style = "+quote+"font-weight: bold; text-align: center;"+quote+"> Santa Ana El Salvador </ small >" +
            //    "</ body > "; 
            
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential(cEmisor, passEmisor);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            //smtp.UseDefaultCredentials = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(cEmisor,"Sistema de Notas");
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = "Restablecer contraseña";
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtp.Send(mail);
        }
    }
}