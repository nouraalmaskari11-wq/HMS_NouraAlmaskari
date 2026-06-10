using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public static class EmailService
    {
        public static string SystemEmail { get; set; } = "hms@grandcodeline.om";
        public static void SendEmail(string to, string subject, string body) 
        {
            Console.WriteLine($"From: {SystemEmail}");
            Console.WriteLine($"To:{ to}");
            Console.WriteLine($"subject: {subject}");
            Console.WriteLine($"Body: { body}");
            Console.WriteLine("");
            Console.WriteLine("Email sent successfully!");
        }
    }
}
