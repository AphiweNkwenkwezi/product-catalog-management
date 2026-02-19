import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import { Product } from '../../shared/models/product.model';

describe('ProductService', () => {

  let service: ProductService;
  let httpMock: HttpTestingController;

  const mockResponse = {
    items: [
      {
        id: '1',
        name: 'Laptop Pro 15',
        sku: 'LTP-001',
        price: 15000,
        quantity: 10,
        categoryId: 'cat-1'
      }
    ],
    totalCount: 1,
    page: 1,
    pageSize: 10
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // ensures no unexpected HTTP calls
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch products with pagination and filters', () => {

    service.getProducts('laptop', 'cat-1', 1, 10)
      .subscribe(response => {
        expect(response.items.length).toBe(1);
        expect(response.totalCount).toBe(1);
        expect(response.items[0].name).toBe('Laptop Pro 15');
      });

    const req = httpMock.expectOne(req =>
      req.method === 'GET' &&
      req.url.includes('/api/products')
    );

    expect(req.request.params.get('search')).toBe('laptop');
    expect(req.request.params.get('categoryId')).toBe('cat-1');
    expect(req.request.params.get('page')).toBe('1');
    expect(req.request.params.get('pageSize')).toBe('10');

    req.flush(mockResponse);
  });

  it('should delete a product', () => {

    service.delete('1').subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne('/api/products/1');
    expect(req.request.method).toBe('DELETE');

    req.flush({});
  });

});
