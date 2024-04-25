import { Pipe, PipeTransform } from '@angular/core';
import { Patient } from '../domain/domain';

@Pipe({
  name: 'patientSeen',
  standalone: true
})
export class PatientSeenPipe implements PipeTransform {

  transform(patients: Patient[], hideSeen: boolean): Patient[] {

    if (!hideSeen) {
      return patients;
    }

    return patients.filter((patient: Patient) => {
      return patient.measurements.some((measurement) => {
        return !measurement.seen;
      });
    });

  }

}
