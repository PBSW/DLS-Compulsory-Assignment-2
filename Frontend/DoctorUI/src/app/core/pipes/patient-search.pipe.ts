import { Pipe, PipeTransform } from '@angular/core';
import { Patient } from '../domain/domain';

@Pipe({
  name: 'patientSearch',
  standalone: true
})
export class PatientSearchPipe implements PipeTransform {

  transform(patients: Patient[], query: string): Patient[] {
    if (!query) {
      return patients;
    }

    return patients.filter((patient: Patient) => {
      return patient.name.toLowerCase().includes(query.toLowerCase()) ||
        patient.ssn.includes(query);
    });
  }

}
