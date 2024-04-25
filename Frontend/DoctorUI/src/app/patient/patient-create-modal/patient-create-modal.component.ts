import { Component, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Patient } from '../../core/domain/domain';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-create-modal',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './patient-create-modal.component.html',
  styleUrl: './patient-create-modal.component.scss'
})
export class PatientCreateModalComponent {
  activeModal = inject(NgbActiveModal);

  formGroup: FormGroup = this._createFromGroup()

  newPatient: Patient = {
    ssn: '',
    name: '',
    mail: '',
    measurements: []
  };

  constructor() {
  }



  _createFromGroup(): FormGroup {
    const regex: RegExp = new RegExp('^(0[1-9]|[12]\\d|3[01])(0[1-9]|1[0-2])\\d{2}[-]?\\d{4}$');

    return new FormGroup({
      ssn: new FormControl('', [Validators.required, Validators.pattern(regex)]),
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }


  create() {
    this.newPatient.ssn = this.formGroup.get('ssn')?.value;
    this.newPatient.name = this.formGroup.get('firstname')?.value + ' ' + this.formGroup.get('lastname')?.value;
    this.newPatient.mail = this.formGroup.get('email')?.value;

    this.activeModal.close(this.newPatient);
  }

  cancel() {
    this.activeModal.dismiss();
  }



}
