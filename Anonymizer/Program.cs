using Anonymizer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<AnonymizerService>();
var app = builder.Build();



app.MapPost("/anonymize", ([FromBody] string input, [FromServices] AnonymizerService service) =>
    service.AnonymizeAsync(input));

app.MapPost("/assess-text-trustworthiness", ([FromBody] string input, [FromServices] AnonymizerService service) =>
    service.AssessTextTrustworthinessAsync(input));


app.Run();





