import { ApplicationConfig, LOCALE_ID, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { withFetch } from '@angular/common/http';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
registerLocaleData(localePt);

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(),  
    importProvidersFrom(HttpClientModule),
    { provide: LOCALE_ID, useValue: 'pt' }]
};
