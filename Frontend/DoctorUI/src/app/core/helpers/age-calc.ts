export function calcAge(ssn: string): number {
  const dob = ssn.substring(0, 6);
  const dobDay = parseInt(dob.substring(0, 2));
  const dobMonth = parseInt(dob.substring(2, 4))-1;
  const dobYear = parseInt(dob.substring(4, 6));
  const dobFullYear = dobYear < 20 ? 2000 + dobYear : 1900 + dobYear;

  let dobDate = new Date();
  dobDate.setFullYear(dobFullYear, dobMonth, dobDay);
  const ageDifMs = Date.now() - dobDate.getTime();
  const ageDate = new Date(ageDifMs);
  return Math.abs(ageDate.getUTCFullYear() - 1970);
}
