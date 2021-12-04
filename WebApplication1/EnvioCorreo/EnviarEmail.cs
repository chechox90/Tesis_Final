using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ConductorEnRed.EnvioCorreo
{
    public class EnviarEmail
    {
        SmtpClient cliente;
        MailMessage mm;
        String usuario;
        String contrasena;
        String servidor;
        int puerto;
        const String cuerpoHtml1 = "";
        const String cuerpoHtml2 = "";
        public EnviarEmail()
        {
            usuario = "noreplyconductorenred@gmail.com";
            contrasena = "ConductorEnRed2021#";
            servidor = "smtp.gmail.com";
            puerto = 587;
            cliente = new SmtpClient();
            cliente.Port = puerto;
            cliente.Host = servidor;
            cliente.EnableSsl = true;
            cliente.Timeout = 10000;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;
            cliente.Credentials = new System.Net.NetworkCredential(usuario, contrasena);
        }
        public Boolean enviarEmail(String destinatario, String asunto, String mensaje)
        {
            String cuerpoCompleto = "";

            cuerpoCompleto = cuerpoHtml1 + "</br></br/><p>" + mensaje + " " + "</p><br/><br/>" +cuerpoHtml2;
            mm = new MailMessage(usuario, destinatario, asunto, cuerpoCompleto);
            mm.IsBodyHtml = true;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            cliente.Send(mm);
            return true;
        }
    }
}