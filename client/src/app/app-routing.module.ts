import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

//Roots repersent a root configuration for Router service, array of root, obj used Router.config
//path: each one of these root is going to be obj
//componet: specify the component we want to load 
// chỉ dẫn đường link dẫn đến các components khi đã baseUrl
// add to app.component.html 
//baseURL: lay tu account service vi da inject vao app.component.ts
const routes: Routes = [
  {path: '',component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:[
        //6. Adding an Angular route guard (ko cho truy cap lung tung dung ko co authorized)
        {path: 'members',component: MemberListComponent, canActivate: [AuthGuard]},
        //each of members is going to have a root parameter 
        {path: 'members/:id',component: MemberDetailComponent},
        {path: 'lists',component: ListsComponent},
        {path: 'messages',component: MessagesComponent}
    ]
  },
  //check loi, hien thi man hinh 
  {path: 'errors', component: TestErrorsComponent},
  //hien thi page not Found
  {path: 'not-found', component: NotFoundComponent},
  //hien thi server error
  {path: 'server-error', component: ServerErrorComponent},
  //as in the user's typed in something that doesn't match anything inside our reconfiguration
  //we'll direct them to Not Found
  //VD: http://localhost:4200/server-errordasdasd
  //use wildcard(**) and we also specify an extra attribute and we say "pathMatch"
  {path: '**',component: NotFoundComponent, pathMatch: 'full'},
];
// So we have some routes in place => we need to provide to Angular (app.component.html)
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
