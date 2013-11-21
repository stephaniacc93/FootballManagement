using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManagement.Commons.Entities;

namespace FootballManagement.Data.Persistence
{
    public class CardPersistence : IPersistence<Card>
    {

        public Card Create(Card card)
        {
            Card response = new Card();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.Cards.AddObject(card);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.Cards.Single(x => x.Id == card.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Card Read(int ID)
        {
            Card response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Cards.Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Card Update(Card card)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var d = new Card { Id = card.Id };
                    footballmanagementEntities.Cards.Attach(d);
                    footballmanagementEntities.Cards.ApplyCurrentValues(card);
                    footballmanagementEntities.SaveChanges();
                    return card;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Card card)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var delete = footballmanagementEntities.Cards.Include("Match").Include("Player").Single(x => x.Id == card.Id);
                    footballmanagementEntities.Cards.Attach(delete);
                    footballmanagementEntities.Cards.DeleteObject(delete);
                    footballmanagementEntities.SaveChanges();
                    response = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Card> GetList()
        {
            List<Card> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Cards.ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }
    }
}
