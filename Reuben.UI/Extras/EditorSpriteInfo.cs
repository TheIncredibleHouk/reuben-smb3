using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reuben.Model;

namespace Reuben.UI
{
    public class EditorSpriteInfo
    {
        public static string Serialize(List<SpriteInfo> infos)
        {
            List<string> strings = new List<string>();
            foreach(SpriteInfo info in infos)
            {
            strings.Add(string.Format("X={0} Y={1} Sprite={2:X2} Table={3:X2} Palette={4:X2} Overlay={5} HFlip={6} VFlip={7} Properties={8}",
                                    info.X,
                                    info.Y,
                                    info.Value,
                                    info.Table,
                                    info.Palette,
                                    info.Overlay,
                                    info.HorizontalFlip,
                                    info.VerticalFlip,
                                    String.Join(",", info.Properties.OrderBy(p => p).Select(p => p.ToString()))
                                    ));
            }

            return string.Join("\r\n", strings);
        }

        public static List<SpriteInfo> Deserialize(string serializedInfo)
        {
            List<SpriteInfo> infos = new List<SpriteInfo>();
            foreach (string info in serializedInfo.Split('\n'))
            {
                SpriteInfo spriteInfo = new SpriteInfo();
                try
                {
                    string[] split = info.Split(' ');
                    foreach (string s in split)
                    {
                        string[] split2 = s.Split('=');
                        switch (split2[0].ToUpper().Trim())
                        {
                            case "X":
                                spriteInfo.X = Convert.ToInt32(split2[1]);
                                break;

                            case "Y":
                                spriteInfo.Y = Convert.ToInt32(split2[1]);
                                break;

                            case "SPRITE":
                                spriteInfo.Value = Convert.ToInt32(split2[1], 16);
                                break;

                            case "TABLE":
                                spriteInfo.Table = Convert.ToInt32(split2[1], 16);
                                break;

                            case "PALETTE":
                                spriteInfo.Palette = Convert.ToInt32(split2[1], 16);
                                break;

                            case "OVERLAY":
                                spriteInfo.Overlay = Convert.ToBoolean(split2[1]);
                                break;

                            case "HFLIP":
                                spriteInfo.HorizontalFlip = Convert.ToBoolean(split2[1]);
                                break;

                            case "VFLIP":
                                spriteInfo.VerticalFlip = Convert.ToBoolean(split2[1]);
                                break;

                            case "PROPERTIES":
                                spriteInfo.Properties = split2[1].Split(',').Select(p => Convert.ToInt32(p)).OrderBy(p => p).ToList();
                                break;
                        }
                    }
                }
                catch
                {
                }

                infos.Add(spriteInfo);
            }

            return infos;
        }
    }
}
