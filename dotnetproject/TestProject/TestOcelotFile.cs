using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Ocelot.Tests
{
    [TestFixture]
    public class OcelotJsonTests
    {
        private JObject ocelotJson;

        [SetUp]
        public void Setup()
        {
            // Load the Ocelot.json file for testing (replace with your actual file path)
            string jsonFilePath = @"D:\dotnetSolutions\MicroservicesProject\VirtusaProjects\CustomerCQRSProject\dotnetproject\dotnetapigateway\Ocelot.json";
            string jsonText = System.IO.File.ReadAllText(jsonFilePath);
            ocelotJson = JObject.Parse(jsonText);
        }

       [Test]
        public void VerifyAddCustomerRoute()
        {
            var CustomerRoute = ocelotJson["Routes"][2];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/Customer"));
            Assert.That(CustomerRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(7214));
        }

        [Test]
        public void VerifyDeleteCustomerIDRoute()
        {
            var CustomerRoute = ocelotJson["Routes"][3];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/Customer/{id}"));
            Assert.That(CustomerRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(7214));
        }
[Test]
        public void VerifyCustomerRoute()
        {
            var CustomerRoute = ocelotJson["Routes"][0];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/Customer"));
            Assert.That(CustomerRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(6200));
        }

        [Test]
        public void VerifyCustomerIDRoute()
        {
            var CustomerRoute = ocelotJson["Routes"][1];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/Customer/{id}"));
            Assert.That(CustomerRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(CustomerRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(6200));
        }

        [Test]
        public void VerifyCustomerRouteUpstreamPath()
        {
            var CustomerRoute = ocelotJson["Routes"][0];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/GetCustomer"));
        }
        [Test]
        public void VerifyCustomerIDRouteUpstreamPath()
        {
            var CustomerRoute = ocelotJson["Routes"][1];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/GetCustomer/{id}"));
        }
        [Test]
        public void VerifyCustomersRouteUpstreamPath()
        {
            var CustomerRoute = ocelotJson["Routes"][2];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Customer"));
        }
        [Test]
        public void VerifyCustomerNamesRouteUpstreamPath()
        {
            var CustomerRoute = ocelotJson["Routes"][3];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Customer/{id}"));
        }

        [Test]
        public void VerifyCustomerRouteHttpMethods()
        {
            var CustomerRoute = ocelotJson["Routes"][0];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "GET" }));
        }
        [Test]
        public void VerifyCustomerIDRouteHttpMethods()
        {
            var CustomerRoute = ocelotJson["Routes"][1];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "GET" }));
        }
        [Test]
        public void VerifyCustomersRouteHttpMethods()
        {
            var CustomerRoute = ocelotJson["Routes"][2];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "POST"}));
        }

        [Test]
        public void VerifyCustomerDeleteIDRouteHttpMethods()
        {
            var CustomerRoute = ocelotJson["Routes"][3];

            Assert.That(CustomerRoute, Is.Not.Null);
            Assert.That(CustomerRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "DELETE" }));
        }
    }
}
