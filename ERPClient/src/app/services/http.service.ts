import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { api } from '../constants';
import { ResultModel } from '../models/result.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient
  ) { }

  post<T>(apiUrl:string, body:any, callBack:(res:T)=> void,errorCallBack?:()=> void ){
    this.http.post<ResultModel<T>>(`${api}/${apiUrl}`,body,{
      headers: {
        "Authorization": "Bearer " + "token"
      }
    }).subscribe({
      next: (res)=> {
        if(res.data){
          callBack(res.data);
        }        
      },
      error: (err:HttpErrorResponse)=> {
        if(errorCallBack){
          errorCallBack();
        }
      }
    })
  }
}
