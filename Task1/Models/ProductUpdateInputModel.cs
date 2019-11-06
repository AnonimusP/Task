using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Models
{
    [Table("Product")]
    [NotMapped]
    public class ProductUpdateInputModel : Product
    {
        [Required]
        public new Guid Id { get; set; }
    }
}