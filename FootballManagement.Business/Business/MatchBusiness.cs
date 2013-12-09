using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class MatchBusiness
    {
        public bool CreateMatch(Match match)
        {
            bool response = false;
            try
            {
                MatchPersistence matchPersistence = new MatchPersistence();
                matchPersistence.Create(match);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Match ReadMatch(int ID)
        {
            Match response;
            try
            {
                MatchPersistence matchPersistence = new MatchPersistence();
                response = matchPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Match UpdateMatch(Match match)
        {
            Match response = new Match();
            try
            {
                MatchPersistence matchPersistence = new MatchPersistence();
                response = matchPersistence.Update(match);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteMatch(Match match)
        {
            bool response = false;
            try
            {
                MatchPersistence matchPersistence = new MatchPersistence();
                response = matchPersistence.Delete(match);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Match> GetListMatch()
        {
            try
            {
                MatchPersistence matchPersistence = new MatchPersistence();
                List<Match> l = new List<Match>();
                l = matchPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
