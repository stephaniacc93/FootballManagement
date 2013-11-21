using FootballManagement.Commons.Entities;
using FootballManagement.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Business.Business
{
    public class PlayerBusiness
    {
        public bool CreatePlayer(Player player)
        {
            bool response = false;
            try
            {
                PlayerPersistence playerPersistence = new PlayerPersistence();
                playerPersistence.Create(player);
                response = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Player ReadPlayer(int ID)
        {
            Player response;
            try
            {
                PlayerPersistence playerPersistence = new PlayerPersistence();
                response = playerPersistence.Read(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool UpdatePlayer(Player player)
        {
            bool response = false;
            try
            {
                PlayerPersistence playerPersistence = new PlayerPersistence();
                playerPersistence.Update(player);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeletePlayer(Player player)
        {
            bool response = false;
            try
            {
                PlayerPersistence playerPersistence = new PlayerPersistence();
                response = playerPersistence.Delete(player);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Player> GetListPlayer()
        {
            try
            {
                PlayerPersistence playerPersistence = new PlayerPersistence();
                List<Player> l = new List<Player>();
                l = playerPersistence.GetList();
                return l;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
