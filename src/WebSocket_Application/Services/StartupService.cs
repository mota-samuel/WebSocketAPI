using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocket_Application.Services;
public class StartupService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (Process.GetProcessesByName("obs64").Length == 0)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"C:\Program Files\obs-studio\bin\64bit\obs64.exe",
                WorkingDirectory = @"C:\Program Files\obs-studio\bin\64bit",
                UseShellExecute = true
            });
        }

        // espera até OBS estar aberto
        int tentativas = 0;

        while (Process.GetProcessesByName("obs64").Length == 0 && tentativas < 10)
        {
            await Task.Delay(1000, cancellationToken);
            tentativas++;
            Console.WriteLine(tentativas);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
