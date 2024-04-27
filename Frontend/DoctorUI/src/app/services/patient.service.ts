import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement, Patient } from '../core/domain/domain';
import { map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  constructor(private http: HttpClient) {}

  getPatients() {
    return this.http.get<Patient[]>('/patient');
  }

  getPatient(ssn: string) {
    return this.http.get<Patient>(`/patient/${ssn}`);
  }

  addPatient(patient: Patient) {

    if (patient.ssn.includes('-')) {
      patient.ssn = patient.ssn.replace('-', '');
    }

    const body = {
      ssn: patient.ssn,
      name: patient.name,
      mail: patient.mail,
    };

    return this.http.post<Patient>('/patient', body);
  }

  deletePatient(ssn: string) {
    const body = {
      ssn: ssn,
    };
    return this.http.delete<boolean>(`/patient`, { body: body });
  }

  getPatientMeasurements(ssn: string): Observable<Measurement[]> {
    return this.http.get<Measurement[]>(`/measurement/${ssn}`);
  }

  markMeasurementAsSeen(measurement: Measurement): Observable<boolean> {
    const body = {
      id: measurement.id,
      patientSSN: measurement.patientSSN,
      systolic: measurement.systolic,
      diastolic: measurement.diastolic,
      date: measurement.date,
      seen: true,
    };
    return this.http.put<Measurement>(`/measurement`, body).pipe(
      map((response) => {
        console.log(response);
        if (response) {
          return true;
        }
        else return false;
      })
    );
  }
}
