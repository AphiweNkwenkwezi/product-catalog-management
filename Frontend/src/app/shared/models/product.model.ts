export interface Product {
  id: string;
  name: string;
  description?: string;
  sku: string;
  price: number;
  quantity: number;
  categoryId: string;
  createdAt?: string;
  updatedAt?: string;
}
