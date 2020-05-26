using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_web_api.Models;

namespace my_web_api.Controllers
{
  [ApiController]
  [Route("worn")]
  public class WornUserController : ControllerBase
  {
    private static readonly HttpClient client = new HttpClient();

    private readonly MyWebApiContext _context;

    public WornUserController(MyWebApiContext context)
    {
      _context = context;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
      var users = _context.Users.ToList();
      return Ok(users);
    }

    [HttpGet("users/{id}", Name = "GetById")]
    public IActionResult GetById(string id)
    {
      var user = _context.Find<User>(int.Parse(id));

      return Ok(user);
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
      _context.Add(user);

      _context.SaveChanges();

      return CreatedAtRoute("GetById", new { id = user.UserId }, user);
    }

    [HttpPatch("users/patch/{id}")]
    public IActionResult Patch(string id, [FromBody] User user)
    {
      if (user == null)
      {
        return BadRequest();
      }

      if (ModelState.IsValid)
      {
        User userToUpdate = _context.Find<User>(int.Parse(id));

        if (userToUpdate != null)
        {
          user.UserId = int.Parse(id);

          // TODO need to figure out how to update just the values offered by the user? or maybe just figure out a way of validating that
          // TODO on the client side
          _context.Entry(userToUpdate).CurrentValues.SetValues(user);

          _context.Update(userToUpdate);

          _context.SaveChanges();

          return CreatedAtRoute("GetById", new { id = userToUpdate.UserId }, userToUpdate);
        }

        return NotFound();
      }



      return null;
    }

  }
}
