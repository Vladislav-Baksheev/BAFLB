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

        private Game _game = new Game();

        public GameController(ILogger<GameController> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
            Console.WriteLine("������ ����������");
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
            var round = _db.Rounds;
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
                // ����� ���������� ����� � ������� � �� ������� ������� ���� ������ � �������� ������.
                return NotFound(new { message = "������������ �� ������!"});
            }

            _db.Users.Remove(user);

            _db.SaveChanges();

            // �� ��, ��� � ������
            return Ok(new {message = "������������ ������� ������!"});
        }


        [Route("game/users/add")]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            // �� ����� ����� � ��� ����, �� ��� ������� ����� Ok ��������� � �������.
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // ��� ������� ��� ������ ���� �� ������ �� ���� �������� �������������, � �� � ������� ���������.
        [Route("game/start")]
        [HttpGet]
        public IResult StartGame()
        {
            //string updateSQL = "UPDATE users set MaxShot = @newMaxShot WHERE Name = @name;";

            /*_game.Users = users.ToList();*/
            var users = _db.Users.ToList();

            _game.Users = users;
            _game.StartGame();

            _db.Rounds.Add(_game.Round);

            _db.SaveChanges();

            return Results.Ok(new
            {
                Users = users,
                _game.Round
            });


            // ��������� ��� � �������.
            /*var resultRound = (from r in _db.Rounds where (r.Id == 1) select r).FirstOrDefault();*/

            // � ���� ����� Null ����� ��� ������ ������, ��� ���� �� ��������
            // ���� ��������� � ����� ������ ������.
            /*if (resultRound != null)
            {
                resultRound.Name = _game.Round.Name;

                _db.SaveChanges();
            }*/

            /*foreach (var user in _game.Users)
            {
                // ��� ����
                var resultUsers = (from r in _db.Users where (r.Id == user.Id) select r).FirstOrDefault();

                if(resultUsers != null)
                {
                    resultUsers.CurrentShot = 0;

                    resultUsers.IsDead = false;

                    resultUsers.MaxShot = user.MaxShot;

                    _db.SaveChanges();
                }
            }*/
        }

        [Route("game/shoot")]
        [HttpPost]
        public IResult UserShoot([FromBody] int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Results.BadRequest(new {message = "������������ �� ������!"});
            }

            _game.UserShoot(user);
            _db.SaveChanges();

///*//            /*var result = (from r in context.Users where (r.Id == user.Id) select r).FirstOrDefault();*//*
////            var result = _db.Users.FirstOrDefault(r => r.Id == user.Id);*//*
/////*
////            if (result != null)
////            {
////                result.CurrentShot = user.CurrentShot;

////                result.IsDead = user.IsDead;

////                _db.SaveChanges();
////            }*/
/////*
////            var responseUser = _db.Users.FirstOrDefault(r => r.Id == user.Id*//*);*/*/*/

            return Results.Ok(user);
        }
    }
}
