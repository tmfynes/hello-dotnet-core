# hello-dotnet-core

ASP.NET Core 8 hello-world for **homelab1.fynesy.com** Linux Diego cells (`cflinuxfs4` + `dotnet_core_buildpack`).

Companion to [hello-windows](https://hello-windows.homelab1.fynesy.com) on the Windows stemcell.

```bash
cf target -o hermes -s hermes
cf push -f manifest.yml -p .
```

Route: https://hello-dotnet-core.homelab1.fynesy.com

Binds `0.0.0.0:$PORT`.
