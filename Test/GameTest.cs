using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoLife.Test
{
    [TestClass]
    public class GameTest
    {
        private GameOfLifeBase<CellWorld> game;

        [TestInitialize]
        public void Prepare()
        {
            game = new GameOfLife();
            var w = new CellWorld
            {
                [-10, -10] = true,

                [0, 0] = true,
                [0, 1] = true,
                [1, 0] = true,
                [1, 1] = true,

                [10, 1] = true,
                [10, 2] = true,
                [10, 3] = true,
                [11, 1] = true,
                [11, 2] = true,
                [11, 3] = true,
                [12, 1] = true,
                [12, 2] = true,
                [12, 3] = true,

                [20, 1] = true,
                [20, 2] = true,
                [20, 3] = true,
                [21, 2] = true,
                [22, 2] = true,

                [30, 1] = true,
                [30, 2] = true,
                [30, 3] = true,
                [31, 2] = true,
                [32, 1] = true,
                [32, 2] = true,
                [32, 3] = true,

                [40, 1] = true,
                [40, 2] = true,
                [40, 3] = true,
                [41, 1] = true,
                [41, 2] = true,
                [42, 1] = true,
                [42, 2] = true,
                [42, 3] = true
            };

            game.Initialize(new Generation<CellWorld> { Live = w });
        }

        [TestMethod]
        public void GetNumberOfNeighboursTest()
        {
            Assert.AreEqual(0, game.GetNumberOfAliveNeighbours(-10, -10));
            Assert.AreEqual(1, game.GetNumberOfAliveNeighbours(22, 2));
            Assert.AreEqual(2, game.GetNumberOfAliveNeighbours(19, 1));
            Assert.AreEqual(3, game.GetNumberOfAliveNeighbours(0, 1));
            Assert.AreEqual(3, game.GetNumberOfAliveNeighbours(19, 2));
            Assert.AreEqual(4, game.GetNumberOfAliveNeighbours(21, 2));
            Assert.AreEqual(5, game.GetNumberOfAliveNeighbours(10, 2));
            Assert.AreEqual(6, game.GetNumberOfAliveNeighbours(31, 2));
            Assert.AreEqual(7, game.GetNumberOfAliveNeighbours(41, 2));
            Assert.AreEqual(8, game.GetNumberOfAliveNeighbours(11, 2));
        }

        [TestMethod]
        public void Rule_Underpopulated()
        {
            var aliveat_10_10 = game.GetCellAt(-10, -10); //0
            var aliveat_22_2 = game.GetCellAt(22, 2); // //1

            Assert.IsTrue(aliveat_10_10); //0
            Assert.IsTrue(aliveat_22_2); // //1

            game.StartFromPrevious = true;
            game.Stop = true;
            game.Run();

            Assert.IsTrue(aliveat_10_10 && !game.GetCellAt(-10, -10)); //0
            Assert.IsTrue(aliveat_22_2 && !game.GetCellAt(22, 2)); // //1
        }


        [TestMethod]
        public void Rule_Lives()
        {
            game.Stop = true;
            game.Run();

            Assert.IsTrue(game.GetCellAt(20, 2)); // 2
            Assert.IsTrue(game.GetCellAt(0, 1)); // 3 lives
        }


        [TestMethod]
        public void Rule_Overpopulated()
        {
            var aliveat_21_2 = game.GetCellAt(21, 2); // 4
            var aliveat_10_2 = game.GetCellAt(10, 2); // 5
            var aliveat_31_2 = game.GetCellAt(31, 2); // 6
            var aliveat_41_2 = game.GetCellAt(41, 2); // 7
            var aliveat_11_2 = game.GetCellAt(11, 2); // 8

            Assert.IsTrue(aliveat_21_2); // 4
            Assert.IsTrue(aliveat_10_2); // 5
            Assert.IsTrue(aliveat_31_2); // 6
            Assert.IsTrue(aliveat_41_2); // 7
            Assert.IsTrue(aliveat_11_2); // 8


            game.StartFromPrevious = true;
            game.Stop = true;
            game.Run();

            Assert.IsTrue(aliveat_21_2 && !game.GetCellAt(21, 2)); // 4
            Assert.IsTrue(aliveat_10_2 && !game.GetCellAt(10, 2)); // 5
            Assert.IsTrue(aliveat_31_2 && !game.GetCellAt(31, 2)); // 6
            Assert.IsTrue(aliveat_41_2 && !game.GetCellAt(41, 2)); // 7
            Assert.IsTrue(aliveat_11_2 && !game.GetCellAt(11, 2)); // 8
        }


        [TestMethod]
        public void Rule_NewBorn()
        {
            var aliveat_19_2 = game.GetCellAt(19, 2);
            game.Stop = true;
            game.Run();

            Assert.IsTrue(!aliveat_19_2 && game.GetCellAt(19, 2)); // 3 born
        }

        [TestMethod]
        public void ComputeNextGenerationTest()
        {
            game.Stop = true;
            game.Run();

            Assert.IsFalse(game.GetCellAt(-10, -10)); //0
            Assert.IsFalse(game.GetCellAt(22, 2)); // //1
            Assert.IsTrue(game.GetCellAt(20, 2)); // 2
            Assert.IsTrue(game.GetCellAt(0, 1)); // 3 lives
            Assert.IsTrue(game.GetCellAt(19, 2)); // 3 born
            Assert.IsFalse(game.GetCellAt(21, 2)); // 4
            Assert.IsFalse(game.GetCellAt(10, 2)); // 5
            Assert.IsFalse(game.GetCellAt(31, 2)); // 6
            Assert.IsFalse(game.GetCellAt(41, 2)); // 7
            Assert.IsFalse(game.GetCellAt(11, 2)); // 8
        }
    }
}
