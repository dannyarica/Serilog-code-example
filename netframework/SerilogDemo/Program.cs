using Serilog;
using SeriLogDemo;
using System;

namespace SerilogDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var log = new LoggerConfiguration()
                            .Enrich.WithProperty("Environment", "Development")
                            .Enrich.WithProperty("Version", "1.0.0")
                            .Enrich.WithProperty("Application", "CajaTrujillo-App")
                            .MinimumLevel.Debug()
                            .WriteTo.Seq("http://localhost:5341")
                            .WriteTo.File("log-.txt",
                                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                            rollingInterval: RollingInterval.Day)
                            .CreateLogger())
            {
                //log.Information("Ah, there you are!");

                var account = new Account
                {
                    AccountId = 2706,
                    Identifier = Guid.NewGuid().ToString(),
                    Name = "Luis Fonsi",
                    Phone = "+51944589632"
                };

                //log.Debug("This is the Account object => {@account}", account);
                
                log.Error("An error was found in Account {@account}", account, new Exception("no content file"));
            }
            Console.ReadKey();
        }
    }
}