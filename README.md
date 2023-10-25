# Project-RMT
**Gruppe 1**: Jonathan Krottenthaler, Paul Seypt

Umsetzung der Projektaufgabe Routing Simulation Tool im Zuge von LF10 - LF12

## Anforderungen

- Installieren des .NET 7 SDKs: [Hier zum Download](https://dotnet.microsoft.com/en-us/download)

- Überprüfen, ob das SDK richtig installiert wurde:
```bash
dotnet --list-sdks
```

sollte zu folgenden Output führen (Minor Version des SDKs darf abweichen):
```bash
7.0.400 [Pfad zur Installation]
```


## Quick-Start Guide

- Ausführen des Projekts:

im Root-Verzeichnis des Repos folgendes ausführen:
```bash
dotnet run --project ./ProjectRMT.Core/Project_RMT.csproj
```

sollte zu etwa folgenden Output führen:
```bash
Buildvorgang wird ausgeführt...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5150
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
```
die Adresse aus dem ``Now listening on:`` herauskopieren und in einen Browser einfügen

- Ausführen des Unit-Test Projekts

im Root-Verzeichnis des Repos folgendes ausführen:
```bash
dotnet test ./Project_RMT.UnitTests/Project_RMT.UnitTests.csproj
```

