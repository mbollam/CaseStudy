using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CouponManagerAPI
{
    public class Campaign
    {
        #region Properties
        
        public int CampaignId { get; set; }
        public string CampaignName { get; set;}
        public string CampaignDescription { get; set; }

        #endregion

        #region Methods
        public void CreateCampaign() { }
        #endregion
    }
}
