import { User } from "./User";

export class UserParams {
    // we will be displaying this information to a user when they load the members list
    //display what a default currently are inside here 
    gender!: string;
    minAge = 18;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 5;
    orderBy = "lastActive";

    constructor(user: User){
        //if user is a female, then we're going to return male
        // if not, return female
        this.gender = user.gender === 'female' ? 'male' : 'female';
    }
}