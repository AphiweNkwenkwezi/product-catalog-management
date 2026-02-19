export interface Category {
  id: string;
  name: string;
  description?: string;
  parentCategoryId?: string | null;
  parentName?: string | null;
}

export interface CategoryNode {
  id: string;
  name: string;
  description?: string;
  children: CategoryNode[];
}
