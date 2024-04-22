import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor() { }

  errorHandler(err:HttpErrorResponse){
    console.log(err);

    if(err.status === 0){
      
    }    
  }
}
