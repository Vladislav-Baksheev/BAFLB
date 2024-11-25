using BAFLB.Data;
using Microsoft.AspNetCore.Mvc;

namespace BAFLB.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private Game game = new Game();

        private ApplicationContext context = new ApplicationContext();

        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [Route("game/users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = context.Users;
            return Ok(users);
        }

        [Route("game/round")]
        [HttpGet]
        public IActionResult GetRound()
        {
            var round = context.Rounds;
            return Ok(round);
        }

        [Route("game/users/{id}")]
        [HttpGet]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("game/users/delete/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);

            context.SaveChanges();

            return Ok();
        }


        [Route("game/users/add")]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [Route("game/start")]
        [HttpPost]
        public async Task StartGame(User[] users)
        {
            //string updateSQL = "UPDATE users set MaxShot = @newMaxShot WHERE Name = @name;";
            
            game.Users = users.ToList();
            game.StartGame();

            var resultRound = (from r in context.Rounds where (r.Id == 1) select r).FirstOrDefault();

            if (resultRound != null)
            {
                resultRound.Name = game.Round.Name;

                context.SaveChanges();
            }

            foreach (var user in game.Users)
            {
                var resultUsers = (from r in context.Users where (r.Id == user.Id) select r).FirstOrDefault();

                if(resultUsers != null)
                {
                    resultUsers.CurrentShot = 0;

                    resultUsers.IsDead = false;

                    resultUsers.MaxShot = user.MaxShot;

                    context.SaveChanges();
                }
            }
        }

        [Route("game/shoot")]
        [HttpPost]
        public async Task UserShoot(User user)
        {
            game.UserShoot(user);

            var result = (from r in context.Users where (r.Id == user.Id) select r).FirstOrDefault();

            if (result != null)
            {
                result.CurrentShot = user.CurrentShot;

                result.IsDead = user.IsDead;

                context.SaveChanges();
            }
        }
    }
}
