﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Api.Models
{

    [Table("MovieCategory")]
    public class MovieCategory
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
