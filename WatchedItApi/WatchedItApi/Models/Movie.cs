﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WatchedItApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public int ExternId { get; set; }
        
        public int MovieListId { get; set; }

        public bool movie { get; set; }

        public int voteCount { get; set; }
    }
}
