import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../core/services/category.service';
import { Category } from '../../shared/models/category.model';

@Component({
  standalone: true,
  selector: 'app-category-filter',
  imports: [CommonModule],
  templateUrl: './category-filter.component.html',
  styleUrl: './category-filter.component.scss'
})
export class CategoryFilterComponent implements OnInit {

  @Output() categorySelected = new EventEmitter<string>();

  categories: Category[] = [];

  constructor(private service: CategoryService) {}

  ngOnInit() {
    this.service.getTree().subscribe(data => {
      this.categories = data;
      console.log('ngOnInit()', this.categories);
    });
  }

  onChange(event: any) {
    this.categorySelected.emit(event.target.value);
  }
}
