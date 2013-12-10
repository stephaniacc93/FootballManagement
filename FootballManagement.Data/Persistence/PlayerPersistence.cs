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
                    response = footballmanagementEntities.People.OfType<Player>().Single(x => x.Id == ID);
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
                    Player d = new Player { Id = player.Id };
                    footballmanagementEntities.People.Attach(d);
                    footballmanagementEntities.People.ApplyCurrentValues(player);
                    footballmanagementEntities.SaveChanges();
                    return player;
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
                    response = footballmanagementEntities.People.Include("Team").OfType<Player>().ToList();

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
