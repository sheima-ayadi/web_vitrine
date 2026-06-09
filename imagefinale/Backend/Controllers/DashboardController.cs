using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfiniSoft.Admin.Data;
using InfiniSoft.Admin.Models;
using System.Net.Mail;
using System.Net;

namespace InfiniSoft.Admin.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DashboardController(AppDbContext context, IConfiguration configuration) : ControllerBase
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var visits = await context.SiteStatistics.CountAsync(s => s.Type == "Visit");
        var mails = await context.ContactMessages.CountAsync();
        var blogPosts = await context.BlogPosts.CountAsync();
        var services = await context.Services.CountAsync();

        var chartData = new {
            Visits = new[] { 120, 150, 180, 200, 220, 250, 300 },
            Mails = new[] { 5, 8, 12, 10, 15, 7, 20 },
            Services = new {
                Labels = new[] { "Cyber", "Dev", "Audit", "ERP" },
                Data = new[] { 40, 25, 15, 20 }
            }
        };

        return Ok(new {
            Visits = visits + 1250,
            Mails = mails,
            Posts = blogPosts,
            Services = services,
            ChartData = chartData
        });
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages() => Ok(await context.ContactMessages.OrderByDescending(m => m.CreatedAt).ToListAsync());

    [HttpPost("messages")]
    [AllowAnonymous]
    public async Task<IActionResult> PostMessage(ContactMessage msg)
    {
        Console.WriteLine($"Tentative d'enregistrement d'un message de : {msg.Name} ({msg.Email})");
        try 
        {
            context.ContactMessages.Add(msg);
            await context.SaveChangesAsync();
            Console.WriteLine("Message enregistré en base de données avec succès.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERREUR CRITIQUE DB :");
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null) Console.WriteLine($"Inner Exception : {ex.InnerException.Message}");
            return StatusCode(500, new { error = "Erreur base de données", details = ex.Message });
        }

        // Envoi de l'email en arrière-plan
        _ = Task.Run(async () => {
            try 
            {
                var emailSettings = configuration.GetSection("EmailSettings");
                var senderEmail = emailSettings["SenderEmail"];
                var senderPass = emailSettings["SenderPassword"]?.Replace(" ", ""); // Supprime les espaces du code Google
                
                Console.WriteLine($"Tentative SMTP avec : {emailSettings["SmtpServer"]}:{emailSettings["Port"]} pour {senderEmail}");
                
                if (string.IsNullOrEmpty(senderEmail) || senderEmail == "votre-email@gmail.com")
                {
                    Console.WriteLine("SMTP non configuré. L'email ne sera pas envoyé.");
                    return;
                }

                using var smtpClient = new SmtpClient(emailSettings["SmtpServer"])
                {
                    Port = int.Parse(emailSettings["Port"]!),
                    UseDefaultCredentials = false, // Obligatoire pour utiliser NetworkCredential ensuite
                    Credentials = new NetworkCredential(senderEmail, senderPass),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 15000 // 15 secondes
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail!, "Infini-Soft Website"),
                    Subject = $"[CONTACT] {msg.Subject}",
                    Body = $@"
                        <div style='font-family: sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
                            <h2 style='color: #0077ff;'>Nouveau message de contact</h2>
                            <p><strong>Nom :</strong> {msg.Name}</p>
                            <p><strong>Email :</strong> {msg.Email}</p>
                            <p><strong>Téléphone :</strong> {msg.Phone}</p>
                            <p><strong>Entreprise :</strong> {msg.Company}</p>
                            <hr>
                            <p><strong>Message :</strong></p>
                            <p style='background: #f9f9f9; padding: 15px; border-radius: 5px;'>{msg.Message.Replace("\n", "<br>")}</p>
                        </div>",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(emailSettings["ReceiverEmail"]!);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("✅ Email de contact envoyé avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERREUR SMTP : {ex.Message}");
                if (ex.InnerException != null) Console.WriteLine($"Inner Exception SMTP : {ex.InnerException.Message}");
            }
        });

        return Ok(new { message = "Message reçu !" });
    }

    [HttpGet("services")]
    public async Task<IActionResult> GetServices() => Ok(await context.Services.ToListAsync());

    [HttpPost("services")]
    public async Task<IActionResult> AddService(Service service)
    {
        context.Services.Add(service);
        await context.SaveChangesAsync();
        return Ok(service);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers() => Ok(await context.Users.Select(u => new { u.Id, u.Username }).ToListAsync());
}
