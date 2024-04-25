import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../core/domain/domain';
import { Observable, of } from 'rxjs';

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
    return of(patient);
    return this.http.post<Patient>('/patients', patient);
  }

  updatePatient(patient: Patient) {
    return this.http.put<Patient>(`/patients/${patient.ssn}`, patient);
  }

  deletePatient(ssn: string) {
    return of(true);
    return this.http.delete(`/patients/${ssn}`);
    //TODO: Implement this
  }

  getPatientMeasurements(ssn: string) {
    return this.http.get(`/patients/${ssn}/measurements`);
  }

  markMeasurementAsSeen(ssn: string, measurementId: number): Observable<boolean> {
    return of(true)
    this.http.put(`/patients/${ssn}/measurements/${measurementId}/seen`, null);
  }

}
