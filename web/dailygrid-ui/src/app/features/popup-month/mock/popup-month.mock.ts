import { PopupMonthData } from '../../../models/activity.model';

export const POPUP_MONTH_MOCK: PopupMonthData = {
  monthLabel: '2026-02',
  generatedAt: new Date().toISOString(),
  cells: [
    // Week 1
    { date: '2026-01-26', dayOfMonth: 26, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-01-27', dayOfMonth: 27, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-01-28', dayOfMonth: 28, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-01-29', dayOfMonth: 29, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-01-30', dayOfMonth: 30, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-01-31', dayOfMonth: 31, inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-02-01', dayOfMonth: 1,  inCurrentMonth: true,  level: 1, score: 28 },

    // Week 2
    { date: '2026-02-02', dayOfMonth: 2,  inCurrentMonth: true,  level: 2, score: 46 },
    { date: '2026-02-03', dayOfMonth: 3,  inCurrentMonth: true,  level: 3, score: 63 },
    { date: '2026-02-04', dayOfMonth: 4,  inCurrentMonth: true,  level: 1, score: 30 },
    { date: '2026-02-05', dayOfMonth: 5,  inCurrentMonth: true,  level: 4, score: 88 },
    { date: '2026-02-06', dayOfMonth: 6,  inCurrentMonth: true,  level: 3, score: 67 },
    { date: '2026-02-07', dayOfMonth: 7,  inCurrentMonth: true,  level: 2, score: 58 },
    { date: '2026-02-08', dayOfMonth: 8,  inCurrentMonth: true,  level: 1, score: 33 },

    // Week 3
    { date: '2026-02-09', dayOfMonth: 9,  inCurrentMonth: true,  level: 4, score: 84 },
    { date: '2026-02-10', dayOfMonth: 10, inCurrentMonth: true,  level: 3, score: 75 },
    { date: '2026-02-11', dayOfMonth: 11, inCurrentMonth: true,  level: 2, score: 49 },
    { date: '2026-02-12', dayOfMonth: 12, inCurrentMonth: true,  level: 3, score: 62 },
    { date: '2026-02-13', dayOfMonth: 13, inCurrentMonth: true,  level: 2, score: 44 },
    { date: '2026-02-14', dayOfMonth: 14, inCurrentMonth: true,  level: 1, score: 22 },
    { date: '2026-02-15', dayOfMonth: 15, inCurrentMonth: true,  level: 0, score: 10 },

    // Week 4
    { date: '2026-02-16', dayOfMonth: 16, inCurrentMonth: true,  level: 2, score: 40 },
    { date: '2026-02-17', dayOfMonth: 17, inCurrentMonth: true,  level: 3, score: 68 },
    { date: '2026-02-18', dayOfMonth: 18, inCurrentMonth: true,  level: 4, score: 90 },
    { date: '2026-02-19', dayOfMonth: 19, inCurrentMonth: true,  level: 3, score: 70 },
    { date: '2026-02-20', dayOfMonth: 20, inCurrentMonth: true,  level: 2, score: 51 },
    { date: '2026-02-21', dayOfMonth: 21, inCurrentMonth: true,  level: 1, score: 25 },
    { date: '2026-02-22', dayOfMonth: 22, inCurrentMonth: true,  level: 1, score: 27 },

    // Week 5
    { date: '2026-02-23', dayOfMonth: 23, inCurrentMonth: true,  level: 3, score: 73 },
    { date: '2026-02-24', dayOfMonth: 24, inCurrentMonth: true,  level: 2, score: 57 },
    { date: '2026-02-25', dayOfMonth: 25, inCurrentMonth: true,  level: 1, score: 36 },
    { date: '2026-02-26', dayOfMonth: 26, inCurrentMonth: true,  level: 2, score: 48 },
    { date: '2026-02-27', dayOfMonth: 27, inCurrentMonth: true,  level: 4, score: 82 },
    { date: '2026-02-28', dayOfMonth: 28, inCurrentMonth: true,  level: 3, score: 64 },
    { date: '2026-03-01', dayOfMonth: 1,  inCurrentMonth: false, level: 0, score: 0 },

    // Week 6
    { date: '2026-03-02', dayOfMonth: 2,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-03', dayOfMonth: 3,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-04', dayOfMonth: 4,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-05', dayOfMonth: 5,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-06', dayOfMonth: 6,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-07', dayOfMonth: 7,  inCurrentMonth: false, level: 0, score: 0 },
    { date: '2026-03-08', dayOfMonth: 8,  inCurrentMonth: false, level: 0, score: 0 }
  ],
  topApps: [
    { name: 'IntelliJ IDEA', minutes: 612 },
    { name: 'Chrome', minutes: 488 },
    { name: 'VS Code', minutes: 401 },
    { name: 'Windows Terminal', minutes: 268 },
    { name: 'Discord', minutes: 156 }
  ]
};
