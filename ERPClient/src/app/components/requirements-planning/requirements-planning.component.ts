import { Component } from '@angular/core';
import { RequirementsPlanningModel } from '../../models/requirements-planning.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-requirements-planning',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './requirements-planning.component.html',
  styleUrl: './requirements-planning.component.css'
})
export class RequirementsPlanningComponent {
  data: RequirementsPlanningModel = new RequirementsPlanningModel();
}
