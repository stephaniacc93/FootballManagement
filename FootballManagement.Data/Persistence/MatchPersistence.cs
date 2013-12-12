using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class MatchPersistence : IPersistence<Match>
    {
        public Match Create(Match match)
        {
            Match response = new Match();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    match.Tournament = footballmanagementEntities.Tournaments.First(x => x.Id == match.Tournament.Id);
                    match.Team = footballmanagementEntities.Teams.First(x => x.Id == match.Team.Id);
                    match.Team1 = footballmanagementEntities.Teams.First(x => x.Id == match.Team1.Id);
                    match.Referees = footballmanagementEntities.People.OfType<Referee>().AsEnumerable().Where(x => match.Referees.Any(y => x.Id == y.Id)).ToList();
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
                    response = footballmanagementEntities.Matches.Include("Players").Include("Team.Players").Include("Team1.Players").Include("Referees").Include("Team.Matches").Include("Team1.Matches").Include("Team").Include("Team1").Include("Tournament").Single(x => x.Id == ID);
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
                    var d = footballmanagementEntities.Matches.Include("Players").Include("Referees").Include("Team").Include("Team1").Include("Tournament").FirstOrDefault(x => x.Id == match.Id);
                    d.Tournament = footballmanagementEntities.Tournaments.First(x => x.Id == match.Tournament.Id);
                    d.Team = footballmanagementEntities.Teams.First(x => x.Id == match.Team.Id);
                    d.Team1 = footballmanagementEntities.Teams.First(x => x.Id == match.Team1.Id);
                    d.MatchDate = match.MatchDate;
                    d.Referees = footballmanagementEntities.People.OfType<Referee>().AsEnumerable().Where(x => match.Referees.Any(y => x.Id == y.Id)).ToList();
                    footballmanagementEntities.SaveChanges();
                    return d;
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
                    var delete = footballmanagementEntities.Matches.Include("Team.Players").Include("Team1.Players").Include("Referees").Include("Team.Matches").Include("Team1.Matches").Include("Team").Include("Team1").Include("Tournament").Single(x => x.Id == match.Id);
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
                    response = footballmanagementEntities.Matches.Include("Team.Players").Include("Team1.Players").Include("Referees").Include("Team.Matches").Include("Team1.Matches").Include("Team").Include("Team1").Include("Tournament").ToList();
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
