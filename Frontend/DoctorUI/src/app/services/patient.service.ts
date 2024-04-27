import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement, Patient } from '../core/domain/domain';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) { }


  getPatients() {
    return this.http.get<Patient[]>('/patient');
  }

  getPatient(ssn: string) {
    return this.http.get<Patient>(`/patient/${ssn}`);
  }

  addPatient(patient: Patient) {
    const body = {
      ssn: patient.ssn,
      name: patient.name,
      mail: patient.mail,
    }
    return of(patient);
    return this.http.post<Patient>('/patient', body);
  }

  deletePatient(ssn: string) {
    const body = {
      ssn: ssn
    }
    return of(true);
    return this.http.delete(`/patient`, {body : body});
    //TODO: Implement this
  }

  getPatientMeasurements(ssn: string) {
    return this.http.get(`/measurement/${ssn}`);
  }

  markMeasurementAsSeen(measurement: Measurement): Observable<boolean> {
    const body = {
      seen: true,
    }

    return of(true)
    this.http.put(`/measurement`, body);
  }

}
