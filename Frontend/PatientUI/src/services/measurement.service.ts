import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement } from '../domain/measurement';
import { catchError, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  constructor(private http: HttpClient) { }

  postMeasurement(measurement: Measurement): Observable<boolean> {
    return this.http.post<HttpResponse<any>>('/measurements', measurement)
    .pipe(
      map((response) => {
      if (response.ok) {
        return true;
      }
      else return false;
    }))
  }

  validateCvr(cvr: string) {
    return this.http.get(`/cvr/${cvr}`);
  }

}
