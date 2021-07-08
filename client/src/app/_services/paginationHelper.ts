import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { PaginatedResult } from "../_models/pagination";

//8. Setting up the Angular app for messaging
export function getPaginatedResult<T>(url: any , params: any, http: HttpClient) {
    // 5. Setting up client pagination
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return http.get<T>(url, { observe: 'response', params }) /*, httpOptions);*/.pipe(
      // map(members => {
      //   this.members = members;
      //   return members;
      // })
      map(response => {
        // 5. Setting up client pagination
        //response.body contain member[]
        paginatedResult.result = response.body!;
        //check our pagination header 
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '');
        }
        return paginatedResult;
      })
    );
  }

  // 9. Cleaning up the member service
  // append noi chuoi
  export function getPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();
      //pageNumber, pageSize: query string
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
    return params;
  }