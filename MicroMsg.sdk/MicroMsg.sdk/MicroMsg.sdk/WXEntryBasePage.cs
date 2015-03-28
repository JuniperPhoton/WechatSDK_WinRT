using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MicroMsg
{
    public class WXEntryBasePage : Page
{
    // Fields
    private bool bIsHandled;

    // Methods
    public virtual void On_GetMessageFromWX_Request(GetMessageFromWX.Req request)
    {
    }

    public virtual void On_SendAuth_Response(SendAuth.Resp response)
    {
    }

    public virtual void On_SendMessageToWX_Response(SendMessageToWX.Resp response)
    {
    }

    public virtual void On_SendPay_Response(SendPay.Resp response)
    {
    }

    public virtual void On_ShowMessageFromWX_Request(ShowMessageFromWX.Req request)
    {
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (!this.bIsHandled)
        {
            e.Uri.ToString();
            string str = null;
            //if (e.Parameter.Keys.Contains("fileToken"))
            //{
            //    str = base.get_NavigationContext().get_QueryString()["fileToken"];
            //}
            if (!string.IsNullOrEmpty(str))
            {
               // this.parseData(str);
            }
            this.bIsHandled = true;
        }
    }

    private async void parseData(string fileToken)
    {
        try
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            if (!await FileUtil.dirExists("wechatsdk"))
            {
                await FileUtil.createDir("wechatsdk");
            }
            //await SharedStorageAccessManager.CopySharedFileAsync(localFolder, @"wechat_sdk\wp.wechat", 1, fileToken);
            if (await FileUtil.fileExists(@"wechatsdk\wp.wechat"))
            {
                TransactData data = await TransactData.ReadFromFile(@"wechatsdk\wp.wechat");
                if (!data.ValidateData(true))
                {
                    //MessageBox.Show("数据验证失败");
                }
                else if (!data.CheckSupported())
                {
                    //MessageBox.Show("当前版本不支持该请求");
                }
                else if (data.Req != null)
                {
                    if (data.Req.Type() == 3)
                    {
                        this.On_GetMessageFromWX_Request(data.Req as GetMessageFromWX.Req);
                    }
                    else if (data.Req.Type() == 4)
                    {
                        this.On_ShowMessageFromWX_Request(data.Req as ShowMessageFromWX.Req);
                    }
                }
                else if (data.Resp != null)
                {
                    if (data.Resp.Type() == 2)
                    {
                        this.On_SendMessageToWX_Response(data.Resp as SendMessageToWX.Resp);
                    }
                    else if (data.Resp.Type() == 1)
                    {
                        this.On_SendAuth_Response(data.Resp as SendAuth.Resp);
                    }
                    else if (data.Resp.Type() == 5)
                    {
                        this.On_SendPay_Response(data.Resp as SendPay.Resp);
                    }
                }
            }
        }
        catch (Exception exception)
        {
            //MessageBox.Show(exception.Message);
        }
    }

  
    }
}
