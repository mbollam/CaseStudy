using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CouponManagerAPI
{
    public class Coupon
    {
        #region Properties 
            [Key]
            public int CouponId { get; set; }
            public string CouponTitle { get; set; }
            public string CouponNumber { get; set; }
            public DateTime CouponExpirationDate { get; set; }
            public int CouponDiscount { get; set; }
            public decimal CouponDiscountAmount { get; set; }
            public int CouponsCount { get; set; }
            public int CampaignId { get; set; }
        #endregion

        #region Methods 
        public void CreateOffer() { }
        public void NotifyOffer() { }
        public void AcceptOffer() { }

        #endregion
        
    }
}
