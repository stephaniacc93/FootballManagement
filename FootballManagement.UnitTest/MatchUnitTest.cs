using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class MatchUnitTest
    {
        [TestMethod]
        public void TestCreate_Match()
        {
            //arrange
            Match match = new Match();
            //local visitor

            TeamPersistence tp = new TeamPersistence();
            RefereePersistence rp = new RefereePersistence();
            
            DateTime date = new DateTime(2013, 11, 04);

            TournamentPersistence top = new TournamentPersistence();
            Tournament tournament = top.GetList().First(x=> x.Name == "Copa Stephy");

            match.Team = tp.GetList().First(x=> x.Id == 11);
            match.Team1 = tp.GetList().First(x => x.Id == 12);
            match.MatchDate = date;
            match.Tournament = tournament;
            match.Referees = rp.GetList();

            MatchPersistence matchPersistence = new MatchPersistence();

            //act
            match = matchPersistence.Create(match);

            //assert
            Assert.AreNotEqual(match.Id, 0);
        }

        [TestMethod]
        public void TestRead_Match()
        {
            //arrange
            Match match = new Match();
            Match match1 = new Match();
            match.Id = 1;

            MatchPersistence matchPersistence = new MatchPersistence();

            //act
            match1 = matchPersistence.Read(match.Id);

            //assert
            Assert.AreEqual(match.Id, match1.Id);

        }

        [TestMethod]
        public void TestUpdate_Match()
        {
            //arrange
            Match match = new Match();
            Match match1 = new Match();
            match.Id = 1;

            MatchPersistence matchPersistence = new MatchPersistence();

            //act
            match1 = matchPersistence.Update(match);

            //assert
            Assert.AreEqual(match, match1);
        }

        [TestMethod]
        public void TestDelete_Match()
        {
            //arrange
            bool result;
            Match match = new Match();
            match.Id = 8;

            MatchPersistence matchPersistence = new MatchPersistence();

            //act
            result = matchPersistence.Delete(match);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Match()
        {
            //arrange

            List<Match> l = new List<Match>();
            MatchPersistence matchPersistence = new MatchPersistence();
            
            //act
            l = matchPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
