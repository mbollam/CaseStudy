using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CouponManagerAPI.Models
{
    public class CouponNew
    {
        #region Properties 
        [Key]
        public int id { get;  }
        [Required]
        public string code { get; set; }        
        public string amount { get; set; }     
        public DateTime date_created { get; set; }
        public DateTime date_modified { get; set; }
        public enum discount_type { percent, fixed_cart}
        public string description { get; set; }
        public DateTime date_expires { get; set; }
        public int usage_count { get; set; }
        public bool individual_use { get; set; }
        public int usage_limit { get; set; }
        public int usage_limit_per_user { get; set; }
        public int limit_usage_to_x_items { get; set; }
        public bool free_shipping { get; set; }
        public string minimum_amount { get; set; }
        public string maximum_amount { get; set; }
        public string[] email_restrictions { get; set; }
        public string[] used_by { get; set; }
        #endregion
    }
}
