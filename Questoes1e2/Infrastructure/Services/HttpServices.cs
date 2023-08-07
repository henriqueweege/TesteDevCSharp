using Domain.DTO;
using System.Net.Http.Json;

namespace Infrastructure.Services;

public static class HttpServices
{
    public static FootBallMatchesDTO FetchFootBallMatchesData(string url)
        => new HttpClient().GetFromJsonAsync<FootBallMatchesDTO>(url).Result;
}