using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq.JsonPath;
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
    public class JPathPlusParseTests : TestFixtureBase
    {
        [Test]
        public void ParentToken()
        {
            JPath path = new JPath("Blah^");
            Assert.AreEqual(2, path.Filters.Count);
            Assert.AreEqual("Blah", ((FieldFilter)path.Filters[0]).Name);
            Assert.IsInstanceOf<ParentFilter>(path.Filters[1]);
        }

        [Test]
        public void PropertyNameToken()
        {
            JPath path = new JPath("Blah~");
            Assert.AreEqual(2, path.Filters.Count);
            Assert.AreEqual("Blah", ((FieldFilter)path.Filters[0]).Name);
            Assert.IsInstanceOf<PropertyNameFilter>(path.Filters[1]);
        }
    }
}
