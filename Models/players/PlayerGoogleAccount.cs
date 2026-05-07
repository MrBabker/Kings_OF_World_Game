namespace king.Models.players
{
    public class PlayerGoogleAccount
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsGoogleAccount { get; set; } = false;
        public int Level { get; set; } = 0;
    }
}
