using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class MatchPersistence :IPersistence<Match>
    {
        public Match Create(Match match)
        {
            Match response = new Match();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    match.Players = footballmanagementEntities.People.OfType<Player>().ToList();
                    match.Referees = footballmanagementEntities.People.OfType<Referee>().ToList();
                    match.Tournament = footballmanagementEntities.Tournaments.FirstOrDefault();
                    footballmanagementEntities.Matches.AddObject(match);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.Matches.Single(x => x.Id == match.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Match Read(int ID)
        {
            Match response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Matches.Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Match Update(Match match)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var d = new Match { Id = match.Id };
                    footballmanagementEntities.Matches.Attach(d);
                    footballmanagementEntities.Matches.ApplyCurrentValues(match);
                    footballmanagementEntities.SaveChanges();
                    return match;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Match match)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var delete = footballmanagementEntities.Matches.Include("Players").Include("Referees").Include("Tournament").Single(x => x.Id == match.Id);
                    footballmanagementEntities.Matches.Attach(delete);
                    footballmanagementEntities.Matches.DeleteObject(delete);
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

        public List<Match> GetList()
        {
            List<Match> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Matches.ToList();

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
