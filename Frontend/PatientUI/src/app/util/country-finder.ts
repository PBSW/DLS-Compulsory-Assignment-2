export async function getCountry(): Promise<string> {
  const response = await fetch('https://api.ipregistry.co/?key=tryout');
  const json = await response.json();

  const country = json.location.country.name;
  console.log('country', country);
  return country;
}

