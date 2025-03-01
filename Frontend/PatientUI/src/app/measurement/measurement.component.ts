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
import { MeasurementService } from '../../services/measurement.service';
import { take, pipe } from 'rxjs';
import { ToastService } from '../../services/toasts/toast.service';
import { FeatureHub, StrategyAttributeCountryName } from 'featurehub-javascript-client-sdk';
import { getCountry } from '../util/country-finder';

@Component({
  selector: 'app-measurement',
  standalone: true,
  imports: [ReactiveFormsModule, NgbModalModule, CommonModule],
  templateUrl: './measurement.component.html',
  styleUrl: './measurement.component.scss',
})
export class MeasurementComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  isInAllowedCountry = true;

  constructor(
    private measurementService: MeasurementService,
    private toasts: ToastService
  ) {
    this._createFormGroup();
    this.isDenmarkOnlyEnabledInFeatureFlag();
  }

  async ngOnInit() {
    await this.isDenmarkOnlyEnabledInFeatureFlag();
    await this.isInAllowedCountryFlag();
  }

  async isDenmarkOnlyEnabledInFeatureFlag() {
    FeatureHub.feature('dk_only').addListener((featureState) => {
      if (featureState != null) {

        const dkOnly = featureState.getFlag();
        console.log('dkOnly', dkOnly);
        if (dkOnly) {
          this.isInAllowedCountry = dkOnly;
        } else {
          this.isInAllowedCountry = true; // default to true
        }
      }
    });
  }

  async isInAllowedCountryFlag() {
    const country = await getCountry();
    const strategyAttributeCountryName = this._findStrategyAttributeCountryName(country.toLowerCase());
    console.log('strategyAttributeCountryName', strategyAttributeCountryName);
    const client = FeatureHub.context.country(StrategyAttributeCountryName.Denmark);
    const dkOnly = await client.feature('dk_only').getFlag();
    console.log('dkOnly', dkOnly);
    if (dkOnly) {
      this.isInAllowedCountry = dkOnly;
    } else {
      this.isInAllowedCountry = true; // default to true
    }
  }

  private _findStrategyAttributeCountryName(country: string): StrategyAttributeCountryName {
    return StrategyAttributeCountryName[country as keyof typeof StrategyAttributeCountryName];
  }

  resetForm() {
    this.form.reset();
    this.toasts.showInfoNoHeader('Form reset');
  }

  submitMeasurement() {
    if (this.form.valid) {
      this.measurementService
        .postMeasurement(this.measurement)
        .pipe(take(1))
        .subscribe((response) => {
          if (response) {
            this.toasts.showSuccessNoHeader('Measurement posted successfully');
          }
        });
    } else {
      this.toasts.showErrorNoHeader('Form is invalid');
    }
  }

  get measurement(): Measurement {
    const values = this.form.value;
    return {
      cvr: values.cvr1 + values.cvr2 + '',
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
