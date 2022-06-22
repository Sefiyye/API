﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName="decimal(7,2")]
        public decimal Price { get; set; }
    }
}