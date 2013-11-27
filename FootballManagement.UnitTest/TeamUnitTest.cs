using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;
using FootballManagement.Commons.Entities;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class TeamUnitTest
    {
        [TestMethod]
        public void TestCreate_Team()
        {
            //arrange
            Team team = new Team();
            team.Name = "Pumas";
            PlayerPersistence pp = new PlayerPersistence();
            team.Players = pp.GetList();

            TeamPersistence teamPersistence = new TeamPersistence();

            //act
            team = teamPersistence.Create(team);

            //assert
            Assert.AreNotEqual(team.Id, 0);
        }

        [TestMethod]
        public void TestRead_Team()
        {
            //arrange
            Team team = new Team();
            Team team1 = new Team();
            team.Id = 2;

            TeamPersistence teamPersistence = new TeamPersistence();

            //act
            team1 = teamPersistence.Read(team.Id);

            //assert
            Assert.AreEqual(team.Id, team1.Id);

        }

        [TestMethod]
        public void TestUpdate_Team()
        {
            //arrange
            Team team = new Team();
            Team team1 = new Team();
            team.Id = 2;

            TeamPersistence teamPersistence = new TeamPersistence();

            //act
            team1 = teamPersistence.Update(team);

            //assert
            Assert.AreEqual(team, team1);
        }

        [TestMethod]
        public void TestDelete_Team()
        {
            //arrange
            bool result;
            Team team = new Team();
            team.Id = 2;

            TeamPersistence teamPersistence = new TeamPersistence();

            //act
            result = teamPersistence.Delete(team);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Team()
        {
            //arrange

            List<Team> l = new List<Team>();
            TeamPersistence teamPersistence = new TeamPersistence(); 

            //act
            l = teamPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
