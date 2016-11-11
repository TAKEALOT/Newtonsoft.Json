using System;
using System.Collections.Generic;
using System.Text;
#if !(PORTABLE || PORTABLE40 || NET35 || NET20) || NETSTANDARD1_1
using System.Numerics;
#endif
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Tests.Bson;
#if DNXCORE50
using Xunit;
using Test = Xunit.FactAttribute;
using Assert = Newtonsoft.Json.Tests.XUnitAssert;
#else
using NUnit.Framework;
#endif
using Newtonsoft.Json.Linq;
#if NET20
using Newtonsoft.Json.Utilities.LinqBridge;
#else
using System.Linq;

#endif

namespace Newtonsoft.Json.Tests.Linq.JsonPath
{
    [TestFixture]
    public class JPathPlusExecuteTests : TestFixtureBase
    {
        [Test]
        public void EvaluateParentOnProperty()
        {
            JObject o = JObject.Parse(@"{
        ""Stores"": [
          ""Lambton Quay"",
          ""Willis Street""
        ],
        ""Manufacturers"": [
          {
            ""Name"": ""Acme Co"",
            ""Products"": [
              {
                ""Name"": ""Anvil"",
                ""Price"": 50
              }
            ]
          },
          {
            ""Name"": ""Contoso"",
            ""Products"": [
              {
                ""Name"": ""Elbow Grease"",
                ""Price"": 99.95
              },
              {
                ""Name"": ""Headlight Fluid"",
                ""Price"": 4
              }
            ]
          }
        ]
      }");

            string name = (string)o.SelectToken("Manufacturers[0].Products^.Name");

            Assert.AreEqual("Acme Co", name);
        }

        [Test]
        public void EvaluateParentOnArrayMember()
        {
            JObject o = JObject.Parse(@"{
    ""store"": {
      ""book"": [
        {
          ""category"": ""reference"",
          ""author"": ""Nigel Rees"",
          ""title"": ""Sayings of the Century"",
          ""price"": 8.95
        },
        {
          ""category"": ""fiction"",
          ""author"": ""Evelyn Waugh"",
          ""title"": ""Sword of Honour"",
          ""price"": 12.99
        },
        {
          ""category"": ""fiction"",
          ""author"": ""Herman Melville"",
          ""title"": ""Moby Dick"",
          ""isbn"": ""0-553-21311-3"",
          ""price"": 8.99
        },
        {
          ""category"": ""fiction"",
          ""author"": ""J. R. R. Tolkien"",
          ""title"": ""The Lord of the Rings"",
          ""isbn"": ""0-395-19395-8"",
          ""price"": 22.99
        }
      ],
      ""bicycle"": {
        ""color"": ""red"",
        ""price"": 19.95
      }
    }
  }");

            var hits = o.SelectTokens("store.book[0]^").ToList();

            var expectedHits = new[] { o.SelectToken("store.book") };

            Assert.That(hits, Is.EquivalentTo(expectedHits));
        }

        [Test]
        public void EvaluatePropertyNameOnProperty()
        {
            JObject o = JObject.Parse(@"{
        ""Stores"": [
          ""Lambton Quay"",
          ""Willis Street""
        ],
        ""Manufacturers"": [
          {
            ""Name"": ""Acme Co"",
            ""Products"": [
              {
                ""Name"": ""Anvil"",
                ""Price"": 50
              }
            ]
          },
          {
            ""Name"": ""Contoso"",
            ""Products"": [
              {
                ""Name"": ""Elbow Grease"",
                ""Price"": 99.95
              },
              {
                ""Name"": ""Headlight Fluid"",
                ""Price"": 4
              }
            ]
          }
        ]
      }");

            string name = (string)o.SelectToken("Manufacturers[0].Products~");

            Assert.AreEqual("Products", name);
        }

        [Test]
        public void EvaluatePropertyNameOnWildcardProperty()
        {
            JObject o = JObject.Parse(@"{
        ""Stores"": [
          ""Lambton Quay"",
          ""Willis Street""
        ],
        ""Manufacturers"": [
          {
            ""Name"": ""Acme Co"",
            ""Products"": [
              {
                ""Name"": ""Anvil"",
                ""Price"": 50
              }
            ]
          },
          {
            ""Name"": ""Contoso"",
            ""Products"": [
              {
                ""Name"": ""Elbow Grease"",
                ""Price"": 99.95
              },
              {
                ""Name"": ""Headlight Fluid"",
                ""Price"": 4
              }
            ]
          }
        ]
      }");

            var names = o.SelectTokens("Manufacturers[0].*~").Select(token => (string)token).ToList();

            var expectedNames = new[] { "Name", "Products" };

            Assert.That(names, Is.EquivalentTo(expectedNames));
        }

        [Test]
        public void EvaluatePropertyNameOnArrayMember()
        {
            JObject o = JObject.Parse(@"{
        ""Stores"": [
          ""Lambton Quay"",
          ""Willis Street""
        ],
        ""Manufacturers"": [
          {
            ""Name"": ""Acme Co"",
            ""Products"": [
              {
                ""Name"": ""Anvil"",
                ""Price"": 50
              }
            ]
          },
          {
            ""Name"": ""Contoso"",
            ""Products"": [
              {
                ""Name"": ""Elbow Grease"",
                ""Price"": 99.95
              },
              {
                ""Name"": ""Headlight Fluid"",
                ""Price"": 4
              }
            ]
          }
        ]
      }");

            int index = (int)o.SelectToken("Manufacturers[1]~");

            Assert.AreEqual(1, index);
        }

        [Test]
        public void EvaluatePropertyNameOnWilcardArrayMember()
        {
            JObject o = JObject.Parse(@"{
        ""Stores"": [
          ""Lambton Quay"",
          ""Willis Street""
        ],
        ""Manufacturers"": [
          {
            ""Name"": ""Acme Co"",
            ""Products"": [
              {
                ""Name"": ""Anvil"",
                ""Price"": 50
              }
            ]
          },
          {
            ""Name"": ""Contoso"",
            ""Products"": [
              {
                ""Name"": ""Elbow Grease"",
                ""Price"": 99.95
              },
              {
                ""Name"": ""Headlight Fluid"",
                ""Price"": 4
              }
            ]
          }
        ]
      }");

            var indices = o.SelectTokens("Manufacturers[*]~").Select(token => (int)token).ToList();

            var expectedIndices = new[] { 0, 1 };

            Assert.That(indices, Is.EquivalentTo(expectedIndices));
        }
    }
}
