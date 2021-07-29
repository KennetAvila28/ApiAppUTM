using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    [Table("Events")]
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Coordinations")]
        public int Author { get; set; }

        public string Image { get; set; }
        public bool IsActivity { get; set; }
        public bool IsSuggest { get; set; }
        public bool IsPublished { get; set; }
        public IList<EventFavorite> EventFavorite { get; set; }
    }
}