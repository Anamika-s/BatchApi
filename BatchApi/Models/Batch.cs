﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BatchApi.Models
{
    public class Batch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}
