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

  constructor() { }

  _createFromGroup(): FormGroup {
    return new FormGroup({
      ssn: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }


  create() {
    this.activeModal.close(this.newPatient);
  }

  cancel() {
    this.activeModal.dismiss();
  }



}
