using System;
using System.ComponentModel.DataAnnotations;

namespace FitBananas.Models
{
    public class Token
    {
        [Key]
        public int TokenId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}