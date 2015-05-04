using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reuben.UI.ProjectManagement.Enums
{
    public static class ItemList
    {
        public static List<string> ItemNameList;

        static ItemList()
        {
            ItemNameList = new List<string>();
            ItemNameList.Add("None");
            ItemNameList.Add("Super Mushroom");
            ItemNameList.Add("Fire Flower");
            ItemNameList.Add("Super Leaf");
            ItemNameList.Add("Frog Suit");
            ItemNameList.Add("Tanooki Suit");
            ItemNameList.Add("Hammer Suit");
            ItemNameList.Add("Star");
            ItemNameList.Add("Jugem's Cloud");
            ItemNameList.Add("P-Wing");
            ItemNameList.Add("Anchor");
            ItemNameList.Add("Hammer");
            ItemNameList.Add("Warp Whistle");
        }
    }
}
