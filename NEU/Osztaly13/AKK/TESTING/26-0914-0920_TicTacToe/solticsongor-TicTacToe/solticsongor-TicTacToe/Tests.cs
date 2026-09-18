using logicLib;

namespace solticsongor_TicTacToe
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartingPointValueCheck()
        {
            //Players shoot = new Players();
            GameBoard board = new GameBoard();
            Assert.That(board.Move(0, 1), Is.True);
            Assert.That(board.Move(0, 1), Is.False);
        }

        [Test]
        public void IsSecondPlayerRoundComes()
        {
            
        }

        [Test]
        public void IsGameTie()
        {

        }

        [Test]
        public void IsGameWon()
        {

        }

        [Test]
        public void WhatIsTheNameOfTheTwoPlayers()
        {

        }
    }
}
