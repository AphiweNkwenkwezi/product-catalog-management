import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../../../shared/models/category.model';
import { ConfirmModalComponent } from '../../../shared/components/confirm-modal/confirm-modal.component';

@Component({
  standalone: true,
  imports: [CommonModule, ConfirmModalComponent],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss'
})
export class CategoryListComponent implements OnInit {

  categories: Category[] = [];
  loading = false;
  error = '';

  showDeleteModal = false;
  categoryToDelete: string | null = null;

  constructor(
    private service: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.service.getAll().subscribe({
      next: (data) => {
        this.categories = data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load categories';
        this.loading = false;
      }
    });
  }

  add() {
    this.router.navigate(['/categories/create']);
  }

  edit(id: string) {
    this.router.navigate(['/categories/edit', id]);
  }

  openDelete(id: string) {
    this.categoryToDelete = id;
    this.showDeleteModal = true;
  }

  confirmDelete() {
    if (!this.categoryToDelete) return;

    this.service.delete(this.categoryToDelete).subscribe(() => {
      this.load();
      this.showDeleteModal = false;
    });
  }

  closeModal() {
    this.showDeleteModal = false;
  }
}
