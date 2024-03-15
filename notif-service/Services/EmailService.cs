using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace notif_service.Services
{
    public class EmailService:IEmailService
    {

        static readonly string senderAddress = "no-reply@stack-ensat.com";

        public async Task SendEmailType1 (string email, string message, string title,string action ) {
            string subject = "title";

            string textBody = "Amazon SES Test (.NET)\r\n"
                                            + "This email was sent through Amazon SES "
                                            + "using the AWS SDK for .NET.";

            string htmlBody = $@"<html>
<head></head>
<body>
  <td class=""esd-stripe"" align=""center"">
<table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" width=""600"">
 <tbody>
  <tr>
   <td class=""esd-structure es-p15t es-p20r es-p20l"" align=""left"">
    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">
     <tbody>
      <tr>
       <td width=""560"" class=""esd-container-frame"" align=""center"" valign=""top"">
        <table cellpadding=""0"" cellspacing=""0"" width=""100%"">
         <tbody>
          <tr>
           <td align=""center"" class=""esd-block-image es-p10t es-p10b"" style=""font-size: 0px;""><a target=""_blank""><img src=""https://fezfrpm.stripocdn.email/content/guids/CABINET_8625d407e854d8c155bdf950350ad580ff9e95c38c3754500f60d0c50a3adec1/images/image.png"" alt="""" style=""display:block"" width=""195"" class=""adapt-img""></a></td>
          </tr>
          <tr>
           <td align=""center"" class=""esd-block-text es-p15t es-p15b es-m-txt-c"" esd-links-underline=""none""><h1>{title}</h1></td>
          </tr>
          <tr>
           <td align=""left"" class=""esd-block-text es-p10t es-p10b""><p style=""font-size: 16px;"" align=""center"">{message}</p></td>
          </tr>
         </tbody>
        </table></td>
      </tr>
     </tbody>
    </table></td>
  </tr>
  
  <tr>
   <td class=""esd-structure es-p10b es-p20r es-p20l"" align=""left"">
    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">
     <tbody>
      <tr>
       <td width=""560"" class=""esd-container-frame"" align=""center"" valign=""top"">
        <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-radius: 5px; border-collapse: separate;"">
         <tbody>
          <tr>
           <td align=""center"" class=""esd-block-button es-p10t es-p10b""><span class=""es-button-border"" style=""border-radius:6px;background:#333333""><a href=""{action}"" class=""es-button"" target=""_blank"" style=""border-left-width:30px;border-right-width:30px;border-radius:6px;background:#333333;mso-border-alt:10px solid #333333"">Check Now</a></span></td>
          </tr>
          <tr>
           <td align=""left"" class=""esd-block-text es-p20t es-p10b""><p style=""line-height: 150%;"">Got a question? Email us at&nbsp;<a target=""_blank"" href=""mailto:"">support@stack.com</a>&nbsp;or give us a call at&nbsp;<a target=""_blank"" style=""line-height: 150%;"" href=""tel:"">+000 123 456</a>.</p><p><br>Thanks,</p><p>Stack Team!</p></td>
          </tr>
         </tbody>
        </table></td>
      </tr>
     </tbody>
    </table></td>
  </tr>
 </tbody>
</table></td>
</body>
</html>";

            using(var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.APEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = senderAddress,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { email }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = htmlBody
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = textBody
                            }
                        }
                    },
                };
                try
                {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var response = await client.SendEmailAsync(sendRequest);
                    Console.WriteLine("The email was sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);

                }
            }

        }
        
    }
}
