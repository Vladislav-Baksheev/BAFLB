﻿namespace BAFLB
{
    public class Game
    {
        public List<User> Users { get; set; }
        public Round Round { get; set; }
        public Game() 
        {
            Users = new List<User>();
            Round = new Round();
        }

        public void StartGame()
        {
            foreach(var user in Users)
            {
                user.MaxShot = RandomMaxShot();
            }

            Round.Name = RandomRound();
        }

        public void UserShoot(User user)
        {
            user.CurrentShot++;

            // Лучше сначала сделать проверку на неправильный случай, чтобы уменьшить количество if.
            // Почему в коде это не обрабатывается с помощью try catch.
            // А вообще такой случай лучше обработать на фронте и просто вывести, что чел не может стрелять.
            // Ну и здесь на всяк проверку сделать.
            if(user.CurrentShot > user.MaxShot)
            {
                throw new Exception();
            }

            if (user.CurrentShot == user.MaxShot)
            {
                UserDead(user);
            }
        }

        private void UserDead(User user)
        {
            user.IsDead = true;
        }

        private int RandomMaxShot()
        {
            Random rand = new();

            return rand.Next(1, 6);
        }

        private string RandomRound()
        {
            Random rand = new();
            string[] round = { "ACE", "KINGS", "QUEENS" };
            return round[rand.Next(0, 2)];
        }
    }
}
