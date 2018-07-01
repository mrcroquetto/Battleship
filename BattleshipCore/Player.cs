using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore
{
    public class Player
    {
        public Enumerations.PLAYERSTATE State { get; set; }
        public Board Board = null;
        public int Id { get; set; }

        public Player(int boardSize, int id)
        {
            Board = new Board(boardSize);
            State = Enumerations.PLAYERSTATE.Wait;
            Id = id;
        }
    }
}
