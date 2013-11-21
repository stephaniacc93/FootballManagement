using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FootballManagement.Commons.Entities;
using FootballManagement.Business.Business;

namespace FootballManagement.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FootballManagementService : IFootballManagementService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region ServiceCard
        public bool CreateCard(Card card)
        {
            bool response = false;
            try
            {
                CardBusiness cardBusiness = new CardBusiness();
                response = cardBusiness.CreateCard(card);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool UpdateCard(Card card)
        {
            bool response = false;
            try
            {
                CardBusiness cardBusiness = new CardBusiness();
                response = cardBusiness.UpdateCard(card);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public Card ReadCard(int ID)
        {
            Card response = new Card();
            try
            {
                CardBusiness cardBusiness = new CardBusiness();
                response = cardBusiness.ReadCard(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public bool DeleteCard(Card card)
        {
            bool response = false;
            try
            {
                CardBusiness cardBusiness = new CardBusiness();
                response = cardBusiness.DeleteCard(card);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        public List<Card> GetListCard()
        {
            List<Card> response = new List<Card>();
            try
            {
                CardBusiness cardBusiness = new CardBusiness();
                response = cardBusiness.GetListCard();
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        #endregion

        #region ServiceGoal

        public bool CreateGoal(Goal goal)
        {
            bool response = false;
            try
            {
                GoalBusiness goalBusiness = new GoalBusiness();
                response = goalBusiness.CreateGoal(goal);
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
                GoalBusiness goalBusiness = new GoalBusiness();
                response = goalBusiness.UpdateGoal(goal);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Goal ReadGoal(int ID)
        {
            Goal response = new Goal();
            try
            {
                GoalBusiness goalBusiness = new GoalBusiness();
                response = goalBusiness.ReadGoal(ID);
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
                GoalBusiness goalBusiness = new GoalBusiness();
                response = goalBusiness.DeleteGoal(goal);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Goal> GetListGoal()
        {
            List<Goal> response = new List<Goal>();
            try
            {
                GoalBusiness goalBusiness = new GoalBusiness();
                response = goalBusiness.GetListGoal();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion

        #region ServiceMatch

        public bool CreateMatch(Match match)
        {
            bool response = false;
            try
            {
                MatchBusiness matchBusiness = new MatchBusiness();
                response = matchBusiness.CreateMatch(match);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool UpdateMatch(Match match)
        {
            bool response = false;
            try
            {
                MatchBusiness matchBusiness = new MatchBusiness();
                response = matchBusiness.UpdateMatch(match);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Match ReadMatch(int ID)
        {
            Match response = new Match();
            try
            {
                MatchBusiness matchBusiness = new MatchBusiness();
                response = matchBusiness.ReadMatch(ID);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool DeleteMatch(Match match)
        {
            bool response = false;
            try
            {
                MatchBusiness matchBusiness = new MatchBusiness();
                response = matchBusiness.DeleteMatch(match);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Match> GetListMatch()
        {
            List<Match> response = new List<Match>();
            try
            {
                MatchBusiness matchBusiness = new MatchBusiness();
                response = matchBusiness.GetListMatch();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion

        #region ServicePlayer

        public bool CreatePlayer(Player player)
        {
            bool response = false;
            try
            {
                PlayerBusiness playerBusiness = new PlayerBusiness();
                response = playerBusiness.CreatePlayer(player);
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
                PlayerBusiness playerBusiness = new PlayerBusiness();
                response = playerBusiness.UpdatePlayer(player);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Player ReadPlayer(int ID)
        {
            Player response = new Player();
            try
            {
                PlayerBusiness playerBusiness = new PlayerBusiness();
                response = playerBusiness.ReadPlayer(ID);
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
                PlayerBusiness playerBusiness = new PlayerBusiness();
                response = playerBusiness.DeletePlayer(player);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Player> GetListPlayer()
        {
            List<Player> response = new List<Player>();
            try
            {
                PlayerBusiness playerBusiness = new PlayerBusiness();
                response = playerBusiness.GetListPlayer();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion

        #region ServiceReferee

        public bool CreateReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereeBusiness refereeBusiness = new RefereeBusiness();
                response = refereeBusiness.CreateReferee(referee);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool UpdateReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereeBusiness refereeBusiness = new RefereeBusiness();
                response = refereeBusiness.UpdateReferee(referee);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Referee ReadReferee(int ID)
        {
            Referee response = new Referee();
            try
            {
                RefereeBusiness refereeBusiness = new RefereeBusiness();
                response = refereeBusiness.ReadPlayer(ID);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool DeleteReferee(Referee referee)
        {
            bool response = false;
            try
            {
                RefereeBusiness refereeBusiness = new RefereeBusiness();
                response = refereeBusiness.DeleteReferee(referee);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Referee> GetListReferee()
        {
            List<Referee> response = new List<Referee>();
            try
            {
                RefereeBusiness refereeBusiness = new RefereeBusiness();
                response = refereeBusiness.GetListReferee();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion

        #region ServiceTeam

        public bool CreateTeam(Team team)
        {
            bool response = false;
            try
            {
                TeamBusiness teamBusiness = new TeamBusiness();
                response = teamBusiness.CreateTeam(team);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool UpdateTeam(Team team)
        {
            bool response = false;
            try
            {
                TeamBusiness teamBusiness = new TeamBusiness();
                response = teamBusiness.UpdateTeam(team);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Team ReadTeam(int ID)
        {
            Team response = new Team();
            try
            {
                TeamBusiness teamBusiness = new TeamBusiness();
                response = teamBusiness.ReadTeam(ID);
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
                TeamBusiness teamBusiness = new TeamBusiness();
                response = teamBusiness.DeleteTeam(team);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Team> GetListTeam()
        {
            List<Team> response = new List<Team>();
            try
            {
                TeamBusiness teamBusiness = new TeamBusiness();
                response = teamBusiness.GetListTeam();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion

        #region ServiceTournament

        public bool CreateTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentBusiness tournamentBusiness = new TournamentBusiness();
                response = tournamentBusiness.CreateTournament(tournament);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentBusiness tournamentBusiness = new TournamentBusiness();
                response = tournamentBusiness.UpdateTournament(tournament);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public Tournament ReadTournament(int ID)
        {
            Tournament response = new Tournament();
            try
            {
                TournamentBusiness tournamentBusiness = new TournamentBusiness();
                response = tournamentBusiness.ReadTournament(ID);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public bool DeleteTournament(Tournament tournament)
        {
            bool response = false;
            try
            {
                TournamentBusiness tournamentBusiness = new TournamentBusiness();
                response = tournamentBusiness.DeleteTournament(tournament);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        public List<Tournament> GetListTournament()
        {
            List<Tournament> response = new List<Tournament>();
            try
            {
                TournamentBusiness tournamentBusiness = new TournamentBusiness();
                response = tournamentBusiness.GetListTournament();
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

        #endregion
    }
}
