import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Todo } from './models/todo.model';
import { TodoFormComponent } from './todo-form/todo-form';
import { TodoItemComponent } from './todo-item/todo-item';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, TodoFormComponent, TodoItemComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  private nextId = 1;
  todos: Todo[] = [];

  onAdd(text: string) {
    this.todos = [...this.todos, { id: this.nextId++, text, done: false }];
  }

  onToggle(id: number) {
    this.todos = this.todos.map(t => t.id === id ? { ...t, done: !t.done } : t);
  }

  onRemove(id: number) {
    this.todos = this.todos.filter(t => t.id !== id);
  }

  trackById = (_: number, t: Todo) => t.id;
}
