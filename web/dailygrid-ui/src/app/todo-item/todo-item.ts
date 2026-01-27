import { Component, input, output } from '@angular/core';
import { Todo } from '../models/todo.model';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.css',
})
export class TodoItemComponent {
  todo = input<Todo>({ id: 0, text: '', done: false });

  toggle = output<number>();
  remove = output<number>();

  onToggle() { this.toggle.emit(this.todo().id); }
  onRemove() { this.remove.emit(this.todo().id); }
}
