import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../../../shared/models/category.model';
import { of, switchMap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-form.component.html',
  styleUrl: './category-form.component.scss'
})
export class CategoryFormComponent implements OnInit {

  isEdit = false;
  id: string | null = null;
  loading = false;
  error = '';

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private service: CategoryService,
    private route: ActivatedRoute,
    public router: Router
  ) {
      this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      parentId: [null]
    });
  }

  categories: Category[] = [];

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.isEdit = !!this.id;

    this.service.getAll()
      .pipe(
        switchMap(categories => {

          if (!this.isEdit || !this.id) {
            this.categories = categories;
            return of(null);
          }

          // If editing, also fetch descendants
          return this.service.getDescendantIds(this.id)
            .pipe(
              switchMap(descendants => {

                this.categories = categories.filter(c =>
                  c.id !== this.id &&
                  !descendants.includes(c.id)
                );

                return this.service.getById(this.id!);
              })
            );
        })
      )
      .subscribe({
        next: (category) => {
          if (category) {
            this.form.patchValue({
              name: category.name,
              description: category.description,
              parentId: category.parentCategoryId ?? null
            });
          }
        },
        error: () => this.error = 'Failed to load data'
      });
  }

  private loadCategories(): void {

    this.service.getAll()
      .pipe(
        switchMap(categories => {

          if (!this.isEdit || !this.id) {
            return of({ categories, descendants: [] });
          }

          return this.service.getDescendantIds(this.id)
            .pipe(
              switchMap(descendants =>
                of({ categories, descendants })
              )
            );
        })
      )
      .subscribe({
        next: ({ categories, descendants }) => {

          this.categories = categories.filter(c =>
            c.id !== this.id &&
            !descendants.includes(c.id)
          );

        },
        error: () => this.error = 'Failed to load categories'
      });
  }

  submit() {
    if (this.form.invalid) return;

    this.loading = true;

    const request = this.isEdit
      ? this.service.update(this.id!, this.form.value)
      : this.service.create(this.form.value);

    request.subscribe({
      next: () => this.router.navigate(['/categories']),
      error: () => {
        this.error = 'Save failed';
        this.loading = false;
      }
    });
  }
}
