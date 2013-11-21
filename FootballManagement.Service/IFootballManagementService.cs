using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FootballManagement.Commons.Entities;

namespace FootballManagement.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFootballManagementService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        //CARD
        [OperationContract]
        bool CreateCard(Card card);

        [OperationContract]
        bool UpdateCard(Card card);

        [OperationContract]
        Card ReadCard(int ID);

        [OperationContract]
        bool DeleteCard(Card card);

        [OperationContract]
        List<Card> GetListCard();

        //GOAL
        [OperationContract]
        bool CreateGoal(Goal goal);

        [OperationContract]
        bool UpdateGoal(Goal goal);

        [OperationContract]
        Goal ReadGoal(int ID);

        [OperationContract]
        bool DeleteGoal(Goal goal);

        [OperationContract]
        List<Goal> GetListGoal();

        //MATCH
        [OperationContract]
        bool CreateMatch(Match match);

        [OperationContract]
        bool UpdateMatch(Match match);

        [OperationContract]
        Match ReadMatch(int ID);

        [OperationContract]
        bool DeleteMatch(Match match);

        [OperationContract]
        List<Match> GetListMatch();

        //PLAYER
        [OperationContract]
        bool CreatePlayer(Player player);

        [OperationContract]
        bool UpdatePlayer(Player player);

        [OperationContract]
        Player ReadPlayer(int ID);

        [OperationContract]
        bool DeletePlayer(Player player);

        [OperationContract]
        List<Player> GetListPlayer();

        //REFEREE
        [OperationContract]
        bool CreateReferee(Referee referee);

        [OperationContract]
        bool UpdateReferee(Referee referee);

        [OperationContract]
        Referee ReadReferee(int ID);

        [OperationContract]
        bool DeleteReferee(Referee referee);

        [OperationContract]
        List<Referee> GetListReferee();

        //TEAM
        [OperationContract]
        bool CreateTeam(Team team);

        [OperationContract]
        bool UpdateTeam(Team team);

        [OperationContract]
        Team ReadTeam(int ID);

        [OperationContract]
        bool DeleteTeam(Team team);

        [OperationContract]
        List<Team> GetListTeam();

        //TOURNAMENT
        [OperationContract]
        bool CreateTournament(Tournament tournament);

        [OperationContract]
        bool UpdateTournament(Tournament tournament);

        [OperationContract]
        Tournament ReadTournament(int ID);

        [OperationContract]
        bool DeleteTournament(Tournament tournament);

        [OperationContract]
        List<Tournament> GetListTournament();
        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "FootballManagement.Service.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
