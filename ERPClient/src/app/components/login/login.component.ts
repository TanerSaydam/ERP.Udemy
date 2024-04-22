import { Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { LoginModel } from '../../models/login.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginModel = new LoginModel();
 
  signIn(){
    
  }
}
