# Urlaubsmanagementsystem

> Interne Blazor-App aus meiner Ausbildung bei PICA GmbH. Mitarbeiter stellen Urlaubsanträge, Manager genehmigen oder lehnen sie ab.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor_Server-512BD4?logo=blazor&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-9-512BD4?logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Syncfusion](https://img.shields.io/badge/Syncfusion-Blazor-FF6D00)

---

## Was es macht

**Mitarbeiter** sehen auf ihrer Startseite einen Kalender mit ihren eigenen genehmigten Urlauben und können über einen Button neue Anträge stellen. Beim Anlegen wählt man Start- und Enddatum per Klick in einem Kalender, die Arbeitstage werden automatisch berechnet und Wochenenden sowie Feiertage abgezogen.

**Manager** haben ein eigenes Dashboard mit einem aggregierten Kalender über alle Mitarbeiter-Urlaube. Die Tage sind farbcodiert nach Auslastung: ab 3 Abwesenden wird der Tag orange, ab 5 rot. Über die Review-Seite werden offene Anträge einzeln per Approve- oder Decline-Button entschieden.

Damit nicht doppelt beantragt wird: wenn ein Mitarbeiter für denselben Zeitraum schon einen Antrag offen hat und noch mal beantragt, wird der bestehende aktualisiert statt dass ein zweiter in der DB landet.

## Screenshots

> *Screenshots werden ergänzt.*

<!--
Beispiel für später:
![Mitarbeiter-Dashboard]
![Manager-Kalender mit Farbcodierung]
![Antragsformular]
-->

## Tech

**Backend:** C# · ASP.NET Core · Blazor Server (.NET 9, Interactive Server Render Mode)
**Datenbank:** Microsoft SQL Server · Entity Framework Core 9
**UI:** Syncfusion.Blazor.Schedule (Manager-Dashboard) · QuickGrid · Bootstrap

## Datenmodell

| Entität        | Zweck                                                  |
| -------------- | ------------------------------------------------------ |
| `User`         | Mitarbeiter mit Rolle (Mitarbeiter oder Manager)       |
| `LeaveRequest` | Urlaubsantrag mit Start-/End-Datum und Entscheidungsstatus |
| `Decision`     | Lookup für Pending / Approved / Declined               |
| `Role`         | Lookup für Mitarbeiter / Manager                       |

## Projektstruktur

```
Urlaubsmanagementsystem/
├── Urlaubsmanagementsystem/
│   ├── Components/
│   │   ├── Layout/
│   │   └── Pages/           # Home, Dashboard, CreateRequest, ViewRequests, Users, …
│   ├── Data/
│   │   └── DatabaseConnection.cs
│   ├── Models/              # User, Role, LeaveRequest, Decision
│   ├── wwwroot/
│   └── Program.cs
└── Urlaubsmanagementsystem.sln
```

## Setup

**Voraussetzungen:** .NET 9 SDK · SQL Server (LocalDB reicht)

```bash
git clone https://github.com/Yamensko/Urlaubsmanagementsystem.git
cd Urlaubsmanagementsystem/Urlaubsmanagementsystem

# Connection-String in appsettings.json anpassen
dotnet ef database update
dotnet run
```

Die App ist unter `https://localhost:5001` erreichbar.

## Status

Funktioniert als MVP. Was noch kommen soll:

- [ ] Richtige Benutzer-Authentifizierung (ASP.NET Core Identity)
- [ ] E-Mail-Benachrichtigung bei Genehmigung oder Ablehnung
- [ ] Urlaubskonto mit automatischer Resturlaubsberechnung
- [ ] Halbtags-Anträge (Vor- oder Nachmittag)
- [ ] Antrag zurückziehen für Mitarbeiter

## Autor

Yamen Machnouk · GitHub [@Yamensko](https://github.com/Yamensko)

---

*Transparenzhinweis: Claude (Anthropic) habe ich fürs Repository-Cleanup und das hochladen auf geithub verwendet, deshalb taucht Claude auch als Co-Author in manchen Commits auf.*
