namespace king.Models.players
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsGoogleAccount { get; set; } = false;
        public int Level { get; set; } = 0;

    }
}
