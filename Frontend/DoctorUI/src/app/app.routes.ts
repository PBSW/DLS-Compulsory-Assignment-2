import { Routes } from '@angular/router';
import { PatientViewComponent } from './patient/patient-view.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full'
  },
  {
    path: 'patients',
    pathMatch: 'full',
    component: PatientViewComponent
  }
];
