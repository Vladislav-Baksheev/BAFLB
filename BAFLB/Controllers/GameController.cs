using BAFLB.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAFLB.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly ApplicationContext _db;

        private readonly ILogger<GameController> _logger;

        private readonly Game _game;

        public GameController(ILogger<GameController> logger, ApplicationContext db, Game game)
        {
            _logger = logger;
            _db = db;
            _game = game;
            Console.WriteLine("Создан контроллер");
        }

        [Route("game/users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [Route("game/round")]
        [HttpGet]
        public IActionResult GetRound()
        {
            var round = _db.Rounds.ToList();
            return Ok(round);
        }

        [Route("game/users/{id}")]
        [HttpGet]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _db.Users.FirstOrDefault(p => p.Id == id);
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
            var user = _db.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "Пользователь не найден!"});
            }

            _db.Users.Remove(user);

            _db.SaveChanges();

            return Ok(new {message = "Пользователь успешно удален!"});
        }


        [Route("game/users/add")]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            // Хз канеш может и так норм, но мне кажется проще Ok отправить с текстом.
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [Route("game/start")]
        [HttpGet]
        public IResult StartGame()
        {
            var users = _db.Users.ToList();

            _game.Users = users;
            _game.StartGame();

            //_db.Rounds.Add(_game.Round);

            var round = _db.Rounds.First(r => r.Id == 1);

            round.Name = _game.Round.Name;

            _db.SaveChanges();

            return Results.Ok(new
            {
                Users = users,
                _game.Round
            });
        }

        [Route("game/shoot")]
        [HttpPost]
        public IResult UserShoot([FromBody] int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Results.BadRequest(new {message = "Пользователь не найдем!"});
            }

            try
            {
                _game.UserShoot(user);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }

            return Results.Ok(user);
        }
    }
}
