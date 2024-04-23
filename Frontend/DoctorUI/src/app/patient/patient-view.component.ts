import { Component } from '@angular/core';
import { MeasurementsComponent } from "./measurements/measurements.component";
import { Patient } from '../core/domain/domain';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PatientSearchPipe } from "../core/pipes/patient-search.pipe";

@Component({
    selector: 'app-patient-view',
    standalone: true,
    templateUrl: './patient-view.component.html',
    styleUrl: './patient-view.component.scss',
    imports: [CommonModule, FormsModule, MeasurementsComponent, PatientSearchPipe]
})
export class PatientViewComponent {
  sortingType: string = 'name';
  sortDirection: string = 'asc';
  searchText: string = '';

  viewingPatient: Patient | null = null;


  patientCollection: Patient[] = [
    { mail: 'mail1@mail.com', measurements: [], name: 'John Doe', ssn: '160499-1234' },
    { mail: 'mail2@mail.com', measurements: [], name: 'Jane Doe', ssn: '311098-1234' }
  ];


  calcAge(ssn: string): number {
    const dob = ssn.substring(0, 6);
    const dobDay = parseInt(dob.substring(0, 2));
    const dobMonth = parseInt(dob.substring(2, 4))-1;
    const dobYear = parseInt(dob.substring(4, 6));
    const dobFullYear = dobYear < 20 ? 2000 + dobYear : 1900 + dobYear;

    let dobDate = new Date();
    dobDate.setFullYear(dobFullYear, dobMonth, dobDay);
    const ageDifMs = Date.now() - dobDate.getTime();
    const ageDate = new Date(ageDifMs);
    return Math.abs(ageDate.getUTCFullYear() - 1970);
  }

}
