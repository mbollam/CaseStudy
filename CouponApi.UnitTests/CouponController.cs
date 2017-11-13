using CouponManagerAPI;
using CouponManagerAPI.Controllers.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CouponApi.UnitTests
{
    public class CouponController: IDisposable
    {
        private readonly ApiContext _context;
        public CouponController() 
        {
            
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

        
        var builder = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("CouponControllerTests")
            .UseInternalServiceProvider(serviceProvider);

        
        _context = new ApiContext(builder.Options);


            _context.Coupon.AddRange(
                    Enumerable.Range(1, 10).Select(t => new Coupon { CampaignId = t, CouponsCount = 10 + t, CouponDiscount = 5 + t, CouponDiscountAmount = 200 + t, CouponExpirationDate = DateTime.Now.AddDays(t), CouponNumber = "MONSOON1" + t, CouponTitle = "Monsoon offer Flat 10" + t + "%" })
                );

            _context.SaveChanges();

        }
        
        [Fact]
        public async Task GetAll_ReturnsCoupon_GivenValidid()
        {                      
            var cc = new CouponsController(_context);
            var result = await cc.GetCoupon();
            var model = Assert.IsAssignableFrom<IEnumerable<Coupon>>(result);
            Assert.Equal(10, model.Count());
        }
        [Fact]
        public async Task GetById_ReturnsCoupon()
        {
            var cc = new CouponsController(_context);
            var result =await cc.GetCoupon(1);
            var objResult = Assert.IsType<OkObjectResult>(result);

            var coupon = Assert.IsAssignableFrom<Coupon>(objResult.Value);

            Assert.Equal(1, coupon.CouponId);
        }
        [Fact]
        public async Task GetById_ReturnsNotFound_GivenInvalidId()
        {
            var cc = new CouponsController(_context);

            var result = await cc.GetCoupon(21);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenNullItem()
        {
            var cc = new CouponsController(_context);

            var result = await cc.CreateCoupon(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var cc = new CouponsController(_context);
            cc.ModelState.AddModelError("Name", "Required");

            var result = await cc.CreateCoupon(new Coupon());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsNewlyCreatedCoupon()
        {
            var cc = new CouponsController(_context);
            var coupon = new Coupon() { CampaignId = 22, CouponsCount = 10, CouponDiscount = 5, CouponDiscountAmount = 200, CouponExpirationDate = DateTime.Now.AddDays(1), CouponNumber = "MONSOON", CouponTitle = "Monsoon offer Flat 10%" };
            var result = await cc.CreateCoupon(coupon);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var cc = new CouponsController(_context);

            var result = await cc.UpdateCoupon(99, new Coupon { CouponId = 1, CouponTitle = "Coupon 1" });

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequestWhenCouponIsInvalid()
        {
            var controller = new CouponsController(_context);

            var result = await controller.UpdateCoupon(1, null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var cc = new CouponsController(_context);
            cc.ModelState.AddModelError("Name", "Required");

            var result = await cc.UpdateCoupon(1, new Coupon { CouponId = 1, CouponTitle = null });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenIdIsInvalid()
        {
            var cc = new CouponsController(_context);

            var result = await cc.UpdateCoupon(99, new Coupon { CouponId = 99, CouponTitle = "Coupon title" });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenItemUpdated()
        {
            var cc = new CouponsController(_context);

            var result = await cc.UpdateCoupon(9, new Coupon { CouponId = 9, CouponTitle = "Coupon Title update"});

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIsIsInvalid()
        {
            var cc = new CouponsController(_context);

            var result = await cc.DeleteCoupon(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenItemDeleted()
        {
            var cc = new CouponsController(_context);

            var result = await cc.DeleteCoupon(2);

            Assert.IsType<OkObjectResult>(result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
