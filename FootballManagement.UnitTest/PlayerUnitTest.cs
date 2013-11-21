using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class PlayerUnitTest
    {
        [TestMethod]
        public void TestCreate_Player()
        {
            PlayerPersistence playerPersistence = new PlayerPersistence();
            GoalPersistence gp = new GoalPersistence();
            TeamPersistence tp = new TeamPersistence();
            MatchPersistence mp = new MatchPersistence();

            //arrange
            Player player = new Player();
            player.Name = "Mario Diaz";
            player.Gender = "Masculino";
            player.Birthday = new DateTime(1990, 09, 04);
            player.IsAuthorized = true;
            player.IsCaptain = true;

            //act
            player = playerPersistence.Create(player);

            //assert
            Assert.AreNotEqual(player.Id, 0);
        }

        [TestMethod]
        public void TestRead_Player()
        {
            //arrange
            Player player = new Player();
            Player player1 = new Player();
            player.Id = 11;

            PlayerPersistence playerPersistence = new PlayerPersistence();

            //act
            player1 = playerPersistence.Read(player.Id);

            //assert
            Assert.AreEqual(player.Id, player1.Id);

        }

        [TestMethod]
        public void TestUpdate_Player()
        {
            //arrange
            Player player = new Player();
            Player player1 = new Player();
            player.Id = 11;

            PlayerPersistence playerPersistence = new PlayerPersistence();

            //act
            player1 = playerPersistence.Update(player);

            //assert
            Assert.AreEqual(player, player1);
        }

        [TestMethod]
        public void TestDelete_Player()
        {
            //arrange
            bool result;
            Player player = new Player();
            player.Id = 11;

            PlayerPersistence playerPersistence = new PlayerPersistence();

            //act
            result = playerPersistence.Delete(player);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Player()
        {
            //arrange

            List<Player> l = new List<Player>();
            PlayerPersistence playerPersistence = new PlayerPersistence();

            //act
            l = playerPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
