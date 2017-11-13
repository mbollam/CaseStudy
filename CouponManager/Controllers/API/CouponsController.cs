using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CouponManagerAPI;


namespace CouponManagerAPI.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Coupons")]
    public class CouponsController : Controller
    {
        private readonly ApiContext _context;

        public CouponsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Coupons
        [HttpGet]
        public async Task<IEnumerable<Coupon>> GetCoupon()
        {
            return await _context.Coupon.ToListAsync();
        }

        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoupon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coupon = await _context.Coupon.SingleOrDefaultAsync(m => m.CouponId == id);

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // PUT: api/Coupons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon([FromRoute] int id, [FromBody] Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.CouponId)
            {
                return BadRequest();
            }

            _context.Entry(coupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coupons
        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Coupon.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoupon", new { id = coupon.CouponId }, coupon);
        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coupon = await _context.Coupon.SingleOrDefaultAsync(m => m.CouponId == id);
            if (coupon == null)
            {
                return NotFound();
            }

            _context.Coupon.Remove(coupon);
            await _context.SaveChangesAsync();

            return Ok(coupon);
        }

        private bool CouponExists(int id)
        {
            return _context.Coupon.Any(e => e.CouponId == id);
        }

        public Task UpdateCampaign(int v, Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCampaign(int v)
        {
            throw new NotImplementedException();
        }
    }
}