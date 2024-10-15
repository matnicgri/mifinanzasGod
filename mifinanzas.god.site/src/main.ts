import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import '@angular/compiler';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes'; 
import { importProvidersFrom, ErrorHandler } from '@angular/core';
import { GlobalErrorHandler } from '../src/app/global-error-handler'; 

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideRouter(routes), 
    { provide: ErrorHandler, useClass: GlobalErrorHandler } 
  ],
})
.catch((err) => console.error(err));
