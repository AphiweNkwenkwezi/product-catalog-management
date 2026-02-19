import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './layout/admin-layout.component';
import { ProductListComponent } from './products/product-list.component';
import { ProductFormComponent } from './products/product-form.component';

export const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [

      {
        path: 'products',
        children: [
          { path: '', component: ProductListComponent },
          { path: 'create', component: ProductFormComponent },
          { path: 'edit/:id', component: ProductFormComponent }
        ]
      },

      {
        path: 'categories',
        loadChildren: () =>
          import('./categories/category.routes')
            .then(m => m.CATEGORY_ROUTES)
      },

      { path: '', redirectTo: 'products', pathMatch: 'full' }
    ]
  },

  { path: '**', redirectTo: '' }
];
