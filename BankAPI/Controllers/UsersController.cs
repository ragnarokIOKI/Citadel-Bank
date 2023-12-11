using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Models;
using Microsoft.IdentityModel.Tokens;
using RailwayAPI;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public UsersController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("/LogIn")]
        public IActionResult Logg(string login, string password)
        {
            HashSalt hashingcs = new HashSalt();
            if (_context.Users == null)
            {
                return NotFound();
            }

            object response = null;

            var users = _context.Users.ToList();
            foreach (var user in _context.Users)
            {
                if (user.Login == login.Trim())
                {
                    bool b = false;
                    b = hashingcs.AreEqual(password, user.Password, user.Salt);
                    if (b == true)
                    {
                        var identity = GetIdentity(login, user);
                        if (identity == null)
                        {
                            return BadRequest(new { errorText = "Invalid username or password." });
                        }

                        var now = DateTime.UtcNow;
                        var jwt = new JwtSecurityToken(
                        issuer: AuthToken.ISSUER,
                        audience: AuthToken.AUDIENCE,
                        notBefore: now,
                    claims: identity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        return Ok(new { access_token = "Bearer " + encodedJwt, username = login, id = user.IdUser, role = user.RoleId });

                    }
                    else
                    {
                        return Ok("Неуспешная авторизация!");
                    }
                }
            }


            if (response == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(response);
            }
        }

        private ClaimsIdentity GetIdentity(string username, User user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpPost("/Authorize")]
        public async Task<ActionResult<User>> AuthUser(User user)
        {
            HashSalt h = new HashSalt();

            var pass = user.Password;
            var salt = h.CreateSalt(10);
            var hashedpass = h.GenerateHash(pass, salt);
            if (_context.Users == null)
            {
                return Problem("Entity set 'Railway_DatabaseAPIContext.Users'  is null.");
            }
            _context.Users.Add(user);
            if (h.AreEqual(pass, hashedpass, salt))
            {
                user.Password = hashedpass;
                user.Salt = salt;
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult> RefreshToken(string access_token)
        {
            var login = GetTokenInfo(access_token);
            User user = _context.Users.FirstOrDefault(x => x.Login == login);

            var identity = GetIdentityRefresh(user.Login, user.Password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создание JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthToken.ISSUER,
                    audience: AuthToken.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                id = user.IdUser
            };

            return Ok(response);
        }

        private string GetTokenInfo(string token)
        {
            var t = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return t.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
        }

        private ClaimsIdentity GetIdentityRefresh(string username, string password)
        {
            User person = _context.Users.FirstOrDefault(x => x.Login == username && x.Password == password.ToString());
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.RoleId.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(bool? orderBy = null, string? search = null)
        {
            IQueryable<User> query = _context.Users.AsQueryable();

            if (search != null)
            {
                query = query.Where(u => u.FirstName.Contains(search));
            }

            if (orderBy != null)
            {
                query = query.Where(prsn => prsn.UserDeleted == orderBy);
            }

            var Users = await query.ToListAsync();

            if (!Users.Any())
            {
                return NotFound();
            }

            return Ok(Users);
        }

        public class IndexViewModel
        {
            public IEnumerable<User> Users { get; set; }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                HashSalt h = new HashSalt();

                var pass = user.Password;
                var salt = h.CreateSalt(10);
                var hashedpass = h.GenerateHash(pass, salt);
                if (h.AreEqual(pass, hashedpass, salt))
                {
                    user.Password = hashedpass;
                    user.Salt = salt;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/ChangePassword
        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> UpdateUserPassword(int id, string oldPassword, string newPassword)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            HashSalt h = new HashSalt();
            bool isValidPassword = h.AreEqual(oldPassword, user.Password, user.Salt);
            if (!isValidPassword)
            {
                return Unauthorized();
            }

            var salt = h.CreateSalt(10);
            var hashedpass = h.GenerateHash(newPassword, salt);

            if (h.AreEqual(newPassword, hashedpass, salt))
            {
                user.Password = hashedpass;
                user.Salt = salt;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("");
            }
        }


        [HttpPut("UsersIDDelete")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteIds(int[] id)
        {
            foreach (int userId in id)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    user.UserDeleted = false;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }

            }

            return await _context.Users.ToListAsync();
        }

        [HttpPut("UsersIDReturn")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<User>>> ReturnIds(int[] id)
        {
            foreach (int userId in id)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    user.UserDeleted = true;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }

            return await _context.Users.ToListAsync();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}