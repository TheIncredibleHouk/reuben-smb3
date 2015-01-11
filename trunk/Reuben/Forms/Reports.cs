using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder output = new System.Text.StringBuilder();
            Dictionary<BlockProperty, List<string>> powerupProperty = new Dictionary<BlockProperty, List<string>>();
            powerupProperty[BlockProperty.FireFlower] = new List<string>();
            powerupProperty[BlockProperty.SuperLeaf] = new List<string>();
            powerupProperty[BlockProperty.FrogSuit] = new List<string>();
            powerupProperty[BlockProperty.KoopaSuit] = new List<string>();
            powerupProperty[BlockProperty.SledgeSuit] = new List<string>();
            powerupProperty[BlockProperty.IceFlower] = new List<string>();
            powerupProperty[BlockProperty.FireFoxSuit] = new List<string>();
            powerupProperty[BlockProperty.BooSuit] = new List<string>();
            powerupProperty[BlockProperty.NinjaSuit] = new List<string>();
            foreach (LevelInfo info in ProjectController.LevelManager.Levels)
            {
                Level level = new Level();
                level.Load(info);
                BlockDefinition definition = ProjectController.BlockManager.GetDefiniton(level.Type);

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        BlockProperty bp = definition[level.LevelData[x, y]].BlockProperty;
                        if (powerupProperty.ContainsKey(bp))
                        {
                            if (!powerupProperty[bp].Contains(info.Name))
                            {
                                powerupProperty[bp].Add(info.Name);
                            }
                        }

                    }
                }
            }

            foreach (BlockProperty key in powerupProperty.Keys)
            {
                output.Append(key.ToString() + "(" + powerupProperty[key].Count + "): ");
                foreach (string s in powerupProperty[key])
                {
                    output.Append(s + ", ");
                }
                output.Append("\r\n\r\n\r\n");
            }

            Output.Text = output.ToString();
        }
    }
}
