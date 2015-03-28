using MicroMsg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace App3
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async  void pickClick(object sender,RoutedEventArgs e)
        {
            
        }

        private async void wechatClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int scene = SendMessageToWX.Req.WXSceneChooseByUser; //发给微信朋友

                WXImageMessage message = new WXImageMessage();

                var file =await KnownFolders.SavedPictures.GetFileAsync("Bing_1_12_2015 5_50_44 PM.jpg");
                using(var filestream=await file.OpenStreamForReadAsync())
                {
                    byte[] buffer = new byte[filestream.Length];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        filestream.Position = 0;
                        filestream.Read(buffer, 0, (int)filestream.Length);

                         ms.Write(buffer, 0, (int)filestream.Length);

                         var data=ms.ToArray();

                         message.ImageData = data;
                         message.Title = "图片";
                         message.Description = "这是一个图片";

                         SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                         IWXAPI api = WXAPIFactory.CreateWXAPI("wxafd875d032f05470");
                         var isok = await api.SendReq(req);
                    }

                    
                    
                }
                //RenderTargetBitmap bitmap = new RenderTargetBitmap();
                //await bitmap.RenderAsync(renderGrid);
                //var pixel = await bitmap.GetPixelsAsync();
                
                //message.ThumbData = pixel.ToArray();

                
                //new MessageDialog(isok).ShowAsync();
            }
            catch (Exception ex)
            {
                //new MessageDialog(ex.Message).ShowAsync();
            }

        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }
    }
}
