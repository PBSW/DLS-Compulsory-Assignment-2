import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientCreateModalComponent } from './patient-create-modal.component';

describe('PatientCreateModalComponent', () => {
  let component: PatientCreateModalComponent;
  let fixture: ComponentFixture<PatientCreateModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientCreateModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientCreateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
