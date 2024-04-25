import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Measurement, MeasurementGrade, Patient } from '../../core/domain/domain';
import { calcAge } from '../../core/helpers/age-calc';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';
import { NgbModal, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientDeleteModalComponent } from '../patient-delete-modal/patient-delete-modal.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-measurements',
  standalone: true,
  imports: [CommonModule, NgbTooltipModule ],
  templateUrl: './measurements.component.html',
  styleUrl: './measurements.component.scss'
})
export class MeasurementsComponent {

  @Input() patient: Patient | null = null;
  @Output() deletePatientEvent = new EventEmitter<Patient>();
  private modalService = inject(NgbModal);

  constructor(
    private patientService: PatientService,
    private sanitizer: DomSanitizer
  ) { }



  markAsSeen(measurements: Measurement) {
    this.patientService.markMeasurementAsSeen(this.patient?.ssn!, measurements.id);
    //TODO: Implement this
    measurements.seen = true;
  }

  calcAge(ssn: string | undefined): number {
    if (!ssn) {
      return 0;
    }

    return calcAge(ssn);
  }



  openDeleteModal(patient: Patient) {
    const modalRef =this.modalService.open(PatientDeleteModalComponent, { centered: true });
    modalRef.result.then((bool) => {
      if (bool) {
        console.log('Delete patient');
        this.deletePatientEvent.emit(patient);
      }
    });
  }




  averageMeasurement(): string {

    if (!this.patient?.measurements.length) {
      return 'No measurements';
    }

    let systolicSum = 0;
    let diastolicSum = 0;

    this.patient?.measurements.forEach((measurement) => {
      systolicSum += measurement.systolic;
      diastolicSum += measurement.diastolic;
    });

    const avg = [systolicSum / this.patient?.measurements.length!, diastolicSum / this.patient?.measurements.length!];
    return avg.join('/');
  }

  gradeMeasurement(measurement: Measurement): MeasurementGrade {
    const { systolic, diastolic } = measurement;

    if (systolic < 90 || diastolic < 60) {
      return MeasurementGrade.DANGER;
    } else if (
      systolic >= 90 &&
      systolic <= 120 &&
      diastolic >= 60 &&
      diastolic < 80
    ) {
      return MeasurementGrade.NORMAL;
    } else if (
      (systolic >= 120 && systolic < 140) ||
      (diastolic >= 80 && diastolic < 90)
    ) {
      return MeasurementGrade.WARNING;
    } else {
      return MeasurementGrade.DANGER;
    }
  }

  get EMeasurementGrade() {
    return MeasurementGrade;
  }

  getSafeUrl(url: string): any {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

}
