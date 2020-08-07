using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Models;
using PrsServer.Utility;

namespace PrsServer.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {

        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context) {
            _context = context;
        }

        // GET: api/Requests/Review/5
        [HttpGet("review/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsToBeReviewed(int userId) {
            return await _context.Requests
                .Where(r => r.Status.Equals(RequestStatus.Review) && r.UserId != userId)
                .ToListAsync();
        }

        // PUT: api/Requests/Review
        [HttpPut("review")]
        public async Task<IActionResult> PutStatusReview(Request request) {
            if(!await UpdateStatus(request, RequestStatus.Review)) {
                return NotFound();
            }
            return NoContent();
        }
        // PUT: api/Requests/Approve
        [HttpPut("approve")]
        public async Task<IActionResult> PutStatusApprove(Request request) {
            if(!await UpdateStatus(request, RequestStatus.Approved)) {
                return NotFound();
            }
            return NoContent();
        }
        // PUT: api/Requests/Reject
        [HttpPut("reject")]
        public async Task<IActionResult> PutStatusReject(Request request) {
            if(!await UpdateStatus(request, RequestStatus.Rejected)) {
                return NotFound();
            }
            return NoContent();
        }
        private async Task<bool> UpdateStatus(Request request, string status) {
            //var requestdb = await _context.Requests.FindAsync(request.Id);
            if(request == null) {
                return false;
            }
            _context.Entry(request).State = EntityState.Modified;

            // when setting the request to review status, if the total
            // is less than or equal to 50, set the status to approved
            request.Status = status;
            if(request.Total <= 50 && status == RequestStatus.Review) {
                request.Status = RequestStatus.Approved;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests() {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id) {
            var request = await _context.Requests.FindAsync(id);

            if(request == null) {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request) {
            if(id != request.Id) {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!RequestExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request) {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id) {
            var request = await _context.Requests.FindAsync(id);
            if(request == null) {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id) {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
