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
        public int ProxyId { get; private set; }
        public int InGameId { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Name { get; private set; }
        public int Class { get; private set; }
        public string Group { get; private set; }
        public int MaxLeftX { get; private set; }
        public int MaxRightX { get; private set; }
        public int MaxTopY { get; private set; }
        public int MaxBottomY { get; private set; }
        public List<String> PropertyDescriptions { get; private set; }

        public SpriteDefinition()
        {
            Sprites = new List<SpriteInfo>();
            MaxBottomY = MaxLeftX = MaxRightX = MaxTopY = 0;
            PropertyDescriptions = new List<string>();
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (XAttribute a in e.Attributes())
            {
                switch (a.Name.LocalName.ToLower())
                {
                    case "id":
                        InGameId = a.Value.ToIntFromHex();
                        break;

                    case "width":
                        Width = a.Value.ToInt();
                        break;

                    case "height":
                        Height = a.Value.ToInt();
                        break;

                    case "name":
                        Name = a.Value;
                        break;

                    case "class":
                        Class = a.Value.ToInt();
                        break;

                    case "group":
                        Group = a.Value;
                        break;

                    case "proxy":
                        ProxyId = a.Value.ToInt();
                        break;
                }
            }

            foreach (var x in e.Elements("sprite"))
            {
                SpriteInfo s = new SpriteInfo();
                s.LoadFromElement(x);
                Sprites.Add(s);
                if (s.X > MaxRightX)
                    MaxRightX = s.X;
                if (s.X < MaxLeftX)
                    MaxLeftX = s.X;
                if (s.Y > MaxBottomY)
                    MaxBottomY = s.Y;
                if (s.Y < MaxTopY)
                    MaxTopY = s.Y;
            }

            foreach (var p in e.Elements("property"))
            {
                PropertyDescriptions.Add(p.Value);
            }

            if (MaxLeftX < 0 && MaxLeftX % 16 != 0)
                MaxLeftX -= 16;

            MaxLeftX = (MaxLeftX / 16) * 16;// -(MaxLeftX % 8);

            if (MaxRightX % 16 != 0)
                MaxRightX += 16;

            MaxRightX = ((MaxRightX / 16) + 1) * 16;// -(MaxRightX % 8);

            if (MaxTopY < 0 && MaxTopY % 16 != 0)
                MaxTopY -= 16;
            MaxTopY = (MaxTopY / 16) * 16;// -8 - (MaxTopY % 8);

            if (MaxBottomY % 16 != 0)
                MaxBottomY += 16;

            MaxBottomY = ((MaxBottomY / 16) + 1) * 16;// MaxBottomY % 8;

            return true;
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("spritedefinition");
            e.SetAttributeValue("id", InGameId.ToHexString());
            e.SetAttributeValue("width", Width.ToHexString());
            e.SetAttributeValue("height", Height.ToHexString());
            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("group", Class);
            e.SetAttributeValue("class", Group);
            if (ProxyId > 0)
            {
                e.SetAttributeValue("proxy", ProxyId);
            }

            foreach (var s in Sprites)
            {
                e.Add(s.CreateElement());
            }

            return e;
        }
    }
}
