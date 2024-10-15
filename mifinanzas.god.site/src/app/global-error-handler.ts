import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  handleError(error: any): void {
    console.error('An unexpected error occurred:', error);
    alert('An unhandled error occurred. Please try again.');
  }
}