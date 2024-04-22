import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement } from '../domain/measurement';
import { catchError, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  constructor(private http: HttpClient) { }

  postMeasurement(measurement: Measurement): Observable<boolean> {
    return this.http.post('/measurements', measurement)
    .pipe(
      catchError((error) => {
        console.error(error);
        return [error];
      }),
      map((response) => {
      if (response instanceof Error
        || response instanceof ErrorEvent
        || response === undefined
        || response === null
        || response === false) {
        return false;
      }
      return true;
    }))
  }

  validateCvr(cvr: string) {
    return this.http.get(`/cvr/${cvr}`);
  }

}
