using System.Text.Json;

namespace AppClient.Api.Models
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItemsPPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public PaginationHeader(int currentPage, int itemsPPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPPage = itemsPPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}