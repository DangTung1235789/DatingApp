import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/User';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

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
// don't need this because use jwt.interceptor.ts
@Injectable({
  providedIn: 'root'
})
export class MembersService {
  //it's a service that's going to make HTTP request to our API 
  
  baseUrl = environment.apiUrl;
  //9. Using the service to store state
  members: Member[] = [];
  //16. Restoring the caching for members
  // when we want store smt with a key and value, a good thing to use is a map
  memberCache = new Map();
  user!: User;
  userParams!: UserParams;
  //we're also going to bring in HTTP client 
  constructor(private http:HttpClient, private accountService: AccountService) {
    //18. Remembering the filters for a user in the service 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }
  //18. Remembering the filters for a user in the service
  getUserParams(){
    return this.userParams;
  }
  setUserParams(params: UserParams){
    this.userParams = params;
  }

  resetUserParams(){
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }
  // Member[], username in member.ts ( dùng member.ts để type safety inside functions )
  //we specify the type in our get request to tell it what we're receiving back from the serve
  // we're going to have full time safety and intelligence based on just adding this property inside method
  // 5. Setting up client pagination
  // 9. Cleaning up the member service
  getMembers(UserParams: UserParams){
    var response = this.memberCache.get(Object.values(UserParams).join('-'));
    if(response){
      return of(response);
    }
    //9. Using the service to store state
    //return members, but dont forget we need to return this as unobservable because our client
    //our component is going to be observing this data  => use of
    //return the members from the service as an observable 
    //if(this.members.length > 0) return of(this.members);
    //and then we need to do is set the members once we've gone
    //and made the effort to get them from the API => use pipe
    // 5. Setting up client pagination
    // 9. Cleaning up the member service
    let params = getPaginationHeaders(UserParams.pageNumber, UserParams.pageSize);

    params = params.append('minAge',UserParams.minAge.toString());
    params = params.append('maxAge',UserParams.maxAge.toString());
    params = params.append('gender',UserParams.gender);
    params = params.append('orderBy',UserParams.orderBy);
    
    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
      //16. Restoring the caching for members
      .pipe(map(response => {
        this.memberCache.set(Object.values(UserParams).join('-'), response);
        return response;
      }))
  }
  
  // we're going to have full time safety and intelligence based on just adding this property inside method
  getMember(username: string){
    //9. Using the service to store state
    // const member = this.members.find(x => x.username === username);
    // if(member !== undefined) return of(member);
    //17. Restoring caching for member detailed
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.username === username);
    
    if(member){
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + 'users/' + username)//, httpOptions);
  }
  //=> hiển thị đường link web, lấy dữ liệu từ API
  //7. Updating the user in the client app
  /*
  - if we're getting our members now from our service, then if we update then when they go back, they're going to go and see the old data
  that's stored members: Member[] = [];
  */
  updateMember(member: Member){
    //using "pipe" because we're going to do smt with this data
    return this.http.put(this.baseUrl + 'users', member).pipe(
      //we need to get member from the service
      map(()=>{
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }
  //13. Setting the main photo in the client
  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }
  //15. Deleting photos - Client
  // ('users/delete-photo/'): called root in userController.cs
  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
  //6. Setting up the likes functions in the Angular app
  addLike(username: string){
    return this.http.post(this.baseUrl + 'likes/' + username, {});
  }
  //9. Paginating the likes on the client
  getLikes(predicate: string, pageNumber: any, pageSize: any){
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate);
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'likes', params, this.http);
  }

  //9. Cleaning up the member service
  
}
