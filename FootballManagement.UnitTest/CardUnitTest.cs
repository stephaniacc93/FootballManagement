using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class CardUnitTest
    {
        [TestMethod]
        public void TestCreate_Card()
        {
            //arrange
            Card card = new Card();
            DateTime date = new DateTime(2013, 11, 4, 12, 05, 30);
            Match match = new Match();
            Player player = new Player();

            MatchPersistence mp = new MatchPersistence();
            match = mp.GetList().First();
            PlayerPersistence pp = new PlayerPersistence();
            player = pp.GetList().First();

            card.Date = date;
            card.isRedCard = true;
            card.Match = match;
            card.Player = player;

            CardPersistence cardPersistence = new CardPersistence();

            //act
            card = cardPersistence.Create(card);

            //assert
            Assert.AreNotEqual(card.Id, 0);
        }

        [TestMethod]
        public void TestRead_Card()
        {
            //arrange
            Card card = new Card();
            Card card1 = new Card();
            card.Id = 1;

            CardPersistence cardPersistence = new CardPersistence();

            //act
            card1 = cardPersistence.Read(card.Id);

            //assert
            Assert.AreEqual(card.Id, card1.Id);

        }

        [TestMethod]
        public void TestUpdate_Program()
        {
            //arrange
            Card card = new Card();
            Card card1 = new Card();
            card.Id = 1;

            CardPersistence cardPersistence = new CardPersistence();


            //act
            card1 = cardPersistence.Update(card);

            //assert
            Assert.AreEqual(card, card1);
        }

        [TestMethod]
        public void TestDelete_Card()
        {
            //arrange
            bool result;
            Card card = new Card();
            card.Id = 1;

            CardPersistence cardPersistence = new CardPersistence();

            //act
            result = cardPersistence.Delete(card);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Card()
        {
            //arrange

            List<Card> l = new List<Card>();
            CardPersistence cardPersistence = new CardPersistence();

            //act
            l = cardPersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
