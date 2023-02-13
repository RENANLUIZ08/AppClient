export class Pagination {
  currentPage = 1 as Number;
  itemsPPage = 3 as Number;
  totalItems = 1 as Number;
}

export class PaginationResult<T> {
  result = {} as T;
  pagination = {} as Pagination;
}
