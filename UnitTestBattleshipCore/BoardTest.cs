using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipCore;

namespace UnitTestBattleshipCore
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestInit()
        {
            Board b = new Board(10);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.AreEqual(b.BoardMap[i, j], 0);
                }
            }
        }

        [TestMethod]
        public void TestResetBoard()
        {
            Board b = new Board(10);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    b.BoardMap[i, j] = 1;
                }
            }

            b.ResetBoard();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.AreEqual(b.BoardMap[i, j], 0);
                }
            }
        }

        [TestMethod]
        public void TestAddShip()
        {
            Board b = new Board(10);

            Assert.IsTrue(b.AddShip(new Ship(3, 0, 0, Enumerations.ORIENTATION.Horizontal)));
            Assert.IsTrue(b.AddShip(new Ship(1, 1, 0, Enumerations.ORIENTATION.Horizontal)));
            Assert.IsTrue(b.AddShip(new Ship(10, 2, 0, Enumerations.ORIENTATION.Horizontal)));
            Assert.IsFalse(b.AddShip(new Ship(11, 3, 0, Enumerations.ORIENTATION.Horizontal)));
            Assert.IsFalse(b.AddShip(new Ship(11, 4, 5, Enumerations.ORIENTATION.Vertical)));
            Assert.IsTrue(b.AddShip(new Ship(3, 4, 5, Enumerations.ORIENTATION.Vertical)));
            Assert.IsFalse(b.AddShip(new Ship(3, 4, 5, Enumerations.ORIENTATION.Vertical)));
            Assert.IsFalse(b.AddShip(new Ship(3, 10, 8, Enumerations.ORIENTATION.Vertical)));
            Assert.IsFalse(b.AddShip(new Ship(3, 10, 10, Enumerations.ORIENTATION.Vertical)));
            Assert.IsFalse(b.AddShip(new Ship(3, -3, -2, Enumerations.ORIENTATION.Vertical)));
            Assert.IsTrue(b.AddShip(new Ship(4, 5, 4, Enumerations.ORIENTATION.Vertical)));
            Assert.IsFalse(b.AddShip(new Ship(5, 5, 4, Enumerations.ORIENTATION.Vertical)));
        }

        [TestMethod]
        public void TestCheckAlivePoints()
        {
            Board b = new Board(10);
            b.AddShip(new Ship(10, 2, 0, Enumerations.ORIENTATION.Horizontal));

            Assert.AreEqual(b.CheckAlivePoints(), 10);

        }

        [TestMethod]
        public void TestIsValidCoordinate()
        {
            Board b = new Board(10);

            Assert.IsTrue(b.IsValidCoordinate(new KeyValuePair<int, int>(0, 0)));
            Assert.IsTrue(b.IsValidCoordinate(new KeyValuePair<int, int>(9, 9)));
            Assert.IsTrue(b.IsValidCoordinate(new KeyValuePair<int, int>(3, 7)));
            Assert.IsTrue(!b.IsValidCoordinate(new KeyValuePair<int, int>(-4, -4)));
            Assert.IsTrue(!b.IsValidCoordinate(new KeyValuePair<int, int>(10, 10)));

            b.BoardMap[5, 5] = 2;
            Assert.IsTrue(!b.IsValidCoordinate(new KeyValuePair<int, int>(5, 5)));
        }


        [TestMethod]
        public void TestStrike()
        {
            Board b = new Board(10);
            b.AddShip(new Ship(3, 0, 0, Enumerations.ORIENTATION.Horizontal));
            b.AddShip(new Ship(1, 1, 0, Enumerations.ORIENTATION.Horizontal));
            b.AddShip(new Ship(1, 2, 0, Enumerations.ORIENTATION.Horizontal));

            Assert.AreEqual(Enumerations.OUTCOME.Hit, b.Strike(new KeyValuePair<int, int>(0, 0)));
            Assert.AreEqual(Enumerations.OUTCOME.Hit, b.Strike(new KeyValuePair<int, int>(0, 1)));
            Assert.AreEqual(Enumerations.OUTCOME.Hit, b.Strike(new KeyValuePair<int, int>(0, 2)));
            Assert.AreEqual(Enumerations.OUTCOME.Hit, b.Strike(new KeyValuePair<int, int>(1, 0)));
            Assert.AreEqual(Enumerations.OUTCOME.Miss, b.Strike(new KeyValuePair<int, int>(0, 3)));
            Assert.AreEqual(Enumerations.OUTCOME.Invalid, b.Strike(new KeyValuePair<int, int>(0, 0)));
            Assert.AreEqual(Enumerations.OUTCOME.Invalid, b.Strike(new KeyValuePair<int, int>(0, 1)));
            Assert.AreEqual(Enumerations.OUTCOME.Invalid, b.Strike(new KeyValuePair<int, int>(10, 10)));
            Assert.AreEqual(Enumerations.OUTCOME.Won, b.Strike(new KeyValuePair<int, int>(2, 0)));
        }
    }
}
