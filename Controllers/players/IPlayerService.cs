using king.data;
using king.Models.players;

namespace king.Controllers.players
{
    public interface IPlayerService
    {

        public Task<PlayerSelectsDTO[]> GetAllPlayers(int page , int pageSize);

        public Task<PlayerSelectsDTO?> GetCurrentPlayer(string firebaseUid);

    }
}
