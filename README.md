# Daily_Grid_Angular_C-_Version

DailyGrid is a local-first productivity analytics app that tracks active screen time and basic input activity (keyboard/mouse) on Windows, then visualizes your usage patterns in a clean Angular dashboard. 

## Description
DailyGrid consists of:
- A **Windows telemetry collector** (C#/.NET) that captures system activity signals (e.g., keyboard keydown count, mouse click count, mouse movement distance) and aggregates them into time-sliced telemetry.
- A **local persistence layer** (SQLite) that stores daily records and summaries.
- A **web dashboard** (Angular) that renders charts and reports for daily/weekly review.


## Tech Stack
### Backend / Windows Collector
- **C# / .NET**
- **Win32 P/Invoke** (low-level keyboard/mouse hooks)
- **Threading + background runtime host** (HookManager to manage hook lifecycle)
- **SQLite** for local storage (implementation varies: raw SQL / Dapper / EF Core depending on setup)

### Frontend Dashboard
- **Angular**
- **TypeScript**
- **Charting** (e.g., Chart.js or similar, depending on chosen library)
- **REST API or local data bridge** (depending on backend hosting model)

## Key Features (MVP)
- Track **active time** in time slices (like 0.5s ticks)
- Track **keyboard keydown count**
- Track **mouse click count**
- Track **mouse movement distance** (px-based MVP)
- Daily and weekly summaries
- Charts (time-series + distribution views)

## Architecture (High-Level)
- **Core (platform-agnostic)**: defines telemetry contracts (`TelemetryTick`, `KeyboardSample`, `MouseSample`, `ScreenSample`) and aggregation logic.
- **Windows Platform Layer**: installs low-level hooks, captures raw events, converts them into minimal deltas, and emits `TelemetryTick`.
- **Storage Layer**: persists daily records and derived summaries into SQLite.
- **Angular UI**: visualizes records and trends.


### Prerequisites
- Windows 10/11
- .NET SDK (latest LTS recommended)
- Node.js + npm
- Angular CLI


## Privacy Note
DailyGrid is designed to be **local-first**. Telemetry is intended to stay on-device. Window titles and other sensitive context (if enabled) should be treated carefully and may be disabled or truncated by default.

## License

