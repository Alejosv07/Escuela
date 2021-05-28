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
                enviarCorreo(estudiante.Email, code, estudiante.Nombre + " " + estudiante.Apellido);
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

        public void enviarCorreo(string receptor, string code, string nameReceptor)
        {
            string cEmisor = "snotas93@gmail.com";
            string passEmisor = "01SistemaNotas";
            //const string quote = "\"";
            string body = "<div style=\"margin:0;padding:0;background:#e8ecee;font-family:&quot;Helvetica Neue&quot;,Helvetica,arial,sans-serif;background-color:#f4f4f7;width:100%!important;min-height:600px!important\">" +
                "<table align=\"center\" style =\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px\">" +
                "<tbody>" +
                "<tr>" +
                "<td style=\"border-collapse:collapse\">" +
                "<table align=\"center\" style =\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px\">" +
                "<tbody width=\"600\">" +
                "<tr>" +
                "<td align=\"center\" style =\"border-collapse:collapse;padding-right:35px;padding-top:15px;padding-bottom:15px\">" +
                "<h4 style=\"color: #06151e; padding-top: 2%;\"><strong>Colegio Santa Ana</strong></h4>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"center\" cellpadding =\"0\" cellspacing =\"0\" border =\"0\" style =\"border-collapse:collapse;background-color:#06151e;width:600px;height:8px;border-top-right-radius:8px;border-top-left-radius:8px;max-width:600px\">" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td width=\"600\" style =\"border-collapse:collapse;padding:34px 48px 0px 48px;background-color:#ffffff;border-bottom-left-radius:8px;border-bottom-right-radius:8px\" align =\"center\">" +
                "<table style=\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px\">" +
                "<tbody>" +
                "<tr>" +
                "<td style=\"border-collapse:collapse\">"+nameReceptor+".: Por favor, <strong>Restablece Tu Contraseña</strong><br>" +
                "<ul style=\"list-style-type:none;margin-left:15px;padding:0\">" +
                "<li>1. Copia el código enviado con este correo</li>" +
                "<li style=\"padding-bottom: 1rem; padding-top: 1rem; padding-left: 1rem; padding-right: 1rem; border: black 1px; background-color: #e8ecee; width: 140px; margin-top: 1.5rem; margin-bottom: 1.5rem;\"><strong style=\"border-right: white;\">Código: </strong><span>" + code + "</span></li>" +
                "<li>2. Pega el código y verificalo para poder cambiar tú contraseña</li>" +
                "</ul>" +
                "<br><br>" +
                "<p style=\"margin:0px 0px!important\">¿No solicitaste una nueva contraseña? Puedes ignorar este correo electrónico: tu cuenta está segura. Para obtener más información sobre tu cuenta de <strong>Colegio Santa Ana</strong> y encontrar ayuda 24 / 7, escribemenos a nuestro<strong> <a href =\"mailto:snotas93@gmail.com\" style =\"color:#24c7ff\">corrreo electrónico.</a></strong></p>" +
                "</td>" +
                "</tr>" +
                "<tr width=\"100%\" height =\"42px\">" +
                "<td style=\"border-collapse:collapse \"></td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"center\" cellpadding =\"0\" cellspacing =\"0\" border =\"0\" style =\"border-collapse:collapse;background-color:#06151e;width:600px;height:8px;border-bottom-right-radius:8px;border-bottom-left-radius:8px;max-width:600px\">" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td width=\"100%\" style =\"border-collapse:collapse \">" +
                "<table style=\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px;color:#333;font-size:11px\" width =\"100%\" cellpadding =\"0\" cellspacing =\"0\" border=\"0\" align =\"center \">" +
                "<tbody>" +
                "<tr>" +
                "<td width=\"100%\" height =\"15\" colspan =\"3\" style =\"border-collapse:collapse \"></td>" +
                "</tr>" +
                "<tr>" +
                "<td width=\"8.3%\" height =\"100%\" style =\"border-collapse:collapse \"></td>" +
                "<td width=\"3.83%\" style =\"border-collapse:collapse \">" +
                "<table align=\"left\" style =\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px \">" +
                "<tbody>" +
                "<tr>" +
                "<td valign=\"top\" style =\"border-collapse:collapse;width:22.93px;height:37.98px \">" +
                "<img src=\"https://i.ibb.co/8d7VTQb/Logo.png\" style =\"width: 54.39; height: 47.98;\">" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "<td width=\"3%\" height =\"100%\" style =\"border-collapse:collapse \"></td>" +
                "<td style=\"border-collapse:collapse \">" +
                "<table style=\"border-collapse:collapse;border:1px;border-color:#e8ecee;border-radius:0px 0px 16px 16px;width:100%;max-width:632px \">" +
                "<tbody>" +
                "<tr>" +
                "<td width=\"77.33%\" style =\"border-collapse:collapse \"></td>" +
                "</tr>" +
                "<tr>" +
                "<td style=\"border-collapse:collapse;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px\">" +
                "<a href=\"#\" style =\"text-decoration:none;color:#24c7ff;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px\">Términos de Servicio</a> |" +
                "<a href=\"#\" style =\"text-decoration:none;color:#24c7ff;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px\">Privacidad</a> |" +
                "<a href=\"#\" style =\"text-decoration:none;color:#24c7ff;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px\">Darse de Baja</a> |" +
                "<a href=\"mailto: snotas93@gmail.com\" style =\"text-decoration:none;color:#24c7ff;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px\">Soporte</a>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td valign=\"bottom\" style =\"border-collapse:collapse;color:#abaeba;font-family:HelveticaNeue;font-size:12px;font-weight:400;line-height:16px \">" +
                "Colegio Santa Ana. | By pass a Metapán y carretera Antigua a San Salvador, Santa Ana El Salvador | Santa Ana, Santa Ana 02010" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "<td width=\"8.3%\" height =\"100%\" style =\"border-collapse:collapse \">" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "<td style=\"border-collapse:collapse \"></td>" +
                "</tr>" +
                "<tr>" +
                "<td width=\"600\" height =\"16\" style =\"border-collapse:collapse \"></td>" +
                "</tr>" +
                "<tr>" +
                "<td style=\"border-collapse:collapse;width:100%!important;height:50px!important \"></td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</div>" +
                "</div>";
            //string body = "<body><h1 style=\"background-color: yellow;\">Restablece tú contraseña</h1><h3>¡No te quedes sin acceso " + nameReceptor + "!</h3><br><br><span>Código: " + code + "</span><br><br><p>Si no quieres cambiar la contraseña solo ignora este mensaje.</p></body>";
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
            mail.From = new MailAddress(cEmisor, "Sistema de Notas");
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = "Restablecer contraseña";
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtp.Send(mail);
        }
    }
}