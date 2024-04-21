import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MeasurementComponent } from "./measurement/measurement.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, MeasurementComponent]
})
export class AppComponent {
  title = 'PatientUI';
}
