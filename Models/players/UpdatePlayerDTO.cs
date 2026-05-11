namespace king.Models.players
{
    public class UpdatePlayerDTO
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public int Level { get; set; } = 0;
    }

    public class UpdatePlayerNameDTO
    {
        public string? Name { get; set; }
    }

    public class UpdatePlayerLevelDTO
    {
        public int Level { get; set; } = 0;
    }

    public class UpdatePlayerEmailDTO
    {
        public string Email { get; set; }
    }
}
