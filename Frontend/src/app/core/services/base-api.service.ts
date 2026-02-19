import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export abstract class BaseApiService {

  constructor(protected http: HttpClient) {}

  protected handleError(error: HttpErrorResponse) {
    console.error('API Error:', error);
    return throwError(() => error.message || 'API Error');
  }

  protected getRequest<T>(url: string, options?: {params?: any }): Observable<T> {
    return this.http.get<T>(url, { ...options, observe: 'body'})
      .pipe(catchError((error) => this.handleError(error)));
  }

  protected postRequest<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(url, body)
      .pipe(catchError((error) => this.handleError(error)));

  }

  protected putRequest<T>(url: string, body: any): Observable<T> {
    return this.http.put<T>(url, body)
      .pipe(catchError((error) => this.handleError(error)));
  }

  protected deleteRequest<T>(url: string): Observable<T> {
    return this.http.delete<T>(url)
      .pipe(catchError((error) => this.handleError(error)));

  }
}
