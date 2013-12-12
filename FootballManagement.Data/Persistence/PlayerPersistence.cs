using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class PlayerPersistence: IPersistence<Player>
    {
        public Player Create(Player player)
        {
            Player response = new Player();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    player.Team = footballmanagementEntities.Teams.First(x => x.Id == player.Team.Id);
                    player.Team.Players.Add(player);
                    footballmanagementEntities.People.AddObject(player);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.People.OfType<Player>().Single(x => x.Id == player.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Player Read(int ID)
        {
            Player response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.People.OfType<Player>().Include("Team").Include("Matches").Include("Goals").Include("Cards").Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Player Update(Player player)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var d = footballmanagementEntities.People.OfType<Player>().Include("Cards").Include("Goals").Include("Team").Include("Matches").FirstOrDefault(x => x.Id == player.Id);
                    d.IsAuthorized = player.IsAuthorized;
                    d.Team = footballmanagementEntities.Teams.First(x => x.Id == player.Team.Id);
                    d.Goals = footballmanagementEntities.Goals.Where(x => player.Goals.Any(y => y.Id == x.Id)).ToList();
                    d.Matches = footballmanagementEntities.Matches.Where(x => player.Matches.Any(y => y.Id == x.Id)).ToList();
                    footballmanagementEntities.SaveChanges();
                    return d;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Player player)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.People.Attach(player);
                    footballmanagementEntities.People.DeleteObject(player);
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

        public List<Player> GetList()
        {
            List<Player> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.People.Include("Team").Include("Matches").Include("Goals").Include("Cards").OfType<Player>().ToList();

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
