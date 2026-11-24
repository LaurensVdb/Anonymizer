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