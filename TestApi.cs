using System.Text;

var client = new HttpClient();

var content = new StringContent(
    "\"hallo ik ben oskar , mijn nummer 045445788899, mijn rijksregisternummer 4545866699\"",
    Encoding.UTF8,
    "application/json");

var response = await client.PostAsync(
    "https://localhost:5001/anonymize",
    content);
Console.WriteLine(await response.Content.ReadAsStringAsync());

content = new StringContent(
   "\"hallo ik ben oskar, ik woon in brussel de kerkstraat 1.\"",
   Encoding.UTF8,
   "application/json");

response = await client.PostAsync(
   "https://localhost:5001/anonymize",
   content);
Console.WriteLine(await response.Content.ReadAsStringAsync());

content = new StringContent(
   "\"hallo ik ben oskar , mijn nummer 045445788899, mijn rijksregisternummer 4545866699\"",
   Encoding.UTF8,
   "application/json");

response = await client.PostAsync(
   "https://localhost:5001/assess-text-trustworthiness",
   content);

Console.WriteLine(await response.Content.ReadAsStringAsync());

