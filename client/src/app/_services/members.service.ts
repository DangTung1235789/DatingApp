import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// /*
//   - we're going to send up our authentication because our users endpoint is protected
//   by authentication => so we need to add that as a header
// */
//  const httpOptions = {
//    //inside HttpHeaders, we specify what header we want to provide 
//    headers: new HttpHeaders({  
//      Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')||'')?.token
//    })
//  }

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  //it's a service that's going to make HTTP request to our API 
  
  baseUrl = environment.apiUrl;
  //we're also going to bring in HTTP client 
  constructor(private http:HttpClient) { }
  // Member[], username in member.ts ( dùng member.ts để type safety inside functions )
  //we specify the type in our get request to tell it what we're receiving back from the serve
  // we're going to have full time safety and intelligence based on just adding this property inside method
  getMembers(): Observable<Member[]>{
    return this.http.get<Member[]>(this.baseUrl + 'users')//, httpOptions);
  }
  // we're going to have full time safety and intelligence based on just adding this property inside method
  getMember(username: string){
    return this.http.get<Member>(this.baseUrl + 'users/' + username)//, httpOptions);
  }
  //=> hiển thị đường link web, lấy dữ liệu từ API
}
