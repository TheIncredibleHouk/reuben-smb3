using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class MusicManager
    {
        public List<Music> MusicList { get; private set; }

        public MusicManager()
        {
            MusicList = new List<Music>();
        }

        public void LoadDefault()
        {
            XDocument xDoc = XDocument.Parse(Resource.music);
            MusicList.Clear();
            foreach (var e in xDoc.Element("music").Elements("track"))
            {
                Music m = new Music();
                m.LoadFromElement(e);
                MusicList.Add(m);
            }
        }

        public bool LoadMusic(string filename)
        {
            if (!File.Exists(filename)) return false;

            XDocument xDoc = XDocument.Load(filename);
            MusicList.Clear();
            foreach (var e in xDoc.Element("music").Elements("track"))
            {
                Music m = new Music();
                m.LoadFromElement(e);
                MusicList.Add(m);
            }

            return true;
        }

        public void Save(string filename)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("music");

            foreach (var m in MusicList)
            {
                root.Add(m.CreateElement());
            }

            xDoc.Add(root);
            xDoc.Save(filename);
        }
    }
}
