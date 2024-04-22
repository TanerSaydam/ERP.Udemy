import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token: string = "";
  user: UserModel = new UserModel();

  constructor(
    private router: Router
  ) { }

  isAuthenticated(){
    this.token = localStorage.getItem("token") ?? "";
    if(this.token === ""){
      this.router.navigateByUrl("/login");
      return false;
    }

    const decode: JwtPayload | any = jwtDecode(this.token);
    const exp = decode.exp;
    const now = new Date().getTime() / 1000;

    if(now > exp){
      this.router.navigateByUrl("/login");
      return false;
    }

    this.user.id = decode["Id"];
    this.user.name = decode["Name"];
    this.user.email = decode["Email"];
    this.user.userName = decode["UserName"];

    console.log(this.user);
    

    return true;
  }
}