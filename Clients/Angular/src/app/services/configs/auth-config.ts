import { MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';
import { BrowserCacheLocation, Configuration, InteractionType, IPublicClientApplication, LogLevel, PublicClientApplication } from '@azure/msal-browser';
import { environment } from '../../../environments/environment';

export const b2cPolicies = {
  names: {
    signUpSignIn: environment.azureAdB2C.policies.signUpSignIn,
    editProfile: environment.azureAdB2C.policies.editProfile,
    resetpassword: environment.azureAdB2C.policies.resetPassword
  },
  authorities: {
    signUpSignIn: `https://${environment.azureAdB2C.tenantName}.b2clogin.com/${environment.azureAdB2C.tenantName}.onmicrosoft.com/${environment.azureAdB2C.policies.signUpSignIn}`,
    editProfile: `https://${environment.azureAdB2C.tenantName}.b2clogin.com/${environment.azureAdB2C.tenantName}.onmicrosoft.com/${environment.azureAdB2C.policies.editProfile}`,
    resetPassword: `https://${environment.azureAdB2C.tenantName}.b2clogin.com/${environment.azureAdB2C.tenantName}.onmicrosoft.com/${environment.azureAdB2C.policies.resetPassword}`
  },
  authorityDomain: `${environment.azureAdB2C.tenantName}.b2clogin.com`
}

export const msalConfig: Configuration = {
  auth: {
    clientId: environment.azureAdB2C.clientId,
    authority: b2cPolicies.authorities.signUpSignIn,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: environment.azureAdB2C.redirectUrl
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage
  },
  system: {
    loggerOptions: {
      loggerCallback: (_, message, _1) => {
        console.log(message);
      },
      logLevel: LogLevel.Verbose,
      piiLoggingEnabled: false
    }
  }
}

export function MsalInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication(msalConfig)
}

export function MsalInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map<string, Array<string>>()
  }
}

export function MsalGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect
  }
}
