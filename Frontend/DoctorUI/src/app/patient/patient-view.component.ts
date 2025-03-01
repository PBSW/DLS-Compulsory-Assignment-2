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
import { FeatureHub } from 'featurehub-javascript-client-sdk';

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
  patientCreateAllowed: boolean = true;

  viewingPatient: Patient | null = null;
  patientCollection: Patient[] = [];

  constructor() {
    this.patientService.getPatients().subscribe((patients) => {
      console.log(patients);
      this.patientCollection = patients;
    });
    this.isPatientCreateDisabled();
  }

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

  isPatientCreateDisabled() {
    FeatureHub.feature('patient-create')
    .addListener((featureState) => {
      const state = featureState.enabled;
      this.patientCreateAllowed = state;
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
          console.log(response);
          this.patientCollection.push(response);
        },
      );
    });
  }

  onPatientDeleted(patient: Patient) {
    this.patientService.deletePatient(patient.ssn).subscribe((reponse) => {
      if (reponse) {
        console.log(reponse);
        this.patientCollection = this.patientCollection.filter(
          (p) => p !== patient
        );
        this.viewingPatient = null;
      }
    });
  }
}
