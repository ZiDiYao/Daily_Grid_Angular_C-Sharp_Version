export interface DayCell {
  date: string; // YYYY-MM-DD
  dayOfMonth: number;
  inCurrentMonth: boolean;
  level: 0 | 1 | 2 | 3 | 4; // activity level
  score: number; // 0~100
}

export interface TopAppItem {
  name: string;
  minutes: number;
}

export interface PopupMonthData {
  monthLabel: string;      // e.g. 2026-02
  generatedAt: string;     // ISO datetime
  cells: DayCell[];        // 42 cells (6 weeks * 7 days)
  topApps: TopAppItem[];   // top 5
}
