import { Routes } from '@angular/router';
import { MeasurementComponent } from './measurement/measurement.component';

export const routes: Routes = [
  { path: '', redirectTo: 'measurement', pathMatch: 'full' },
  { path: 'measurement', component: MeasurementComponent }
];
