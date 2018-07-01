using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BattleshipCore;

namespace BattleshipAPI.Controllers
{
    public class GameController : ApiController
    {
        // GET api/game
        public Enumerations.GAMESTATE Get()
        {
            return Game.Instance.State;
        }

        [Route("api/game/AddPlayer")]
        [HttpGet]
        public bool AddPlayer(int id)
        {
            if (Game.Instance.State == Enumerations.GAMESTATE.Idle)
            {
                return Game.Instance.AddPlayer(id);
            }
            throw new InvalidOperationException("The game is currently on going. Please wait until the game state turns to IDLE before making a new attempt.");
        }

        [Route("api/game/GetPlayerState")]
        [HttpGet]
        public Enumerations.PLAYERSTATE GetPlayerState(int id)
        {
            if (Game.Instance.Players.Count > 0)
            {
                if (Game.Instance.Players.ContainsKey(id))
                {
                    return Game.Instance.Players[id].State;
                }
                else
                    throw new IndexOutOfRangeException("The specified ID Doesn't correspond to any of the players in this session.");
            }
            throw new FieldAccessException("No Players found");
        }

        [Route("api/game/GetNumberOfPlayers")]
        [HttpGet]
        public int GetNumberOfPlayers()
        {
            return Game.Instance.NumberOfPlayers();
        }

        [Route("api/game/AddShip")]
        [HttpPost]
        public bool AddShip([FromUri]int id, [FromBody]Ship ship)
        {
            if (Game.Instance.State == Enumerations.GAMESTATE.Setup)
            {
                Game.Instance.AddShipForPlayer(id, ship);
                return true;
            }
            throw new InvalidOperationException("You can only add ships to your board once the game is in SETUP state. Please wait for making a new attempt.");
        }

        [Route("api/game/Strike")]
        [HttpPost]
        public Enumerations.OUTCOME Strike([FromUri]int id, [FromBody]Strike strike)
        {
            if (Game.Instance.State == Enumerations.GAMESTATE.Playing)
            {
                return Game.Instance.StrikePlayer(id, strike);
            }
            throw new InvalidOperationException("You can only attack your opponent once the game is in PLAYING state. Please wait for making a new attempt.");
        }
    }
}
