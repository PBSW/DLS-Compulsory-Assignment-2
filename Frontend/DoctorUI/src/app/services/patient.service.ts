import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../core/domain/domain';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) { }


  getPatients() {
    return this.http.get<Patient[]>('/patients');
  }

  getPatient(ssn: string) {
    return this.http.get<Patient>(`/patients/${ssn}`);
  }

  addPatient(patient: Patient) {
    return this.http.post<Patient>('/patients', patient);
  }

  updatePatient(patient: Patient) {
    return this.http.put<Patient>(`/patients/${patient.ssn}`, patient);
  }

  deletePatient(ssn: string) {
    return this.http.delete(`/patients/${ssn}`);
  }

  getPatientMeasurements(ssn: string) {
    return this.http.get(`/patients/${ssn}/measurements`);
  }

  markMeasurementAsSeen(ssn: string, measurementId: number) {
    return this.http.put(`/patients/${ssn}/measurements/${measurementId}/seen`, null);
  }

}
