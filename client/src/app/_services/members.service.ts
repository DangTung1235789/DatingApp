import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
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
// don't need this because use jwt.interceptor.ts
@Injectable({
  providedIn: 'root'
})
export class MembersService {
  //it's a service that's going to make HTTP request to our API 
  
  baseUrl = environment.apiUrl;
  //9. Using the service to store state
  members: Member[] = [];
  //we're also going to bring in HTTP client 
  constructor(private http:HttpClient) { }
  // Member[], username in member.ts ( dùng member.ts để type safety inside functions )
  //we specify the type in our get request to tell it what we're receiving back from the serve
  // we're going to have full time safety and intelligence based on just adding this property inside method
  getMembers(): Observable<Member[]>{
    //9. Using the service to store state
    //return members, but dont forget we need to return this as unobservable because our client
    //our component is going to be observing this data  => use of
    //return the members from the service as an observable 
    if(this.members.length > 0) return of(this.members);
    //and then we need to do is set the members once we've gone
    //and made the effort to get them from the API => use pipe
    return this.http.get<Member[]>(this.baseUrl + 'users')/*, httpOptions);*/.pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }
  // we're going to have full time safety and intelligence based on just adding this property inside method
  getMember(username: string){
    //9. Using the service to store state
    const member = this.members.find(x => x.username === username);
    if(member !== undefined) return of(member);
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
}
