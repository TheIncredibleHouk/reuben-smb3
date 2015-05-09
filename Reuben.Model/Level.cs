using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class Level
    {
        public Level()
        {
            Data = new byte[240, 27];
            Sprites = new List<Sprite>();
            Pointers = new List<LevelPointer>();
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int LevelType { get; set; }

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public int GraphicsID { get; set; }

        [DataMember]
        public int AnimationType { get; set; }

        [DataMember]
        public int MusicID { get; set; }

        [DataMember]
        public int NumberOfScreens { get; set; }

        [DataMember]
        public int StartX { get; set; }

        [DataMember]
        public int StartY { get; set; }

        [DataMember]
        public int EventType { get; set; }

        [DataMember]
        public int StartActionType { get; set; }

        [DataMember]
        public bool InvincibleEnemeies { get; set; }

        [DataMember]
        public bool TemporaryProjectileBlockChanges { get; set; }

        [DataMember]
        public bool RhythmPlatforms { get; set; }

        [DataMember]
        public bool DPadControlsTiles { get; set; }

        [DataMember]
        public int PaletteEffectType { get; set; }

        [DataMember]
        public Guid PaletteID { get; set; }

        [DataMember]
        public int MiscByte1 { get; set; }

        [DataMember]
        public int MiscByte2 { get; set; }

        [DataMember]
        public int MiscByte3 { get; set; }

        [DataMember]
        public byte[,] Data { get; set; }

        [DataMember]
        public List<Sprite> Sprites { get; set; }

        [DataMember]
        public List<LevelPointer> Pointers { get; set; }

    }
}
