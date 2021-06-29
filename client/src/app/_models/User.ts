export interface User{
    //using in account.service.ts, app.component.ts
    username: string;
    token: string;
    //12. Adding the main photo image to the nav bar
    photoUrl: string;
}
