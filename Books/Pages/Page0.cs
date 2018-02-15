using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Timeline.Framework.Drawing.PaintingSuface;
using Timeline.Framework.RPG.RPGPages;
using Timeline.UWP.RPG.Test.Books.Pages.Layers;

namespace Timeline.UWP.RPG.Test.Books.Pages
{
    public class Page0 : Page
    {
        public Page0(Book book)
            : base(book)
        {
            MapLayer = BuildLayer<Map0>();//创建一个图层
        }
        public Map0 MapLayer { get; }
    }
}
