// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  /*
   - what we can put inside this is any variables we want to use across our application
    that will automatically use a different version when we're in production 
  */
  apiUrl: 'https://localhost:5001/api/',
  //17.4. Client side SignalR
  hubUrl: 'https://localhost:5001/hubs/'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
