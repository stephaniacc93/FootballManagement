using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class GoalBusiness
    {
        public bool CreateGoal(Goal goal)
        {
            bool response = false;
            try
            {
                GoalPersistence goalPersistence = new GoalPersistence();
                goalPersistence.Create(goal);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Goal ReadGoal(int ID)
        {
            Goal response;
            try
            {
                GoalPersistence goalPersistence = new GoalPersistence();
                response = goalPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool UpdateGoal(Goal goal)
        {
            bool response = false;
            try
            {
                GoalPersistence goalPersistence = new GoalPersistence();
                goalPersistence.Update(goal);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteGoal(Goal goal)
        {
            bool response = false;
            try
            {
                GoalPersistence goalPersistence = new GoalPersistence();
                response = goalPersistence.Delete(goal);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Goal> GetListGoal()
        {
            try
            {
                GoalPersistence goalPersistence = new GoalPersistence();
                List<Goal> l = new List<Goal>();
                l = goalPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
