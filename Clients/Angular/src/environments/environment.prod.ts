export const environment = {
  production: true,
  azureADB2C: {
    clientId: '',
    redirectUrl: '',
    policies: {
      signUpSignIn: 'B2C_1_SignUpIn',
      editProfile: 'B2C_1_ProfileEdit',
      resetPassword: 'B2C_1_PasswordReset'
    },
    tenantName: ''
  },
  baseApiUrl: ''
};
