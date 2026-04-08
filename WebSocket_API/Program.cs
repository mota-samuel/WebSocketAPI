using OBSWebsocketDotNet;
using WebSocket_Infrastructure;
using WebSocket_Application;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddObsService(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

string token = "123abc";

var obs = new OBSWebsocket();

void ConnectObs()
{
    try
    {
        if (!obs.IsConnected)
        {
            obs.Connect(url, password);
            Console.WriteLine("Conectado ao OBS");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao conectar no OBS: {ex.Message}");
    }
}

// conecta ao iniciar
ConnectObs();


// 🌐 PAINEL WEB
app.MapGet("/", async context =>
{
    var html = @"<!DOCTYPE html>
<html lang='pt-br'>
<head>
<meta charset='UTF-8'>
<title>Painel OBS</title>
<meta name='viewport' content='width=device-width, initial-scale=1'>
<style>
body { font-family: Arial; text-align: center; margin-top: 40px; background: #0f172a; color: white; }
button { width: 80%; padding: 20px; margin: 10px; font-size: 18px; border-radius: 12px; border: none; cursor: pointer; }
.start { background-color: #22c55e;color: white }
.stop { background-color: #ef4444;color: white }
.scene { background-color: #3b82f6;color: white }
.badge { padding: 6px 12px; border-radius: 20px; display: inline-block; margin-top: 10px; }
.on { background: #22c55e; }
.off { background: #ef4444; }
.warn { background: #f59e0b; }
</style>
</head>
<body>

<h1>🎬 Painel OBS</h1>

<button class='start' onclick='start()'>▶ Iniciar</button>
<button class='stop' onclick='stop()'>■ Parar</button>
<button class='scene' onclick='scene(""Cena"")'>🎥 Câmera</button>

<div>
Status:
<div id='statusBadge' class='badge off'>Carregando...</div>
</div>

<script>
const token = '123abc';

async function updateStatus() {
    try {
        const res = await fetch('/status');
        const data = await res.json();

        const badge = document.getElementById('statusBadge');

        if (!data.connected) {
            badge.innerText = 'OBS OFFLINE';
            badge.className = 'badge warn';
            return;
        }

        if (data.recording) {
            badge.innerText = 'Gravando';
            badge.className = 'badge on';
        } else {
            badge.innerText = 'Parado';
            badge.className = 'badge off';
        }

    } catch {
        const badge = document.getElementById('statusBadge');
        badge.innerText = 'Erro conexão';
        badge.className = 'badge warn';
    }
}

function start() {
    fetch('/start?t=' + token).then(updateStatus);
}

function stop() {
    fetch('/stop?t=' + token).then(updateStatus);
}

function scene(name) {
    fetch('/scene?name=' + name + '&t=' + token);
}

setInterval(updateStatus, 2000);
updateStatus();
</script>

</body>
</html>";

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(html);
});


// 📊 STATUS
app.MapGet("/status", () =>
{
    ConnectObs();

    if (!obs.IsConnected)
        return Results.Ok(new { connected = false });

    var status = obs.GetRecordStatus();

    return Results.Ok(new
    {
        connected = true,
        recording = status.IsRecording
    });
});


// 🎥 TROCAR CENA
app.MapGet("/scene", (string name, string t) =>
{
    if (t != token) return Results.Unauthorized();

    ConnectObs();

    if (!obs.IsConnected)
        return Results.Problem("OBS não conectado");

    obs.SetCurrentProgramScene(name);

    return Results.Ok($"Cena alterada: {name}");
});


// ▶ START
app.MapGet("/start", (string t) =>
{
    if (t != token) return Results.Unauthorized();

    ConnectObs();

    if (!obs.IsConnected)
        return Results.Problem("OBS não conectado");

    var status = obs.GetRecordStatus();

    if (status.IsRecording)
        return Results.Ok("Já está gravando");

    obs.StartRecord();

    return Results.Ok("Gravação iniciada");
});


// ⏹ STOP
app.MapGet("/stop", (string t) =>
{
    if (t != token) return Results.Unauthorized();

    ConnectObs();

    if (!obs.IsConnected)
        return Results.Problem("OBS não conectado");

    var status = obs.GetRecordStatus();

    if (!status.IsRecording)
        return Results.Ok("Já está parado");

    obs.StopRecord();

    return Results.Ok("Gravação parada");
});


app.Run();