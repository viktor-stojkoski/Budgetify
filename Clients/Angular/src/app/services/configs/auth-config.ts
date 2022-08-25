import { MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';
import { BrowserCacheLocation, Configuration, InteractionType, IPublicClientApplication, LogLevel, PublicClientApplication } from '@azure/msal-browser';
import { environment } from '../../../environments/environment';

export const b2cPolicies = {
  names: {
    signUpSignIn: environment.azureADB2C.policies.signUpSignIn,
    editProfile: environment.azureADB2C.policies.editProfile,
    resetpassword: environment.azureADB2C.policies.resetPassword
  },
  authorities: {
    signUpSignIn: `https://${environment.azureADB2C.tenantName}.b2clogin.com/${environment.azureADB2C.tenantName}.onmicrosoft.com/${environment.azureADB2C.policies.signUpSignIn}`,
    editProfile: `https://${environment.azureADB2C.tenantName}.b2clogin.com/${environment.azureADB2C.tenantName}.onmicrosoft.com/${environment.azureADB2C.policies.editProfile}`,
    resetPassword: `https://${environment.azureADB2C.tenantName}.b2clogin.com/${environment.azureADB2C.tenantName}.onmicrosoft.com/${environment.azureADB2C.policies.resetPassword}`
  },
  authorityDomain: `${environment.azureADB2C.tenantName}.b2clogin.com`
}

export const msalConfig: Configuration = {
  auth: {
    clientId: environment.azureADB2C.clientId,
    authority: b2cPolicies.authorities.signUpSignIn,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: environment.azureADB2C.redirectUrl
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage
  },
  system: {
    loggerOptions: {
      loggerCallback: (_, message, _1) => {
        if (!environment.production) {
          console.log(message);
        }
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
