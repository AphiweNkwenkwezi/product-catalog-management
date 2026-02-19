import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../core/services/product.service';

@Component({
  standalone: true,
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {

  loading = false;
  error = '';
  isEdit = false;
  id: string | null = null;

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private service: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
      this.form = this.fb.group({
        name: ['', Validators.required],
        sku: ['', Validators.required],
        price: [0, [Validators.required, Validators.min(0)]],
        quantity: [0, [Validators.required, Validators.min(0)]],
        description: [''],
        categoryId: [null]
      });
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.isEdit = !!this.id;

    if (this.isEdit && this.id) {
      this.loading = true;

      this.service.getById(this.id).subscribe({
        next: product => {
          this.form.patchValue(product);
          this.loading = false;
        },
        error: () => {
          this.error = 'Failed to load product';
          this.loading = false;
        }
      });
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const request = this.isEdit
      ? this.service.update(this.id!, this.form.value)
      : this.service.create(this.form.value);

    request.subscribe({
      next: () => this.router.navigate(['/']),
      error: () => {
        this.error = 'Save failed';
        this.loading = false;
      }
    });
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
