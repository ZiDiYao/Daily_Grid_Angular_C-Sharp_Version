import { Component, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './todo-form.html',
  styleUrl: './todo-form.css',
})
export class TodoFormComponent {
  add = output<string>();
  text = '';

  submit() {
    const t = this.text.trim();
    if (!t) return;
    this.add.emit(t);
    this.text = '';
  }
}
