import { Component } from '@angular/core';
import { MeasurementsComponent } from "./measurements/measurements.component";
import { PatientComponent } from "./patient/patient.component";

@Component({
    selector: 'app-patient-view',
    standalone: true,
    templateUrl: './patient-view.component.html',
    styleUrl: './patient-view.component.scss',
    imports: [MeasurementsComponent, PatientComponent]
})
export class PatientViewComponent {

}
