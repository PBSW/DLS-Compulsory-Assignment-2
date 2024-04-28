import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement } from '../domain/measurement';
import { catchError, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MeasurementService {
  constructor(private http: HttpClient) {}

  postMeasurement(measurement: Measurement): Observable<boolean> {
    const body = {
      patientSSN: measurement.cvr,
      diastolic: measurement.diastolic,
      systolic: measurement.systolic,
    };

    return this.http.post<HttpResponse<any>>('/measurement', body).pipe(
      map((response) => {
        console.log(response);
        if (response.ok) {
          return true;
        } else return false;
      })
    );
  }
}
