import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'BMHEcommerce.Admin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5000/',
    redirectUri: baseUrl,
    clientId: 'BMHEcommerce_Admin',
    responseType: 'code',
    scope: 'offline_access BMHEcommerce.Admin',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:5001',
      rootNamespace: 'BMHEcommerce.Admin',
    },
  },
} as Environment;
