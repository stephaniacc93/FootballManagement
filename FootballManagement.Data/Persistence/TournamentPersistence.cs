using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class TournamentPersistence : IPersistence<Tournament>
    {
        public Tournament Create(Tournament tournament)
        {
            Tournament response = new Tournament();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.Tournaments.AddObject(tournament);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.Tournaments.Single(x => x.Id == tournament.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Tournament Read(int ID)
        {
            Tournament response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Tournaments.Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Tournament Update(Tournament tournament)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var d = new Tournament { Id = tournament.Id };
                    footballmanagementEntities.Tournaments.Attach(d);
                    footballmanagementEntities.Tournaments.ApplyCurrentValues(tournament);
                    footballmanagementEntities.SaveChanges();
                    return tournament;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Tournament tournament)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var delete = footballmanagementEntities.Tournaments.Include("Teams").Include("Referees").Include("Matches").Single(x => x.Id == tournament.Id);
                    footballmanagementEntities.Tournaments.Attach(delete);
                    footballmanagementEntities.Tournaments.DeleteObject(delete);
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

        public List<Tournament> GetList()
        {
            List<Tournament> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Tournaments.ToList();

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
