using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    
    public class SystemsController : ControllerBase {

        private readonly PrsDbContext _context;

        public SystemsController(PrsDbContext context) {
            _context = context;
        }

        [HttpGet("init")]
        public async Task<string> GetInit() {
            var messages = new List<string>();
            var users = await _context.Users.ToListAsync();
            var vendorsCount = _context.Vendors.CountAsync().Result;
            var productsCount = _context.Products.CountAsync().Result;
            var requestsCount = _context.Requests.CountAsync().Result;
            messages.Add($"User has {users.Count} instances.");
            messages.Add($"Vendor has {vendorsCount} instances.");
            messages.Add($"Product has {productsCount} instances.");
            messages.Add($"Request has {requestsCount} instances.");
            messages.Add("*-Users--------------*");
            users.ForEach(u => messages.Add($"{u.Username} | {u.Password}"));
            messages.Add("Prs Server is running ...");
            var message = string.Join('\n', messages);
            return message;
        }
    }
}