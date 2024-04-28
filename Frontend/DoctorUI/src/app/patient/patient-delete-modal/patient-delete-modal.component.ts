import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-patient-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './patient-delete-modal.component.html',
  styleUrl: './patient-delete-modal.component.scss'
})
export class PatientDeleteModalComponent {

  activeModal = inject(NgbActiveModal);

	@Input() name: string = '';

  constructor() { }

  delete() {
    this.activeModal.close(true);
  }

  cancel() {
    this.activeModal.close(false);
  }
}
