using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class GoalPersistence : IPersistence<Goal>
    {
        public Goal Create(Goal goal)
        {
            Goal response = new Goal();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.Goals.AddObject(goal);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.Goals.Single(x => x.Id == goal.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Goal Read(int ID)
        {
            Goal response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Goals.Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Goal Update(Goal goal)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var d = new Goal { Id = goal.Id };
                    footballmanagementEntities.Goals.Attach(d);
                    footballmanagementEntities.Goals.ApplyCurrentValues(goal);
                    footballmanagementEntities.SaveChanges();
                    return goal;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Goal goal)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    var delete = footballmanagementEntities.Goals.Include("Player").Include("Match").Single(x => x.Id == goal.Id);
                    footballmanagementEntities.Goals.Attach(delete);
                    footballmanagementEntities.Goals.DeleteObject(delete);
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

        public List<Goal> GetList()
        {
            List<Goal> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.Goals.ToList();

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
