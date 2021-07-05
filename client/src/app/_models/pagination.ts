//5. Setting up client pagination
//we're going to give our pagination information
export interface Pagination{
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}
//our result, our list of members are going to be stored in the "result" property 
// and the pagination information is going to be stored in the "pagination"
export class PaginatedResult<T>{
    result!: T;
    pagination!: Pagination;
}