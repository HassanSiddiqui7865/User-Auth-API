using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace backend.Services
{
    public class EmailSendService
    {
        public  Object SendEmail(string senderEmail,string senderName,string recieverName
            ,string recieverEmail,string subject,string message)
        {
            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderEmail);
            SendSmtpEmailTo reciever = new SendSmtpEmailTo(recieverEmail, recieverName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(reciever);
            string HtmlContent = null;
            string TextContent = message;
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender , To, null, null, HtmlContent, TextContent, subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                return result.ToJson();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
