using System;
using System.Collections.Generic;

namespace BattleshipCore
{
    public class Board
    {
        const int TotalShipUnits = 3;
        private int BoardSize { get; set; }
        private int AlivePoints { get; set; }

        public int RemainingShipUnits { get; set; }
        public int[,] BoardMap = null;  // i: Vertical, j: Horizontal



        public Board(int boardSize)
        {
            BoardSize = boardSize;
            AlivePoints = 0;
            RemainingShipUnits = TotalShipUnits;
            BoardMap = new int[BoardSize, BoardSize];
            ResetBoard();
        }


        public bool AddShip(Ship ship)
        {
            if (!IsValidCoordinate(new KeyValuePair<int, int>(ship.CoordinateX, ship.CoordinateY)))
                return false;

            if (ship.Orientation == Enumerations.ORIENTATION.Vertical)
            {
                if (ship.CoordinateX + ship.Size > BoardSize)
                    return false;

                for (int i = 0; i < ship.Size; i++)
                {
                    if (BoardMap[ship.CoordinateX + i, ship.CoordinateY] != 0)
                        return false;
                }
                for (int i = 0; i < ship.Size; i++)
                {
                    BoardMap[ship.CoordinateX + i, ship.CoordinateY] = 1;
                    AlivePoints++;
                    RemainingShipUnits--;
                }
                return true;
            }
            else if (ship.Orientation == Enumerations.ORIENTATION.Horizontal)
            {
                if (ship.CoordinateY + ship.Size > BoardSize)
                    return false;

                for (int i = 0; i < ship.Size; i++)
                {
                    if (BoardMap[ship.CoordinateX, ship.CoordinateY + i] != 0)
                        return false;
                }
                for (int i = 0; i < ship.Size; i++)
                {
                    BoardMap[ship.CoordinateX, ship.CoordinateY + i] = 1;
                    AlivePoints++;
                    RemainingShipUnits--;
                }
                return true;
            }
            return false;
        }


        public void ResetBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    BoardMap[i, j] = 0;
                }
            }
        }


        public int CheckAlivePoints()
        {
            return AlivePoints;
        }


        public bool IsValidCoordinate(KeyValuePair<int, int> coordinate)
        {
            if (coordinate.Key >= BoardSize || coordinate.Value >= BoardSize || coordinate.Key < 0 || coordinate.Value < 0 
                || BoardMap[coordinate.Key, coordinate.Value] == 2)
                return false;
            else
                return true;
        }


        public Enumerations.OUTCOME Strike(KeyValuePair<int, int> coordinate)
        {
            if (!IsValidCoordinate(coordinate))
                return Enumerations.OUTCOME.Invalid;
            else
            {
                if (BoardMap[coordinate.Key, coordinate.Value] == 0)
                    return Enumerations.OUTCOME.Miss;

                BoardMap[coordinate.Key, coordinate.Value] = 2;
                AlivePoints--;
                if (AlivePoints <= 0)
                    return Enumerations.OUTCOME.Won;
                else
                    return Enumerations.OUTCOME.Hit;
            }
        }
    }
}
