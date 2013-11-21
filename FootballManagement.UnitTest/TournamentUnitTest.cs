using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class TournamentUnitTest
    {
        [TestMethod]
        public void TestCreate_Tournament()
        {
            //arrange
            Tournament tournament = new Tournament();
            tournament.Name = "Copa Libertadores";

            RefereePersistence rp = new RefereePersistence();
            tournament.Referees = rp.GetList();

            TeamPersistence tp = new TeamPersistence();
            tournament.Teams = tp.GetList();

            TournamentPersistence tournamentPersistence = new TournamentPersistence();

            //act
            tournament = tournamentPersistence.Create(tournament);

            //assert
            Assert.AreNotEqual(tournament.Id, 0);
        }

        [TestMethod]
        public void TestRead_Tournament()
        {
            //arrange
            Tournament tournament = new Tournament();
            Tournament tournament1 = new Tournament();
            tournament.Id = 1;

            TournamentPersistence tournamentPersistence = new TournamentPersistence();

            //act
            tournament1 = tournamentPersistence.Read(tournament.Id);

            //assert
            Assert.AreEqual(tournament.Id, tournament1.Id);

        }

        [TestMethod]
        public void TestUpdate_Tournament()
        {
            //arrange
            Tournament tournament = new Tournament();
            Tournament tournament1 = new Tournament();
            tournament.Id = 1;

            TournamentPersistence tournamentPersistence = new TournamentPersistence();

            //act
            tournament1 = tournamentPersistence.Update(tournament);

            //assert
            Assert.AreEqual(tournament, tournament1);
        }

        [TestMethod]
        public void TestDelete_Team()
        {
            //arrange
            bool result;
            Tournament tournament = new Tournament();
            tournament.Id = 1;

            TournamentPersistence tournamentPersistence = new TournamentPersistence();

            //act
            result = tournamentPersistence.Delete(tournament);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Tournament()
        {
            //arrange

            List<Tournament> l = new List<Tournament>();
            TournamentPersistence tournamentPersistence = new TournamentPersistence();

            //act
            l = tournamentPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
