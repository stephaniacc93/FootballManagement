using FootballManagement.Commons.Entities;
using FootballManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Persistence
{
    public class RefereePersistence : IPersistence<Referee>
    {
        public Referee Create(Referee referee)
        {
            Referee response = new Referee();
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.People.AddObject(referee);
                    footballmanagementEntities.SaveChanges();
                    response = footballmanagementEntities.People.OfType<Referee>().Single(x => x.Id == referee.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Referee Read(int ID)
        {
            Referee response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.People.OfType<Referee>().Single(x => x.Id == ID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Referee Update(Referee referee)
        {
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    Referee d = new Referee { Id = referee.Id };
                    footballmanagementEntities.People.Attach(d);
                    footballmanagementEntities.People.ApplyCurrentValues(referee);
                    footballmanagementEntities.SaveChanges();
                    return referee;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Referee referee)
        {
            bool response = false;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    footballmanagementEntities.People.Attach(referee);
                    footballmanagementEntities.People.DeleteObject(referee);
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

        public List<Referee> GetList()
        {
            List<Referee> response;
            try
            {
                using (var footballmanagementEntities = new FootballManagementEntities())
                {
                    response = footballmanagementEntities.People.OfType<Referee>().ToList();

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
