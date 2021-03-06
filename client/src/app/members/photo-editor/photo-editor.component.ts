import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  //8. Adding a photo editor component
  //we're going to receive our member from the parent component 
  @Input() member!: Member;
  //9. Adding a photo uploader
  uploader!: FileUploader
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user!: User;
  //9. Adding a photo uploader
  constructor(private accountService: AccountService, private memberService: MembersService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeUploader();
  }
  //9. Adding a photo uploader
  fileOverBase(e: any){
    this.hasBaseDropzoneOver = e;
  }
  
  //13. Setting the main photo in the client
  // lồng vào cả 12. Adding the main photo image to the nav bar và 11. Setting the main photo in the API
  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;
      this.member.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    })
  } 
  deletePhoto(photoId: number){
    this.memberService.deletePhoto(photoId).subscribe(() => {
      this.member.photos = this.member.photos.filter(x => x.id !== photoId);
    })
  }
  //9. Adding a photo uploader
  initializeUploader(){
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'], //application, image, video, audio, pdf, compress(rar), doc, xls, ppt
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10*1024*1024
    });
    //9. Adding a photo uploader
    this.uploader.onAfterAddingFile = (file) =>{
      file.withCredentials = false;
    }
    //9. Adding a photo uploader
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if(response){
        const photo: Photo = JSON.parse(response);
        this.member.photos.push(photo);
        //khi mới đăng kí register
        if(photo.isMain){
          this.user.photoUrl = photo.url;
          this.member.photoUrl = photo.url;
          this.accountService.setCurrentUser(this.user);
        }
      }
    }
    //13. Setting the main photo in the client
    
  }
}
