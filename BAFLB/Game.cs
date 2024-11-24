namespace BAFLB
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
            if(user.CurrentShot == user.MaxShot)
            {
                UserDead(user);
            }
            if(user.CurrentShot > user.MaxShot)
            {
                throw new Exception();
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
