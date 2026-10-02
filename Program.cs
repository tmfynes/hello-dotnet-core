using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
var app = builder.Build();

app.MapGet("/", () =>
{
    var html = $$"""
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <title>Hello from .NET on Linux Diego</title>
  <style>
    :root { color-scheme: dark; }
    body {
      margin: 0; min-height: 100vh;
      font-family: "Segoe UI", system-ui, sans-serif;
      background: #0b1f18;
      color: #eef6ef;
      display: grid; place-items: center;
    }
    main {
      max-width: 44rem; padding: 2.2rem;
      background: #123026; border-radius: 16px;
      box-shadow: 0 20px 50px rgba(0,0,0,.35);
      border-top: 6px solid #3ddc97;
    }
    h1 { margin: 0 0 .4rem; font-size: 1.8rem; }
    p { color: #c5dccf; line-height: 1.45; }
    a { color: #7ae0b0; }
    dl { display: grid; grid-template-columns: 9rem 1fr; gap: .45rem .8rem; margin: 1.2rem 0 0; }
    dt { color: #7ae0b0; font-size: .82rem; text-transform: uppercase; letter-spacing: .04em; }
    dd { margin: 0; font-family: ui-monospace, Consolas, monospace; }
    .ok { color: #3ddc97; font-weight: 600; }
  </style>
</head>
<body>
  <main>
    <h1>Hello, .NET Core on Linux</h1>
    <p class="ok">This process is running on a Cloud Foundry Linux Diego cell (<code>cflinuxfs4</code>).</p>
    <p>Companion to <a href="https://hello-windows.homelab1.fynesy.com">hello-windows</a> (Windows stemcell). Same ASP.NET Core 8 / Kestrel, different cell OS.</p>
    <dl>
      <dt>OS</dt><dd>{{ Environment.OSVersion }}</dd>
      <dt>Description</dt><dd>{{ RuntimeInformation.OSDescription }}</dd>
      <dt>Architecture</dt><dd>{{ RuntimeInformation.OSArchitecture }} / {{ RuntimeInformation.ProcessArchitecture }}</dd>
      <dt>Framework</dt><dd>{{ RuntimeInformation.FrameworkDescription }}</dd>
      <dt>Machine</dt><dd>{{ Environment.MachineName }}</dd>
      <dt>CF instance</dt><dd>{{ Environment.GetEnvironmentVariable("CF_INSTANCE_INDEX") ?? "n/a" }} @ {{ Environment.GetEnvironmentVariable("CF_INSTANCE_IP") ?? "n/a" }}</dd>
      <dt>PORT</dt><dd>{{ port }}</dd>
      <dt>Time (UTC)</dt><dd>{{ DateTime.UtcNow.ToString("O") }}</dd>
    </dl>
  </main>
</body>
</html>
""";
    return Results.Content(html, "text/html");
});

app.MapGet("/healthz", () => Results.Json(new
{
    ok = true,
    os = RuntimeInformation.OSDescription,
    windows = OperatingSystem.IsWindows(),
    linux = OperatingSystem.IsLinux(),
}));

app.Run();
