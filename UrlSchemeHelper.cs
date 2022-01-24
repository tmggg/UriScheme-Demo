using System.Collections.Generic;

namespace UriScheme.Demo
{
    public static class UrlSchemeHelper
    {
        public static Dictionary<string, string> GetParam(this string[] args)
        {
            Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Contains("?") ? args[i].Split("?")[1] : args[i];
            }
            foreach (var arg in args)
            {
                var data = arg.Split('&');
                foreach (var s in data)
                {
                    var res = s.Split('=');
                    if (res.Length != 2)
                        continue;
                    tempDictionary.Add(res[0], res[1]);
                }
            }
            return tempDictionary;
        }
    }
}
