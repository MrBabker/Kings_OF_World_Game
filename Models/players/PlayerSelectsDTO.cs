namespace king.Models.players
{
    public class PlayerSelectsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Level { get; set; } = 0;
    }
}
