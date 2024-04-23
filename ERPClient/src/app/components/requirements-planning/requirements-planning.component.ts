import { Component } from '@angular/core';
import { RequirementsPlanningModel } from '../../models/requirements-planning.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-requirements-planning',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './requirements-planning.component.html',
  styleUrl: './requirements-planning.component.css'
})
export class RequirementsPlanningComponent {
  data: RequirementsPlanningModel = new RequirementsPlanningModel();
  orderId: string = "";

  constructor(
    private activated: ActivatedRoute,
    private http: HttpService
  ){
    this.activated.params.subscribe(res=> {
      this.orderId = res["orderId"];
      this.get();
    });
  }

  get(){
    this.http.post<RequirementsPlanningModel>("Orders/RequirementsPlanningByOrderId",{orderId: this.orderId}, res=> {
      this.data = res;
    });
  }
}
