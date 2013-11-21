using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class TournamentBusiness
    {
        public bool CreateTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentPersistence tournamentPersistence = new TournamentPersistence();
                tournamentPersistence.Create(tournament);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Tournament ReadTournament(int ID)
        {
            Tournament response;
            try
            {
                TournamentPersistence tournamentPersistence = new TournamentPersistence();
                response = tournamentPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentPersistence tournamentPersistence = new TournamentPersistence();
                tournamentPersistence.Update(tournament);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentPersistence tournamentPersistence = new TournamentPersistence();
                response = tournamentPersistence.Delete(tournament);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Tournament> GetListTournament()
        {
            try
            {
                TournamentPersistence tournamentPersistence = new TournamentPersistence();
                List<Tournament> l = new List<Tournament>();
                l = tournamentPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
