using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore
{
    public class Enumerations
    {
        public enum GAMESTATE
        {
            Idle,
            Setup,
            Playing
        }

        public enum PLAYERSTATE
        {
            Wait,
            Play
        }

        public enum ORIENTATION
        {
            Vertical,
            Horizontal
        }

        public enum OUTCOME
        {
            Hit,
            Miss,
            Invalid,
            Won
        }
    }
}
