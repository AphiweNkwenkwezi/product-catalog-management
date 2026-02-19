import { Component, EventEmitter, Output, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject, takeUntil } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-search-bar',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.scss'
})
export class SearchBarComponent implements OnDestroy {

  @Output() search = new EventEmitter<string>();

  control = new FormControl('');

  private destroy$ = new Subject<void>();

  constructor() {
    this.control.valueChanges
      .pipe(
        debounceTime(400),          // Wait 400ms after typing stops
        distinctUntilChanged(),     // Only emit if value changed
        takeUntil(this.destroy$)
      )
      .subscribe(value => {
        this.search.emit(value?.trim() ?? '');
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
