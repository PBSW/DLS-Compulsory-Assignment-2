export interface Patient {
  ssn: string;
  name: string;
  mail: string;
  measurements: Measurement[];
}

export interface Measurement {
  id: number;
  date: Date;
  systolic: number;
  diastolic: number;
  seen: boolean;
  measurementGrade: MeasurementGrade;
}

export enum MeasurementGrade {
  NORMAL = 'NORMAL',
  WARNING = 'WARNING',
  DANGER = 'DANGER'
}
