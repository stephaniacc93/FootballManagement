using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class TeamBusiness
    {
        public bool CreateTeam(Team team)
        {
            bool response = false;
            try
            {
                TeamPersistence teamPersistence = new TeamPersistence();
                teamPersistence.Create(team);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Team ReadTeam(int ID)
        {
            Team response;
            try
            {
                TeamPersistence teamPersistence = new TeamPersistence();
                response = teamPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Team UpdateTeam(Team team)
        {
            Team response = new Team();
            try
            {
                TeamPersistence teamPersistence = new TeamPersistence();
                response = teamPersistence.Update(team);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteTeam(Team team)
        {
            bool response = false;
            try
            {
                TeamPersistence teamPersistence = new TeamPersistence();
                response = teamPersistence.Delete(team);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Team> GetListTeam()
        {
            try
            {
                TeamPersistence teamPersistence = new TeamPersistence();
                List<Team> l = new List<Team>();
                l = teamPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
