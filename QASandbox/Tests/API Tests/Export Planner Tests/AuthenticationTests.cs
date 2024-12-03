using System.Net;
using RestSharp;

namespace QASandbox.Tests
{
    [TestFixture]
    public class ApiTests
    {
        private RestClient? client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://devapi.portal.za.steinweg.com:7050");
        }

        [TearDown]
        public void Teardown()
        {
            client?.Dispose();
        }

        [Test]
        public void GivenExportPlanner_WhenHttpRequestIsMade_ThenValidateRequiredFieldsExist()
        {
            // Create the request
            var request = new RestRequest("/api/exports", Method.Get);

            // Execute the request
            var response = client.Execute<List<ExportPlannerModel>>(request);

            // Assert that the response status code is OK
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Ensure the response data is not null and has at least 2 items
            Assert.IsNotNull(response.Data);
            Assert.That(response.Data, Has.Count.GreaterThanOrEqualTo(2));

            // Get the item at index 1
            var item = response.Data[1];

            // Ensure that accessing the fields does not throw an exception, confirming that the fields exist in the response.
            Assert.DoesNotThrow(() =>
            {
                var openStackCheck = item.open_stack_check;
                var closeStackCheck = item.close_stack_check;
                var id = item.id;
                var shipmentNumber = item.shipment_number;
                var bookingReference = item.booking_reference;
                var customer = item.customer;
                var commodity = item.commodity;
                var whs = item.Whs;
                var emptyReleaseDate = item.empty_release_date;
                var vessel = item.vessel;
                var line = item.line;
                var Voy = item.Voy;
                var qty20s = item.Qty20s;
                var qty40s = item.Qty40s;
                var qtyFcl = item.Qty_FCL;
                var mtyRec = item.MTY_Rec;
                var osMty = item.OS_MTY;
                var pack = item.Pack;
                var osPack = item.OS_Pack;
                var disp = item.Disp;
                var av = item.AV;
                var fclIn = item.FCL_In;
                var stack = item.Stack;
                var osStack = item.OS_Stack;
                var transportMode = item.transport_mode;
                var berth = item.berth;
                var transnetStackOpen = item.transnet_stack_open;
                var transnetStackClose = item.transnet_stack_close;
                var bosStackOpen = item.bos_stack_open;
                var bosStackClose = item.bos_stack_close;
                var ttsc = item.ttsc;
                var qtyInDem = item.Qty_in_DEM;
                var qtyAtRisk = item.Qty_at_risk;
                var estimatedDemCost = item.Estimated_DEM_cost;
                var controller = item.controller;
                var orderDate = item.order_date;
                var orderNumber = item.order_number;
                var contactPerson = item.contact_person;
                var calcType = item.calc_type;
                var financeBank = item.finance_bank;
                var holdDispatch = item.hold_dispatch;
                var pickupDepot = item.pickup_depot;
                var bookingStatus = item.booking_status;
                var comment = item.comment;
                var transportModeDispatch = item.transport_mode_dispatch;
            });
        }
    }
}
