namespace PozitronDev.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    public static T? FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, _jsonOptions);

    public static async Task<T?> FromJsonAsync<T>(this HttpResponseMessage response) =>
        (await response.Content.ReadAsStringAsync()).FromJson<T>();

    public static string ToJson<T>(this T obj) =>
        JsonSerializer.Serialize(obj, _jsonOptions);

    public static StringContent ToStringContent<T>(this T obj) =>
       new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");
}
