using System.Text.Json;
using AppClient.Api.Models;

namespace AppClient.Api.Extensions
{
    public static class Pagination
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPPage, int totalItems, int totalPages)
        {
            var opts = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var pagination = new PaginationHeader(currentPage, itemsPPage, totalItems, totalPages);
            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagination, opts));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}