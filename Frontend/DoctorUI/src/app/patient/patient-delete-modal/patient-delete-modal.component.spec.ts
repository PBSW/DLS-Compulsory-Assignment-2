import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDeleteModalComponent } from './patient-delete-modal.component';

describe('PatientDeleteModalComponent', () => {
  let component: PatientDeleteModalComponent;
  let fixture: ComponentFixture<PatientDeleteModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientDeleteModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
