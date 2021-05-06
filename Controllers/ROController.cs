using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcoreapinet;
using MMThApi;
using static MMThApi.WS_InterfaceCRMSoapClient;
using System.ServiceModel;
using System;
using System.Xml;
using Newtonsoft.Json;
using netcoreapi.Models;
using Microsoft.AspNetCore.Cors;

namespace netcoreapi.Controllers
{
    [Route("api/[controller]")]
    public class ROController : ControllerBase
    {
        public ROController(AppDb db)
        {
            Db = db;
        }
        // POST api/ro
        [HttpPost]
        public async Task<IActionResult> CreateROtoCrm([FromBody] RoCase body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new CasetQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);

        }

        // GET api/blog/5
        [HttpGet("{ronumber}")]
        public async Task<string> GetRONumber(string ro_number )
        {
            //EndpointConfiguration endpoint = new EndpointConfiguration();

            //WS_InterfaceCRMSoapClient myService = new WS_InterfaceCRMSoapClient(endpoint, "http://pre-mmth.dms-ccp.com/WS_InterfaceCRM/WS_InterfaceCRM.asmx?op=WSCheckHistoryRO");



            //WSCheckHistoryRORequest wr = new WSCheckHistoryRORequest();
            //wr.CMPCDE = "110059";
            //wr.OFFCDE = "110059";
            //wr.RONO = "RO59-2103-0016";
            //wr.REQUEST_NO = "123456789012345678901234567890";
            //Task <WSCheckHistoryROResponse> rs =myService.WSCheckHistoryROAsync(wr);
            ////rs.Result
            //// Entry[] entries = myService.
            /////Create instance of SOAP client
            ///WS_InterfaceCRMSoapClient
            ///

            WS_InterfaceCRMSoapClient soapClient = new WS_InterfaceCRMSoapClient( new EndpointConfiguration(), "http://pre-mmth.dms-ccp.com/WS_InterfaceCRM/WS_InterfaceCRM.asmx");
            //Create instance of credentials
           // ImportManager.SC_Credentials credentials = new ImportManager.SC_Credentials();

            using (new OperationContextScope(soapClient.InnerChannel))
            {
                //Create message header containing the credentials
                //var header = MessageHeader.CreateHeader("SC_Credentials",
               // "http://soapservice.com", credentials, new CFMessagingSerializer(typeof(SC_Credentials)));
                //Add the credentials message header to the outgoing request
               // OperationContext.Current.OutgoingMessageHeaders.Add(header);

                try
                {
                    WSCheckHistoryRORequest wr = new WSCheckHistoryRORequest();
                    wr.CMPCDE = "110059";
                    wr.OFFCDE = "110059";
                    wr.RONO = "RO59-2103-0016";
                    wr.REQUEST_NO = "123456789012345678901234567890";

                    var result = Task.Run(async () => await soapClient.WSCheckHistoryROAsync(wr)).GetAwaiter().GetResult();
                    
                    var cv = result.WSCheckHistoryROResult.Any1.InnerXml.ToString();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(cv);

                    string json = JsonConvert.SerializeXmlNode(doc);

                    Console.WriteLine(json);




                    return json;
                }
                catch (Exception ex)
                {
                    return "";
                    //throw;
                }
            }


            
           
        }


        // GET api/blog/5
        [HttpGet("valid/{ValidateRONumber}")]
        public async Task<IActionResult> ValidateRONumber(string ValidateRONumber)
        {

            await Db.Connection.OpenAsync();
            var query = new CasetQuery(Db);
            var result = await query.ValidateRunumber(ValidateRONumber);
            return new OkObjectResult(result!=null?result:new RoCase());

        }

        // GET api/blog/5
        [HttpGet("list/{dealer}")]
        public async Task<IActionResult> ListbyDealer(string dealer,string userid)
        {

            await Db.Connection.OpenAsync();
            var query = new CasetQuery(Db);
            var result = await query.ListOwnerCase(dealer, userid);
            return new OkObjectResult(result != null ? result : new RoCase());

        }
        public AppDb Db { get; }
    }

   
}