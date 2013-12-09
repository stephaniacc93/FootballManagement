using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class CardBusiness
    {
        public bool CreateCard(Card card)
        {
            bool response = false;
            try
            {
                CardPersistence cardPersistence = new CardPersistence();
                cardPersistence.Create(card);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Card ReadCard(int ID)
        {
            Card response;
            try
            {
                CardPersistence cardPersistence = new CardPersistence();
                response = cardPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Card UpdateCard(Card card)
        {
            Card response = new Card();
            try
            {
                CardPersistence cardPersistence = new CardPersistence();
                response = cardPersistence.Update(card);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteCard(Card card)
        {
            bool response = false;
            try
            {
                CardPersistence cardPersistence = new CardPersistence();
                response = cardPersistence.Delete(card);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Card> GetListCard()
        {
            try
            {
                CardPersistence cardPersistence = new CardPersistence();
                List<Card> l = new List<Card>();
                l = cardPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
