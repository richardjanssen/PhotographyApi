using Newtonsoft.Json;

namespace Functional.Test.Support.Extensions;

public static class ResponseExtensions
{
    public static async Task<T?> ParseTo<T>(this HttpResponseMessage responseMessage)
    {
        var s = await responseMessage.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(s);
    }
}
