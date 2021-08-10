using System;
using System.Collections.Generic;
using AppUTM.Api.DTOS.Coordinations;
using AppUTM.Api.DTOS.Favorites;
using AppUTM.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AppUTM.Api.DTOS.Events
{
    public class EventReturn
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AuthorId { get; set; }
        public CoordinationReturn Author { get; set; }
        public string Image { get; set; }
        public bool IsActivity { get; set; }
        public bool IsSuggest { get; set; }
        public bool IsPublished { get; set; }
        public bool IsRechazed { get; set; }
        public bool IsRevised { get; set; }
        public List<FavoriteReturn> Favorites { get; set; }
    }
}