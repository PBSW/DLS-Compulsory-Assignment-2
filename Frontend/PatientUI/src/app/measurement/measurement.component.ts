import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { Measurement } from '../../domain/measurement';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-measurement',
  standalone: true,
  imports: [ReactiveFormsModule, NgbModalModule, CommonModule],
  templateUrl: './measurement.component.html',
  styleUrl: './measurement.component.scss',
})
export class MeasurementComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  active: number = 1;

  constructor() {
    this._createFormGroup();
  }

  ngOnInit() {}

  resetForm() {
    this.form.reset();
  }

  submitMeasurement() {
    if (this.form.valid) {
      console.log(this.measurement);
    } else {
      console.log('Form is invalid');
      console.log(this.form.errors);
    }
  }

  get measurement(): Measurement {
    const values = this.form.value;
    return {
      cvr: values.cvr1 + '-' + values.cvr2,
      systolic: values.systolic,
      diastolic: values.diastolic,
    };
  }

  private _createFormGroup() {
    this.form = new FormGroup({
      cvr1: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(6),
        Validators.pattern('^[0-9]*$'),
      ]),
      cvr2: new FormControl('', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(4),
        Validators.pattern('^[0-9]*$'),
      ]),
      systolic: new FormControl('', [
        Validators.required,
        Validators.min(0),
        Validators.max(300),
      ]),
      diastolic: new FormControl('', [
        Validators.required,
        Validators.min(0),
        Validators.max(300),
      ]),
    });
  }
}
