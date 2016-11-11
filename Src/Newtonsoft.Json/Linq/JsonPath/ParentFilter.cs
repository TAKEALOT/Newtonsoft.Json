using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
    internal class ParentFilter : PathFilter
    {
        public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
        {
            foreach (JToken t in current)
            {
                JToken v = t.Parent;

                if (v != null)
                {
                    if (v.Type == JTokenType.Property)
                    {
                        v = v.Parent;
                        if (v != null)
                        {
                            yield return v;
                        }
                    }
                    else
                    {
                        yield return v;
                    }
                }
                else if (errorWhenNoMatch)
                {
                    throw new JsonException("JToken does not have a parent.");
                }
            }
        }
    }
}