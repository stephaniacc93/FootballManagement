﻿using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class TeamPersistence : IPersistence<Team>
    {
        public Team Create(Team team)
        {
            Team response = new Team();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    team.Players = footballmanagementEntities.People.OfType<Player>().AsEnumerable().Where(x => team.Players.Any(y => x.Id == y.Id)).ToList();
                    team.Tournament = footballmanagementEntities.Tournaments.First(x => x.Id == team.Tournament.Id);
                    footballmanagementEntities.Teams.AddObject(team);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.Teams.Single(x => x.Id == team.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Team Read(int ID)
        {
            Team response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Teams.Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Team Update(Team team)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    Team d = footballmanagementEntities.Teams.Include("Tournament").FirstOrDefault(x => x.Id == team.Id);
                    d.Tournament = footballmanagementEntities.Tournaments.First(x => x.Id == team.Tournament.Id);
                    footballmanagementEntities.Teams.Attach(d);
                    footballmanagementEntities.Teams.ApplyCurrentValues(team);
                    footballmanagementEntities.SaveChanges();
                    return d;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Team team)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var delete = footballmanagementEntities.Teams.Include("Players").Include("Matches").Include("Tournament").Single(x => x.Id == team.Id);
                    footballmanagementEntities.Teams.Attach(delete);
                    footballmanagementEntities.Teams.DeleteObject(delete);
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

        public List<Team> GetList()
        {
            List<Team> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Teams.Include("Tournament").Include("Players").ToList();

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
