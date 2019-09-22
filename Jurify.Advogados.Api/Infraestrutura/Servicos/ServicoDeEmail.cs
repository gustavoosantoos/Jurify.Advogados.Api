using Jurify.Advogados.Api.Dominio.Servicos;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Servicos
{
    public class ServicoDeEmail : IServicoDeEmail
    {
        private readonly IConfiguration _configuration;

        public ServicoDeEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarEmail(string remetente, string senha, string destinatario, string assunto, string conteudo)
        {
            try
            {
                NetworkCredential credential = new NetworkCredential(remetente, senha);

                SmtpClient client = new SmtpClient(_configuration["Email:Smtp"])
                {
                    Port = Convert.ToInt32(_configuration["Email:Port"]),
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = credential
                };

                using (client)
                {
                    using (var message = new MailMessage(remetente, destinatario))
                    {
                        message.Subject = assunto;
                        message.Body = conteudo;
                        message.IsBodyHtml = true;

                        await client.SendMailAsync(message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
