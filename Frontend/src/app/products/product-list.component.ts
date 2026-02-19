import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../core/services/product.service';
import { Product } from '../shared/models/product.model';
import { SearchBarComponent } from '../shared/components/search-bar/search-bar.component';
import { CategoryFilterComponent } from '../categories/components/category-filter.component';
import { ConfirmModalComponent } from '../shared/components/confirm-modal/confirm-modal.component';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-product-list',
  imports: [CommonModule, SearchBarComponent, CategoryFilterComponent, ConfirmModalComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  loading = false;
  error = '';
  search = '';
  categoryId = '';

  page = 1;
  pageSize = 10;
  totalItems = 0;

  showDeleteModal = false;
  productToDelete: string | null = null;

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    this.productService
      .getProducts(this.search, this.categoryId, this.page, this.pageSize)
      .subscribe({
        next: (response) => {
          this.products = response.items;
          this.totalItems = response.totalCount
          this.loading = false;
        },
        error: () => {
          this.error = 'Failed to load products';
          this.loading = false;
        }
      });
  }

  onSearch(value: string) {
    this.search = value;
    this.loadProducts();
  }

  onCategoryChange(categoryId: string) {
    this.categoryId = categoryId;
    this.loadProducts();
  }

  addProduct() {
    this.router.navigate(['/products/create']);
  }

  edit(id: string) {
    this.router.navigate(['/products/edit', id]);
  }

  openDeleteModal(id: string) {
    this.productToDelete = id;
    this.showDeleteModal = true;
  }

  confirmDelete() {
    if (!this.productToDelete) return;

    this.productService.delete(this.productToDelete).subscribe(() => {
      this.loadProducts();
      this.showDeleteModal = false;
      this.productToDelete = null;
    });
  }

  closeModal() {
    this.showDeleteModal = false;
    this.productToDelete = null;
  }

  previous() {
    if (this.page > 1) {
      this.page--;
      this.loadProducts();
    }
  }

  next() {
    if (this.page * this.pageSize < this.totalItems) {
      this.page++;
      this.loadProducts();
    }
  }
}
