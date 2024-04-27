import { Component, inject } from '@angular/core';
import { MeasurementsComponent } from './measurements/measurements.component';
import { Measurement, MeasurementGrade, Patient } from '../core/domain/domain';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PatientSearchPipe } from '../core/pipes/patient-search.pipe';
import { PatientSeenPipe } from '../core/pipes/patient-seen.pipe';
import { calcAge } from '../core/helpers/age-calc';
import { NgbModal, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientCreateModalComponent } from './patient-create-modal/patient-create-modal.component';
import { DomSanitizer } from '@angular/platform-browser';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-patient-view',
  standalone: true,
  templateUrl: './patient-view.component.html',
  styleUrl: './patient-view.component.scss',
  imports: [
    CommonModule,
    FormsModule,
    MeasurementsComponent,
    PatientSearchPipe,
    PatientSeenPipe,
    NgbTooltipModule,
  ],
})
export class PatientViewComponent {
  showNewOnly: boolean = false;
  searchText: string = '';

  private modalService = inject(NgbModal);
  private sanitizer = inject(DomSanitizer);
  private patientService = inject(PatientService);

  viewingPatient: Patient | null = null;

  patientCollection: Patient[] = [
    {
      mail: 'mail1@mail.com',
      measurements: [
        { date: new Date(), seen: false, diastolic: 60, systolic: 100, id: 0, patientSSN: '160499-1234'},
        { date: new Date(), seen: false, diastolic: 80, systolic: 120, id: 1, patientSSN: '160499-1234'},
        { date: new Date(), seen: true, diastolic: 90, systolic: 130, id: 2, patientSSN: '160499-1234'},
        { date: new Date(), seen: false, diastolic: 100, systolic: 140, id: 3, patientSSN: '160499-1234'},
      ],
      name: 'John Doe',
      ssn: '160499-1234',
    },
    {
      mail: 'mail2@mail.com',
      measurements: [],
      name: 'Jane Doe',
      ssn: '311098-1234',
    },
    {
      mail: 'mail1@mail.com',
      measurements: [],
      name: 'John Doe',
      ssn: '160499-1234',
    },
    {
      mail: 'mail2@mail.com',
      measurements: [],
      name: 'Jane Doe',
      ssn: '311098-1234',
    },
    {
      mail: 'mail1@mail.com',
      measurements: [],
      name: 'John Doe',
      ssn: '160499-1234',
    },
    {
      mail: 'mail2@mail.com',
      measurements: [],
      name: 'Jane Doe',
      ssn: '311098-1234',
    },
    {
      mail: 'mail1@mail.com',
      measurements: [],
      name: 'John Doe',
      ssn: '160499-1234',
    },
    {
      mail: 'mail2@mail.com',
      measurements: [],
      name: 'Jane Doe',
      ssn: '311098-1234',
    },
    {
      mail: 'mail1@mail.com',
      measurements: [],
      name: 'John Doe',
      ssn: '160499-1234',
    },
    {
      mail: 'mail2@mail.com',
      measurements: [],
      name: 'Jane Doe',
      ssn: '311098-1234',
    },
  ];

  calcAge(ssn: string): number {
    return calcAge(ssn);
  }

  getSafeUrl(url: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  selectPatient(patient: Patient) {
    if (this.viewingPatient === patient) {
      this.viewingPatient = null;
    } else {
      this.viewingPatient = patient;
    }
  }

  hasNewMeasurements(patient: Patient): boolean {
    return patient.measurements.some((measurement) => {
      return !measurement.seen;
    });
  }

  newPatient() {
    const modalRef = this.modalService.open(PatientCreateModalComponent, {
      centered: true,
    });

    modalRef.result.then((newPatient: Patient) => {
      if (!newPatient) {
        return;
      }
      this.patientService.addPatient(newPatient).subscribe(
        (response) => {
          this.patientCollection.push(response);
        },
      );
    });
  }

  onPatientDeleted(patient: Patient) {
    this.patientService.deletePatient(patient.ssn).subscribe((reponse) => {
      if (reponse) {
        this.patientCollection = this.patientCollection.filter(
          (p) => p !== patient
        );
        this.viewingPatient = null;
      }
    });
  }
}
