# Anonymizer

A small web API that uses an Ollama model to replace personal information in text with labels such as `[PERSON]` and `[EMAIL]`.

## Run

Set the Ollama endpoint and model in `Anonymizer/appsettings.json`, make sure Ollama is running with that model, then run:

```powershell
dotnet run --project Anonymizer/Anonymizer.csproj
```

Send a `POST` request to `/anonymize` with a JSON string as the body. The API returns the anonymized text.

Send a `POST` request to `/assess-text-trustworthiness` with a JSON string as the body to assess the text's claims. The API returns an assessment of how well the claims are supported by evidence in the provided text, including unsupported or uncertain claims and what additional evidence could help.
