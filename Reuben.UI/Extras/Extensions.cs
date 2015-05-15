using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace Reuben.UI
{
    public static class Extensions
    {
        public static T MakeCopy<T>(this T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        public static Rectangle Combine(this IEnumerable<Rectangle> rectangles)
        {
            if (rectangles.Count() == 0)
            {
                return Rectangle.Empty;
            }

            Rectangle original = rectangles.First();
            foreach (var r in rectangles)
            {
                original = Rectangle.Union(original, r);
            }

            original.Width++;
            original.Height++;
            return original;
        }
    }
}
