using Microsoft.AspNetCore.Mvc;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Query;
using netcoreapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace netcoreapi.Controllers
{
    public class CRMController : Controller
    {
        public IOrganizationService _service = null;
        private string _discoveryServiceAddress = "https://thcrmweb01.ad-mmth.th.mitsubishi-motors.com/XRMServices/2011/Discovery.svc";
        private string _OrganizeServiceAddress = "https://thcrmweb01.ad-mmth.th.mitsubishi-motors.com/mmthqas/XRMServices/2011/Organization.svc";
        private string _organizationUniqueName = "mmthqas";
        // Provide your user name and password.
        private string _userName = "crmapp";
        private string _password = "P@$$w0rd";
        private string _domain = "ad-mmth";
        private OrganizationServiceProxy _serviceProxy;
        public void ConnectToMSCRM(string UserName, string Password, string SoapOrgServiceUri)
        {

            try
            {
                IServiceManagement<IDiscoveryService> serviceManagement =
                           ServiceConfigurationFactory.CreateManagement<IDiscoveryService>(
                           new Uri(_discoveryServiceAddress));
                AuthenticationProviderType endpointType = AuthenticationProviderType.ActiveDirectory;

                // Set the credentials.
                AuthenticationCredentials authCredentials = GetCredentials(serviceManagement, endpointType);


                String organizationUri = String.Empty;
                // Get the discovery service proxy.
                using (DiscoveryServiceProxy discoveryProxy =
                    GetProxy<IDiscoveryService, DiscoveryServiceProxy>(serviceManagement, authCredentials))
                {
                    // Obtain organization information from the Discovery service. 
                    if (discoveryProxy != null)
                    {
                        // Obtain information about the organizations that the system user belongs to.
                        OrganizationDetailCollection orgs = DiscoverOrganizations(discoveryProxy);
                        // Obtains the Web address (Uri) of the target organization.
                        organizationUri = FindOrganization(_organizationUniqueName,
                            orgs.ToArray()).Endpoints[EndpointType.OrganizationService];

                    }
                }
                if (!String.IsNullOrWhiteSpace(organizationUri))
                {
                    IServiceManagement<IOrganizationService> orgServiceManagement =
                        ServiceConfigurationFactory.CreateManagement<IOrganizationService>(
                        new Uri(organizationUri));

                    // Set the credentials.
                    AuthenticationCredentials credentials = GetCredentials(orgServiceManagement, endpointType);

                    // Get the organization service proxy.
                    using (OrganizationServiceProxy organizationProxy =
                        GetProxy<IOrganizationService, OrganizationServiceProxy>(orgServiceManagement, credentials))
                    {
                        // This statement is required to enable early-bound type support.
                        organizationProxy.EnableProxyTypes();
                        _serviceProxy = organizationProxy;
                        // Now make an SDK call with the organization service proxy.
                        // Display information about the logged on user.
                        Guid userid = ((WhoAmIResponse)organizationProxy.Execute(
                            new WhoAmIRequest())).UserId;
                        Entity systemUser = organizationProxy.Retrieve("systemuser", userid,
                            new ColumnSet(new string[] { "firstname", "lastname" }));
                        _service = (IOrganizationService)organizationProxy;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ex1"+ ex.Message);
                //  Console.WriteLine("Error while connecting to CRM " + ex.Message);
                //  Console.ReadKey();
            }
        }
        /// <summary>
        /// Obtain the AuthenticationCredentials based on AuthenticationProviderType.
        /// </summary>
        /// <param name="service">A service management object.</param>
        /// <param name="endpointType">An AuthenticationProviderType of the CRM environment.</param>
        /// <returns>Get filled credentials.</returns>
        private AuthenticationCredentials GetCredentials<TService>(IServiceManagement<TService> service, AuthenticationProviderType endpointType)
        {
            AuthenticationCredentials authCredentials = new AuthenticationCredentials();

            switch (endpointType)
            {
                case AuthenticationProviderType.ActiveDirectory:
                    authCredentials.ClientCredentials.Windows.ClientCredential =
                        new System.Net.NetworkCredential(_userName,
                            _password,
                            _domain);
                    break;
                case AuthenticationProviderType.LiveId:
                    authCredentials.ClientCredentials.UserName.UserName = _userName;
                    //authCredentials.ClientCredentials.UserName.Password = _password;
                    //authCredentials.SupportingCredentials = new AuthenticationCredentials();
                    //authCredentials.SupportingCredentials.ClientCredentials =
                    //    Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
                    break;
                default: // For Federated and OnlineFederated environments.                    
                    authCredentials.ClientCredentials.UserName.UserName = _userName;
                    authCredentials.ClientCredentials.UserName.Password = _password;
                    // For OnlineFederated single-sign on, you could just use current UserPrincipalName instead of passing user name and password.
                    // authCredentials.UserPrincipalName = UserPrincipal.Current.UserPrincipalName;  // Windows Kerberos

                    // The service is configured for User Id authentication, but the user might provide Microsoft
                    // account credentials. If so, the supporting credentials must contain the device credentials.
                    if (endpointType == AuthenticationProviderType.OnlineFederation)
                    {
                        //IdentityProvider provider = service.GetIdentityProvider(authCredentials.ClientCredentials.UserName.UserName);
                        //if (provider != null & amp; &amp; provider.IdentityProviderType == IdentityProviderType.LiveId)
                        //    {
                        //    authCredentials.SupportingCredentials = new AuthenticationCredentials();
                        //    authCredentials.SupportingCredentials.ClientCredentials =
                        //        Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
                        //}
                    }

                    break;
            }

            return authCredentials;
        }

        /// <summary>
        /// Discovers the organizations that the calling user belongs to.
        /// </summary>
        /// <param name="service">A Discovery service proxy instance.</param>
        /// <returns>Array containing detailed information on each organization that 
        /// the user belongs to.</returns>
        public OrganizationDetailCollection DiscoverOrganizations(
            IDiscoveryService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            RetrieveOrganizationsRequest orgRequest = new RetrieveOrganizationsRequest();
            RetrieveOrganizationsResponse orgResponse =
                (RetrieveOrganizationsResponse)service.Execute(orgRequest);

            return orgResponse.Details;
        }

        /// <summary>
        /// Finds a specific organization detail in the array of organization details
        /// returned from the Discovery service.
        /// </summary>
        /// <param name="orgUniqueName">The unique name of the organization to find.</param>
        /// <param name="orgDetails">Array of organization detail object returned from the discovery service.</param>
        /// <returns>Organization details or null if the organization was not found.</returns>
        /// <seealso cref="DiscoveryOrganizations"/>
        public OrganizationDetail FindOrganization(string orgUniqueName,
            OrganizationDetail[] orgDetails)
        {
            if (String.IsNullOrWhiteSpace(orgUniqueName))
                throw new ArgumentNullException("orgUniqueName");
            if (orgDetails == null)
                throw new ArgumentNullException("orgDetails");
            OrganizationDetail orgDetail = null;

            foreach (OrganizationDetail detail in orgDetails)
            {
                if (String.Compare(detail.UniqueName, orgUniqueName,
                    StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    orgDetail = detail;
                    break;
                }
            }
            return orgDetail;
        }

        /// <summary>
        /// Generic method to obtain discovery/organization service proxy instance.
        /// </summary>
        /// <typeparam name="TService">
        /// Set IDiscoveryService or IOrganizationService type to request respective service proxy instance.
        /// </typeparam>
        /// <typeparam name="TProxy">
        /// Set the return type to either DiscoveryServiceProxy or OrganizationServiceProxy type based on TService type.
        /// </typeparam>
        /// <param name="serviceManagement">An instance of IServiceManagement</param>
        /// <param name="authCredentials">The user's Microsoft Dynamics CRM logon credentials.</param>
        /// <returns></returns>
        private TProxy GetProxy<TService, TProxy>(
            IServiceManagement<TService> serviceManagement,
            AuthenticationCredentials authCredentials)
            where TService : class
            where TProxy : ServiceProxy<TService>
        {
            Type classType = typeof(TProxy);

            if (serviceManagement.AuthenticationType !=
                AuthenticationProviderType.ActiveDirectory)
            {
                AuthenticationCredentials tokenCredentials =
                    serviceManagement.Authenticate(authCredentials);
                // Obtain discovery/organization service proxy for Federated, LiveId and OnlineFederated environments. 
                // Instantiate a new class of type using the 2 parameter constructor of type IServiceManagement and SecurityTokenResponse.
                return (TProxy)classType
                    .GetConstructor(new Type[] { typeof(IServiceManagement<TService>), typeof(SecurityTokenResponse) })
                    .Invoke(new object[] { serviceManagement, tokenCredentials.SecurityTokenResponse });
            }

            // Obtain discovery/organization service proxy for ActiveDirectory environment.
            // Instantiate a new class of type using the 2 parameter constructor of type IServiceManagement and ClientCredentials.
            return (TProxy)classType
                .GetConstructor(new Type[] { typeof(IServiceManagement<TService>), typeof(ClientCredentials) })
                .Invoke(new object[] { serviceManagement, authCredentials.ClientCredentials });
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/blog/5
        [HttpGet("{CaseID}")]
        public async Task<IActionResult> ListbyDealer(string CaseID)
        {
            CaseID = "CAS-159095-Z3Q5N0";
            try
            {
                if (_service == null)
                {
                    ConnectToMSCRM(_userName, _password, _OrganizeServiceAddress);
                }

                var columns = new ColumnSet();
                columns.AllColumns = true;

                FilterExpression filter = new FilterExpression(LogicalOperator.Or);

                FilterExpression filter1 = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression("ticketnumber", ConditionOperator.Equal, CaseID ),
                        new ConditionExpression("ticketnumber", ConditionOperator.NotNull)
                    },
                };

                

                filter.AddFilter(filter1);
                //filter.AddFilter(filter2);
                //filter.AddFilter(filter3);

                var query = new QueryExpression("incident");
                query.ColumnSet = columns;
                query.Criteria = filter;
                query.TopCount = 5;

                EntityCollection result = _service.RetrieveMultiple(query);

                return Json(result.Entities.ToList());
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }


        private ResultModel Create(MyEntity myentity)
        {
            try
            {
                if (_service == null)
                {
                    ConnectToMSCRM(_userName, _password, _OrganizeServiceAddress);
                }

                Entity entity = new Entity(myentity.LogicalName);
                var model = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(myentity.Fields);

                foreach (var item in model)
                {
                    string field = item.Key;
                    Dictionary<string, object> attribute = item.Value;
                    string type = attribute["Type"].ToString();

                    switch (type)
                    {
                        case "OptionSet":
                            {
                                int value = int.Parse(item.Value["Value"].ToString());
                                entity[field] = new OptionSetValue(value);
                            }; break;
                        case "DateTime":
                            {
                                string date = item.Value["Date"].ToString();
                                int dd = int.Parse(date.Split('/')[0]);
                                int MM = int.Parse(date.Split('/')[1]);
                                int yyyy = int.Parse(date.Split('/')[2]);

                                string time = item.Value["Time"] != null ? item.Value["Time"].ToString() : "00:00:00";
                                int hh = int.Parse(time.Split(':')[0]);
                                int mm = int.Parse(time.Split(':')[1]);
                                int ss = int.Parse(time.Split(':')[2]);

                                entity[field] = new DateTime(yyyy, MM, dd, hh, mm, ss);
                            }; break;
                        case "Lookup":
                            {
                                string logicalname = item.Value["LogicalName"].ToString();
                                string refid = item.Value["Id"].ToString();

                                entity[field] = new EntityReference(logicalname, new Guid(refid));
                            }; break;
                        case "Money":
                            {
                                string value = item.Value["Value"].ToString();
                                entity[field] = new Money(Decimal.Parse(value));
                            }; break;
                        default:
                            {
                                entity[field] = Convert.ChangeType(item.Value["Value"], Type.GetType("System." + type));
                            }
                            break;
                    }
                }

                entity["createdby"] = "SYSTEM";
                entity["createdon"] = DateTime.Now;
                entity["modifiedby"] = "SYSTEM";
                entity["modifiedon"] = DateTime.Now;

                Guid id = _service.Create(entity);

                return new ResultModel() { Status = "S", Message = "Create Complete", Result = id };
            }
            catch (Exception ex)
            {
                return new ResultModel() { Status = "E", Message = ex.Message };
            }
        }
    }
}
