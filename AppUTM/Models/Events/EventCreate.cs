using Microsoft.AspNetCore.Http;
using System;

namespace AppUTM.Client.Models.Events
{
    public class EventCreate
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Author { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsActivity { get; set; }
        public bool IsSuggest { get; set; }
        public bool IsPublished { get; set; }
    }
}