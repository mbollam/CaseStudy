using System;
using System.Collections.Generic;
using System.Text;
using CouponManagerAPI;
using CouponManagerAPI.Controllers.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CouponApi.UnitTests
{
   public class CampaignController : IDisposable
    {
        private readonly ApiContext _context;
        public CampaignController()
        {

            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();


            var builder = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase("CampaignControllerTests")
                .UseInternalServiceProvider(serviceProvider);


            _context = new ApiContext(builder.Options);


            _context.Campaign.AddRange(
                     Enumerable.Range(1, 10).Select(t => new Campaign { CampaignName = "Monsoon offer" + t, CampaignDescription = "Monsoon offer" + t })
                 ); 

            _context.SaveChanges();

        }

        [Fact]
        public async Task GetAll_Returns_GivenValidid()
        {
            var cc = new CampaignsController(_context);
            var result = await cc.GetCampaign();
            var model = Assert.IsAssignableFrom<IEnumerable<Campaign>>(result);
            Assert.Equal(10, model.Count());
        }
        [Fact]
        public async Task GetById_ReturnsCoupon()
        {
            var cc = new CampaignsController(_context);
            var result = await cc.GetCampaign(1);
            var objResult = Assert.IsType<OkObjectResult>(result);

            var Campaign = Assert.IsAssignableFrom<Campaign>(objResult.Value);

            Assert.Equal(1, Campaign.CampaignId);
        }
        [Fact]
        public async Task GetById_ReturnsNotFound_GivenInvalidId()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.GetCampaign(21);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenNullItem()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.CreateCampaign(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var cc = new CampaignsController(_context);
            cc.ModelState.AddModelError("Name", "Required");

            var result = await cc.CreateCampaign(new Campaign());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsNewlyCreatedCampaign()
        {
            var cc = new CampaignsController(_context);
            var Campaign = new Campaign() { CampaignName = "Monsoon offer", CampaignDescription = "Monsoon offer"  };
            var result = await cc.CreateCampaign(Campaign);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.UpdateCampaign(99, new Campaign { CampaignId = 1, CampaignDescription = "Campaign 1" });

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequestWhenCampaignIsInvalid()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.UpdateCampaign(1, null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var cc = new CampaignsController(_context);
            cc.ModelState.AddModelError("Name", "Required");

            var result = await cc.UpdateCampaign(1, new Campaign { CampaignId = 1, CampaignDescription = null });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenIdIsInvalid()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.UpdateCampaign(99, new Campaign { CampaignId = 99, CampaignDescription = "Campaign title" });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenItemUpdated()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.UpdateCampaign(9, new Campaign { CampaignId = 9, CampaignDescription = " Campaign Title update" });

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIsIsInvalid()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.DeleteCampaign(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenItemDeleted()
        {
            var cc = new CampaignsController(_context);

            var result = await cc.DeleteCampaign(2);

            Assert.IsType<OkObjectResult>(result);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
