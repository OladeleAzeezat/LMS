using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSData.Models
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        [Required]
        public LibraryAsset LibraryAsset { get; set; }
        [Required]
        public LibraryCard LibraryCard { get; set; }
        [Required]
        public DateTime ChekedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
    }
}
