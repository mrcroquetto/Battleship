using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore
{
    public class Game
    {
        private static Game instance;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }

        const int BoardSize = 10;
        const int MaxPlayers = 2;

        public Enumerations.GAMESTATE State { get; set; }
        public Dictionary<int, Player> Players { get; set; }

        public Game()
        {
            Reset();
        }

        public bool AddPlayer(int id)
        {
            if (Players.Count < 2 && State == Enumerations.GAMESTATE.Idle)
            {
                if (Players.ContainsKey(id))
                    throw new ArgumentException("The ID provided already exists in this game session.");

                Players.Add(id, new Player(BoardSize, id));
                if (Players.Count == 2)
                    State = Enumerations.GAMESTATE.Setup;

                return true;
            }
            return false;
        }

        public int NumberOfPlayers()
        {
            if (Players != null)
                return Players.Count;
            else
                return 0;
        }

        public void Reset()
        {
            State = Enumerations.GAMESTATE.Idle;
            if (Players != null)
                Players.Clear();
            else
                Players = new Dictionary<int, Player>();
        }

        public bool AddShipForPlayer(int id, Ship ship)
        {
            if (!Players.ContainsKey(id))
                throw new IndexOutOfRangeException("The specified ID Doesn't correspond to any of the players in this session.");

            if (Players[id].Board.RemainingShipUnits > 0)
            {
                Players[id].Board.AddShip(ship);
                if (CheckSetupEnd())
                {
                    State = Enumerations.GAMESTATE.Playing;
                    Players[id].State = Enumerations.PLAYERSTATE.Play;
                }
                return true;
            }
            throw new Exception($"Player {id} cannot add more ships. Please wait until all players are ready to play and the game state is PLAYING.");
        }

        private bool CheckSetupEnd()
        {
            int playersReady = 0;
            foreach (KeyValuePair<int, Player> p in Players)
            {
                if (p.Value.Board.RemainingShipUnits == 0)
                    playersReady++;
            }
            if (playersReady == Players.Count)
                return true;
            else
                return false;
        }

        public Enumerations.OUTCOME StrikePlayer(int attackingPlayer, Strike strike)
        {
            if (!Players.ContainsKey(attackingPlayer))
                throw new IndexOutOfRangeException("The specified ID doesn't correspond to any of the players in this session.");
            if (!Players.ContainsKey(strike.InflictedPlayerId))
                throw new IndexOutOfRangeException("The ATTACKED ID doesn't correspond to any of the players in this session.");
            if (Players[attackingPlayer].State != Enumerations.PLAYERSTATE.Play)
                throw new Exception("You are not in your attacking turn. Please wait until your state is PLAY.");

            Enumerations.OUTCOME result =  Players[strike.InflictedPlayerId].Board.Strike(new KeyValuePair<int, int>(strike.CoordinateX, strike.CoordinateY));
            Players[attackingPlayer].State = Enumerations.PLAYERSTATE.Wait;
            Players[strike.InflictedPlayerId].State = Enumerations.PLAYERSTATE.Play;
            if(result == Enumerations.OUTCOME.Won)
            {
                Reset();
            }
            return result;
        }
    }
}
