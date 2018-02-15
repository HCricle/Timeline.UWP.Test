using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Timeline.Framework.Drawing.PaintingSuface;
using Timeline.Framework.RPG;
using Timeline.Framework.RPG.RPGDatas;
using Timeline.Framework.RPG.RPGParts;
using Timeline.Framework.RPG.RPGSettings;
using Timeline.UWP.RPG.Test.Books.Pages;
using Timeline.UWP.RPG.Test.Books.Pages.Layers;

namespace Timeline.UWP.RPG.Test.Books
{
    public class StoryBook : RPGBook
    {
        static StoryBook()
        {
            PlayerSettings.PlayerImgUri = new Uri("ms-appx:///Assets/a.png");//设置主角贴图，规定是128*96（高*宽）的图
        }

        public StoryBook()
        {
            base.AddPage<Page0>();//加入一个页，此时不会被实例化

            SetActivePage(0);//设置活动页，这时才会实例化
        }        
    }
}
