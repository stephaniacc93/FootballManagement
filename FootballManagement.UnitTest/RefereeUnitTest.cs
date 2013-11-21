using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Linq;
using System.Collections.Generic;

namespace FootballManagement.UnitTest
{
    [TestClass]
    public class RefereeUnitTest
    {
        [TestMethod]
        public void TestCreate_Referee()
        {
            //arrange
            Referee referee = new Referee();
            referee.Name = "Juan Perez";
            referee.Gender = "Masculino";
            referee.Birthday = new DateTime(1970, 06, 20);
            referee.Degree = "Esquina";

            RefereePersistence refereePersistence = new RefereePersistence();

            //act
            referee = refereePersistence.Create(referee);

            //assert
            Assert.AreNotEqual(referee.Id, 0);
        }

        [TestMethod]
        public void TestRead_Referee()
        {
            //arrange
            Referee referee = new Referee();
            Referee referee1 = new Referee();
            referee.Id = 5;

            RefereePersistence refereePersistence = new RefereePersistence();

            //act
            referee1 = refereePersistence.Read(referee.Id);

            //assert
            Assert.AreEqual(referee.Id, referee1.Id);

        }

        [TestMethod]
        public void TestUpdate_Referee()
        {
            //arrange
            Referee referee = new Referee();
            Referee referee1 = new Referee();
            referee.Id = 5;

            RefereePersistence refereePersistence = new RefereePersistence();

            //act
            referee1 = refereePersistence.Update(referee);

            //assert
            Assert.AreEqual(referee, referee1);
        }

        [TestMethod]
        public void TestDelete_Referee()
        {
            //arrange
            bool result;
            Referee referee = new Referee();
            referee.Id = 5;

            RefereePersistence refereePersistence = new RefereePersistence();

            //act
            result = refereePersistence.Delete(referee);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestList_Referee()
        {
            //arrange

            List<Referee> l = new List<Referee>();
            RefereePersistence refereePersistence = new RefereePersistence();

            //act
            l = refereePersistence.GetList();

            //assert
            Assert.IsTrue(l.Count > 0);

        }
    }
}
