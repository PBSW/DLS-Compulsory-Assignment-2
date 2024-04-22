import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MeasurementComponent } from "./measurement/measurement.component";
import { ToastComponent } from "../services/toasts/toast.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, MeasurementComponent, ToastComponent]
})
export class AppComponent {
  title = 'PatientUI';
}
