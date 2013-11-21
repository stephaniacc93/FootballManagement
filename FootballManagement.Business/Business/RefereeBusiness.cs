using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class RefereeBusiness
    {
        public bool CreateReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereePersistence refereePersistence = new RefereePersistence();
                refereePersistence.Create(referee);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Referee ReadPlayer(int ID)
        {
            Referee response;
            try
            {
                RefereePersistence refereePersistence = new RefereePersistence();
                response = refereePersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool UpdateReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereePersistence refereePersistence = new RefereePersistence();
                refereePersistence.Update(referee);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereePersistence refereePersistence = new RefereePersistence();
                response = refereePersistence.Delete(referee);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Referee> GetListReferee()
        {
            try
            {
                RefereePersistence refereePersistence = new RefereePersistence();
                List<Referee> l = new List<Referee>();
                l = refereePersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
