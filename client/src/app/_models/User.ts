export interface User{
    //using in account.service.ts, app.component.ts
    username: string;
    token: string;
    //12. Adding the main photo image to the nav bar
    photoUrl: string;
    knownAs: string;
    //9. Cleaning up the member service
    gender: string;
    ////14. Adding an admin guard
    roles: string[];
}
