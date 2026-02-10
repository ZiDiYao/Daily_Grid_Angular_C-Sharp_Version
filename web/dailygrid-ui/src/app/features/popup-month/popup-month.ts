import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { PopupMonthData, DayCell } from '../../models/activity.model';
import { POPUP_MONTH_MOCK } from './mock/popup-month.mock';

@Component({
  selector: 'app-popup-month',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './popup-month.html',
  styleUrls: ['./popup-month.css'] 
})
export class PopupMonth {
  data: PopupMonthData = POPUP_MONTH_MOCK;
  weekLabels = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
  selectedCell: DayCell | null = null;

  refreshMock(): void {
    this.data = {
      ...this.data,
      generatedAt: new Date().toISOString(),
      cells: this.data.cells.map((c) => {
        if (!c.inCurrentMonth) return c;
        const delta = Math.floor(Math.random() * 11) - 5;
        const score = Math.max(0, Math.min(100, c.score + delta));
        return { ...c, score, level: this.toLevel(score) };
      })
    };

    if (this.selectedCell) {
      this.selectedCell = this.data.cells.find(x => x.date === this.selectedCell!.date) ?? null;
    }
  }

  onCellClick(cell: DayCell): void {
    if (!cell.inCurrentMonth) return;
    this.selectedCell = this.selectedCell?.date === cell.date ? null : cell;
  }

  onCellKeydown(event: KeyboardEvent, cell: DayCell): void {
    if (event.key === 'Enter' || event.key === ' ') {
      event.preventDefault();
      this.onCellClick(cell);
    }
  }

  trackByDate(_: number, item: { date: string }): string {
    return item.date;
  }

  getUsagePercent(minutes: number): number {
    const max = Math.max(...this.data.topApps.map(a => a.minutes), 1);
    return Math.round((minutes / max) * 100);
  }

  private toLevel(score: number): 0 | 1 | 2 | 3 | 4 {
    if (score >= 80) return 4;
    if (score >= 60) return 3;
    if (score >= 40) return 2;
    if (score >= 20) return 1;
    return 0;
  }
}
