using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Reuben.Model;

namespace Reuben.Controllers
{
    public class ResourceController
    {
        private StringResource strings;

        public bool LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            strings = JsonConvert.DeserializeObject<StringResource>(File.ReadAllText(fileName));

            return strings != null;
        }

        public bool SaveToFile(string fileName)
        {
            try
            {
                File.WriteAllText(JsonConvert.SerializeObject(strings), fileName);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string GetString(string name)
        {
            if (strings.ResourceTable.ContainsKey(name))
            {
                return strings.ResourceTable[name];
            }

            return "<Missing string>";
        }

        public List<string> GetStringList(string name)
        {
            if (strings.ResourceLists.ContainsKey(name))
            {
                return strings.ResourceLists[name];
            }

            return new List<string>();
        }
    }
}
