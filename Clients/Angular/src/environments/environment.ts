// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  azureADB2C: {
    clientId: 'e46f8fb1-4d68-43cc-b53b-cf052b8f195f',
    redirectUrl: 'http://localhost:4200/',
    policies: {
      signUpSignIn: 'B2C_1_SignUpIn',
      editProfile: 'B2C_1_ProfileEdit',
      resetPassword: 'B2C_1_PasswordReset'
    },
    tenantName: 'budgetifydev',
    api: {
      applicationIdUri: 'https://budgetifydev.onmicrosoft.com/api',
      scope: 'read' // or permission
    }
  },
  baseApiUrl: 'http://localhost:55555/api/'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
