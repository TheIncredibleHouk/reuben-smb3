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
    public class StringController
    {
        public StringResource Resource { get; set; }
        private string lastFile;

        public bool Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            lastFile = fileName;
            Resource = JsonConvert.DeserializeObject<StringResource>(File.ReadAllText(fileName));

            return Resource != null;
        }

        public void Save()
        {
            Save(lastFile);
        }

        public bool Save(string fileName)
        {
            try
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(Resource));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<string> GetStringList(string name)
        {
            string key = Resource.ResourceLists.Keys.Where(k => k.ToLower() == name).FirstOrDefault();
            if(key != null)
            {
                return Resource.ResourceLists[key];
            }

            return new List<string>();
        }
    }
}
