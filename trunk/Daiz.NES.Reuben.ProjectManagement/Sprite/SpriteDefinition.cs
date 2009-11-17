using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpriteDefinition : IXmlIO
    {
        public List<SpriteInfo> Sprites { get; private set; }
        public int InGameId { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Name { get; private set; }
        public int Group { get; private set; }
        public string Class { get; private set; }
        public int MaxLeftX { get; private set; }
        public int MaxRightX { get; private set; }
        public int MaxTopY { get; private set; }
        public int MaxBottomY { get; private set; }

        public SpriteDefinition()
        {
            Sprites = new List<SpriteInfo>();
            MaxBottomY = MaxLeftX = MaxRightX = MaxTopY = 0;
        }

        public bool LoadFromElement(XElement e)
        {
            InGameId = e.Attribute("id").Value.ToIntFromHex();
            Width = e.Attribute("width").Value.ToInt();
            Height = e.Attribute("height").Value.ToInt();
            Name = e.Attribute("name").Value;
            Group = e.Attribute("group").Value.ToInt();
            Class = e.Attribute("class").Value;

            foreach (var x in e.Elements("sprite"))
            {
                SpriteInfo s = new SpriteInfo();
                s.LoadFromElement(x);
                Sprites.Add(s);
                if (s.X + 8 > MaxRightX) MaxRightX = s.X + 8;
                if (s.X < MaxLeftX) MaxLeftX = s.X;
                if (s.Y + 16 > MaxBottomY) MaxBottomY = s.Y + 16;
                if (s.Y < MaxTopY) MaxTopY = s.Y;
            }
            if (MaxLeftX % 8 != 0) MaxLeftX -= 8 - (MaxLeftX % 8);
            if (MaxRightX % 8 != 0) MaxRightX += 8 - (MaxRightX % 8);
            if (MaxTopY % 8 != 0) MaxTopY -= 8 - 8 - (MaxTopY % 8);
            if (MaxBottomY % 8 != 0) MaxBottomY += MaxBottomY % 8;

            return true;
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("spritedefinition");
            e.SetAttributeValue("id", InGameId.ToHexString());
            e.SetAttributeValue("width", Width.ToHexString());
            e.SetAttributeValue("height", Height.ToHexString());
            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("group", Group);
            e.SetAttributeValue("class", Class);

            foreach (var s in Sprites)
            {
                e.Add(s.CreateElement());
            }

            return e;
        }
    }
}
