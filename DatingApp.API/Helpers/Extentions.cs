using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extentions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var opt = new JsonSerializerOptions();
            opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, opt));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static int CalculateAge(this DateTime dateOBirth)
        {
            var age = DateTime.Today.Year - dateOBirth.Year;
            if(dateOBirth.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}