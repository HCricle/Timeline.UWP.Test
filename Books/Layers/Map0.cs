using System;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Timeline.Framework.Drawing.PaintingSuface;
using Timeline.Framework.Input;
using Timeline.Framework.RPG.RPGAnimaltor;
using Timeline.Framework.RPG.RPGLayers;
using Timeline.Framework.RPG.RPGParts;
using Timeline.Framework.RPG.RPGSettings;
using Timeline.Framework.RPG.RPGSprites;
using Timeline.Framework.MagicResource;

//写注释
namespace Timeline.UWP.RPG.Test.Books.Pages.Layers
{
    /// <summary>
    /// 地图图层
    /// </summary>
    public class Map0 : MapLayer
    {
        /// <summary>
        /// 地图键值，任意
        /// </summary>
        private object Map1Key => GetHashCode();
        /// <summary>
        /// Bgm键值，任意
        /// </summary>
        private readonly object BgmKey = "BgmMap1";
        /// <summary>
        /// 声音资源组件
        /// </summary>
        private VoiceResource bgm;
        /// <summary>
        /// 交换绘画框组件
        /// </summary>
        private SwapDrawBoundsSprite boom;//爆炸
        /// <summary>
        /// 中间字组件
        /// </summary>
        private CententPageSprite CententPage;
        public Map0(Page page)
            : base(page)
        {            
            Key = Map1Key;
            MapUri = new Uri("ms-appx:///Map/Map1.png");//设置地图文件位置
            UnCrossFileUri = new Uri("ms-appx:///Map/Map1.txt");//设置不可通行区域位置
            PlayerStartPoint = new Vector2(416, 64);          //设置主角起始位置  
            Run();                        
        }
        protected async override void InitMapComplated(ICanvasResourceCreator creator)
        {
            //对于声音可以自已找个MP3测试
            /*
            bgm = ResourceManager.BuildResource<VoiceResource>(BgmKey,new Uri("ms-appx:///Assets/1.mp3"));//加载声音资源
            await bgm.LoadAsync();//装载声音资源
            bgm.Play();//播放声音
            */
            var res = ResourceManager.BuildResource<ImgResource>(10086, new Uri("ms-appx:///Assets/Fire3.png"));//加载图片资源
            await res.LoadAsync();//装载图片
            await res.ToCanvasBitmapAsync(creator);//转换为可画图片资源（流会关闭）
            boom = BuildSprite<SwapDrawBoundsSprite>();//生成组件
            boom.Bitmap = res.Bitmap;//设置组件绘画图
            boom.SwapTimeMs = 140;//交换速度（Ms）
            boom.IsForver = false;//是否一直播放
            base.InitMapComplated(creator);
        }
        /// <summary>
        /// 加载人物在某个位置，按下Enter或者Space键触发的事件
        /// </summary>
        protected override void InitKeyDownActions()
        {
            //茶
            AddKeyDownActions(160, 96, ShowTeaText);
            AddKeyDownActions(192, 96, ShowTeaText);
            AddKeyDownActions(224, 128, ShowTeaText);
            AddKeyDownActions(224, 160, ShowTeaText);
            AddKeyDownActions(192, 192, ShowTeaText);
            AddKeyDownActions(160, 192, ShowTeaText);
            AddKeyDownActions(128, 160, ShowTeaText);
            AddKeyDownActions(128, 128, ShowTeaText);

            //床
            AddKeyDownActions(416, 96, ShowBedTexts);
            AddKeyDownActions(448, 96, ShowBedTexts);
            AddKeyDownActions(384, 96, ShowBedTexts);
            AddKeyDownActions(416, 128, ShowBedTexts);
            AddKeyDownActions(448, 128, ShowBedTexts);

            //书
            AddKeyDownActions(384, 352, ShowBookTexts);
            AddKeyDownActions(416, 352, ShowBookTexts);
            AddKeyDownActions(352, 384, ShowBookTexts);
            AddKeyDownActions(384, 416, ShowBookTexts);
            AddKeyDownActions(416, 416, ShowBookTexts);
            AddKeyDownActions(448, 384, ShowBookTexts);
            //时间
            AddKeyDownActions(64, 64, ShowLocTime);
            base.InitKeyDownActions();
        }
        /// <summary>
        /// 加载人物在某个位置触发的事件
        /// </summary>
        protected override void InitOnActions()
        {
            AddOnActions(224, 416, GameOverTexts);
            AddOnActions(256, 416, GameOverTexts);
            base.InitOnActions();
        }
        string[] teaTexts = {"有三杯茶" };
        public void ShowTeaText()
        {
            TalkListSprite.SetTalkList(teaTexts);//对话框显示并说话
        }
        string[] bedTexts = { "一张床" };
        public void ShowBedTexts()
        {
            TalkListSprite.SetTalkList(bedTexts);//对话框显示并说话
        }
        public void GameOverTexts()
        {
            BuildCententPage();
            PlayerSettings.Player.CanMove = false;//设置人物不可动
            boom.X = Page.Camera.X+100;
            boom.Y = Page.Camera.Y+100;
            boom.Play();//播放交换框
        }
        public void ShowBookTexts()
        {
            BuildCententPage();
            PlayerSettings.Player.CanMove = false;
            CententPage.Resume("一本书");//播放中间字
        }
        public void ShowLocTime()
        {
            BuildCententPage();
            PlayerSettings.Player.CanMove = false;
            CententPage.Resume($"现在时间是:{DateTime.Now}");//播放中间字
        }
        private void BuildCententPage()
        {
            if (CententPage==null)
            {
                CententPage = BuildSprite<CententPageSprite>();//创建中间字控件
                CententPage.IsActive = false;//设置中间字控件不是活动
                CententPage.MinCloseAnimaltor.ComplatedAnimaltor += () => PlayerSettings.Player.CanMove = true;//设置动画完成事件
            }
        }
    }
}
