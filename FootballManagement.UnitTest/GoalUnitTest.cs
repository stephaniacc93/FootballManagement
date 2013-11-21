using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class GoalUnitTest
    {
        [TestMethod]
        public void TestCreate_Goal()
        {
            //arrange
            Goal goal = new Goal();
            Player player = new Player();

            MatchPersistence mp = new MatchPersistence();
            Match match = mp.GetList().First();

            PlayerPersistence pp = new PlayerPersistence();
            player = pp.GetList().First();

            goal.Match = match;
            goal.Player = player;
            goal.Time = new DateTime(2013, 11, 04, 12, 17, 30);

            GoalPersistence goalPersistence = new GoalPersistence();

            //act
            goal = goalPersistence.Create(goal);

            //assert
            Assert.AreNotEqual(goal.Id, 0);
        }

        [TestMethod]
        public void TestRead_Goal()
        {
            //arrange
            Goal goal = new Goal();
            Goal goal1 = new Goal();
            goal.Id = 1;

            GoalPersistence goalPersistence = new GoalPersistence();

            //act
            goal1 = goalPersistence.Read(goal.Id);

            //assert
            Assert.AreEqual(goal.Id, goal1.Id);

        }

        [TestMethod]
        public void TestUpdate_Goal()
        {
            //arrange
            Goal goal = new Goal();
            Goal goal1 = new Goal();
            goal.Id = 1;

            GoalPersistence goalPersistence = new GoalPersistence();

            //act
            goal1 = goalPersistence.Update(goal);

            //assert
            Assert.AreEqual(goal, goal1);
        }

        [TestMethod]
        public void TestDelete_Goal()
        {
            //arrange
            bool result;
            Goal goal = new Goal();
            goal.Id = 1;

            GoalPersistence goalPersistence = new GoalPersistence();

            //act
            result = goalPersistence.Delete(goal);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Goal()
        {
            //arrange

            List<Goal> l = new List<Goal>();
            GoalPersistence goalPersistence = new GoalPersistence();

            //act
            l = goalPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
