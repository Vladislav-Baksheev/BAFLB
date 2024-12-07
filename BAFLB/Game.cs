namespace BAFLB
{
    /// <summary>
    /// Содержит логику ведения игры.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Раунд.
        /// </summary>
        public Round Round { get; set; }

        /// <summary>
        /// Создает экземпляр класса <see cref="Game"/>
        /// </summary>
        public Game() 
        {
            Users = new List<User>();
            Round = new Round();
        }

        /// <summary>
        /// Начинает игру.
        /// </summary>
        public void StartGame()
        {
            foreach(var user in Users)
            {
                user.MaxShot = RandomMaxShot();
                user.CurrentShot = 0;
                user.IsDead = false;
            }

            Round.Name = RandomRound();
        }

        /// <summary>
        /// Обрабатывает выстрел пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void UserShoot(User user)
        {
            user.CurrentShot++;

            // А вообще такой случай лучше обработать на фронте и просто вывести, что чел не может стрелять.
            if(user.CurrentShot > user.MaxShot)
            {
                throw new Exception();
            }

            if (user.CurrentShot == user.MaxShot)
            {
                UserDead(user);
            }
        }

        /// <summary>
        /// Обрабатывает событие смерти пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        private void UserDead(User user)
        {
            user.IsDead = true;
        }

        /// <summary>
        /// Случайно выбирает максимальный выстрел.
        /// </summary>
        /// <returns>Максимальный выстрел.</returns>
        private int RandomMaxShot()
        {
            Random rand = new();

            return rand.Next(1, 6);
        }

        /// <summary>
        /// Случайно выбирает текущий раунд.
        /// </summary>
        /// <returns>Раунд.</returns>
        private string RandomRound()
        {
            Random rand = new();
            string[] round = { "ACES", "KINGS", "QUEENS" };
            return round[rand.Next(0, 3)];
        }
    }
}
