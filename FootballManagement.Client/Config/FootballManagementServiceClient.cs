using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
namespace FootballManagement.Client.FootballManagementServiceReference
{
    public partial class FootballManagementServiceClient : System.ServiceModel.ClientBase<FootballManagement.Client.FootballManagementServiceReference.IFootballManagementService>, FootballManagement.Client.FootballManagementServiceReference.IFootballManagementService
    {
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint,
             System.ServiceModel.Description.ClientCredentials clientCredentials)
        {
            if (serviceEndpoint.Name ==
                    FootballManagementServiceClient.EndpointConfiguration.BasicHttpBinding_IFootballManagementService.ToString())    
            {
                serviceEndpoint.Binding.SendTimeout = TimeSpan.FromMinutes(2);
                //serviceEndpoint.Binding.MaxReceivedMessageSize = 20000001;
                serviceEndpoint.Binding.CloseTimeout = TimeSpan.FromMinutes(2);
                serviceEndpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(2);
                serviceEndpoint.Binding.OpenTimeout = TimeSpan.FromMinutes(2);
                serviceEndpoint.Binding.SendTimeout = TimeSpan.FromMinutes(2);
                foreach (OperationDescription op in serviceEndpoint.Contract.Operations)
                {
                    var behavior = op.OperationBehaviors.First() as DataContractSerializerOperationBehavior;
                    if (behavior != null)
                    {
                        behavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }

            }
        }
    }
}
