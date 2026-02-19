import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../../shared/models/product.model';
import { API_CONFIG } from '../api.config';
import { BaseApiService } from './base-api.service';
import { PagedResult } from '../../shared/models/paged-result.model';

@Injectable({ providedIn: 'root' })
export class ProductService extends BaseApiService {

  private endpoint = `${API_CONFIG.baseUrl}/products`;

  constructor(http: HttpClient) {
    super(http);
  }

  getProducts(
    search?: string,
    categoryId?: string,
    page: number = 1,
    pageSize: number = 10
  ): Observable<PagedResult<Product>> {

    const params: any = {
      page,
      pageSize
    };

    if (search) params.search = search;
    if (categoryId) params.categoryId = categoryId;

    return this.getRequest<PagedResult<Product>>(
      this.endpoint,
      { params }
    );
  }

  getById(id: string): Observable<Product> {
    return this.getRequest<Product>(`${this.endpoint}/${id}`);
  }

  create(product: Partial<Product>): Observable<any> {
    return this.postRequest<any>(this.endpoint, product);
  }

  update(id: string, product: Partial<Product>): Observable<any> {
    return this.putRequest<any>(`${this.endpoint}/${id}`, product);
  }

  delete(id: string): Observable<any> {
    return this.deleteRequest<any>(`${this.endpoint}/${id}`);
  }
}
