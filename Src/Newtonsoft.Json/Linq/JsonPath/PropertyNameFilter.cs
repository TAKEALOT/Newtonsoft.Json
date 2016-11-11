using Newtonsoft.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Newtonsoft.Json.Linq.JsonPath
{
    internal class PropertyNameFilter : PathFilter
    {
        public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
        {
            foreach (JToken t in current)
            {
                yield return GetPropertyName(t);
            }
        }

        private static JToken GetPropertyName(JToken token)
        {
            // this code was adapted from get_Path on JToken.
            // It does not seem straightforward to remove the for loop, since JTokens don't always nicely correspond to items in JsonPath

            if (token.Parent == null)
            {
                return string.Empty;
            }

            JToken previous = null;
            for (JToken current = token; current != null; current = current.Parent)
            {
                switch (current.Type)
                {
                    case JTokenType.Property:
                        return ((JProperty)current).Name;
                    case JTokenType.Array:
                    case JTokenType.Constructor:
                        if (previous != null)
                        {
                            return ((IList<JToken>)current).IndexOf(previous);
                        }
                        break;
                }

                previous = current;
            }

            throw new InvalidOperationException("Unable to get property name for token with path '{0}'".FormatWith(CultureInfo.InvariantCulture, token.Path));
        }
    }
}