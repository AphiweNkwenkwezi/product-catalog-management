import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { Category } from '../../shared/models/category.model';
import { API_CONFIG } from '../api.config';

@Injectable({ providedIn: 'root' })
export class CategoryService extends BaseApiService {

  private endpoint = `${API_CONFIG.baseUrl}/categories`;

  getAll(): Observable<Category[]> {
    return this.getRequest<Category[]>(this.endpoint);
  }

  getTree(): Observable<Category[]> {
    return this.getRequest<Category[]>(`${this.endpoint}/tree`);
  }

  getById(id: string): Observable<Category> {
    return this.getRequest<Category>(`${this.endpoint}/${id}`);
  }

  create(payload: Partial<Category>): Observable<Category> {
    return this.postRequest<Category>(this.endpoint, payload);
  }

  update(id: string, payload: Partial<Category>): Observable<Category> {
    return this.putRequest<Category>(`${this.endpoint}/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.deleteRequest<void>(`${this.endpoint}/${id}`);
  }

  getDescendantIds(id: string) {
    return this.getRequest<string[]>(
      `${this.endpoint}/${id}/descendants`
    );
  }
}
