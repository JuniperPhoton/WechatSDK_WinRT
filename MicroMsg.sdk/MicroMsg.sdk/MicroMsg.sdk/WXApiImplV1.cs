using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;

namespace MicroMsg
{
  internal class WXApiImplV1 : IWXAPI
{
    // Fields
    private string mAppID;
    private const string TEMP_FILE_NAME = "wp";

    // Methods
    internal WXApiImplV1(string appID)
    {
        this.mAppID = appID;
    }

    private static string getCheckContent()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        DateTime time2 = new DateTime(0x7b2, 1, 1);
        return Convert.ToString(time.Subtract(time2).TotalMilliseconds);
    }

    internal static string getCheckSummary(string content, string sdkVersion, string appID)
    {
        return MD5Util.GetHashString(trimToEmpty(content) + trimToEmpty(sdkVersion) + trimToEmpty(appID));
    }

    private static string getTransactionId()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        DateTime time2 = new DateTime(0x7b2, 1, 1);
        return Convert.ToString(time.Subtract(time2).TotalMilliseconds);
    }

    public async void OpenWXApp()
    {
        await Launcher.LaunchUriAsync(new Uri("wechat:LaunchWechat?target=MainPage"));
    }

    private async void sendOut(string filePath, string targetAppID)
    {
        StorageFolder local = await ApplicationData.Current.LocalFolder.GetFolderAsync("wechatsdk");
        StorageFile bqfile = await local.GetFileAsync(filePath);
        if (bqfile != null)
        {
            bool flag1 = await Launcher.LaunchFileAsync(bqfile);
        }
    }

    public async Task<string> SendReq(BaseReq request, string targetAppID = "wechat")
    {
        int p = 0;
        try
        {
            TransactData data;
            if (request == null)
            {
                throw new WXException(1, "Req can't be null.");
            }
            if (string.IsNullOrEmpty(targetAppID))
            {
                throw new WXException(1, "targetAppID can't be empty.");
            }
            p = 1;
            data = new TransactData();
            data.Req=request;
                data.AppID = this.mAppID;
                data.ConmandID = request.Type();
                data.SdkVersion = "1.5";
                data.CheckContent = getCheckContent();
                data.CheckSummary = getCheckSummary(data.CheckContent, data.SdkVersion, data.AppID);
                p = 2;
            if (string.IsNullOrEmpty(request.Transaction))
            {
                request.Transaction = getTransactionId();
            }

            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("wechatsdk", CreationCollisionOption.OpenIfExists);
            p = 3;
            string fileName = "wp." + targetAppID;
            //if (await FileUtil.fileExists(fileName))
            //{
            //   await FileUtil.deleteFile(fileName);
            //}
            if (data.ValidateData(false))
            {
                try
                {
                    p = 4;
                    TransactData.WriteToFile(data, fileName,"wechatsdk");
                    this.sendOut(fileName, targetAppID);
                    return "true";
                }
                catch (Exception exception)
                {
                    throw new WXException(0, exception.Message);
                }
            }
            return "false";
        }
        catch(Exception e)
        {
            return e.Message + "p:" + p.ToString();
        }
    }

    public async Task<bool> SendResp(BaseResp response, string targetAppID)
    {
        TransactData data = null;
        if (response == null)
        {
            throw new WXException(1, "Resp can't be null.");
        }
        if (string.IsNullOrEmpty(targetAppID))
        {
            throw new WXException(1, "targetAppID can't be empty.");
        }
        data = new TransactData {
            Resp = response,
            AppID = this.mAppID,
            ConmandID = response.Type(),
            SdkVersion = "1.5",
            CheckContent = getCheckContent(),
            CheckSummary = getCheckSummary(data.CheckContent, data.SdkVersion, data.AppID)
        };
        if (string.IsNullOrEmpty(response.Transaction))
        {
            response.Transaction = getTransactionId();
        }

        var folder = await ApplicationData.Current.LocalFolder.CreateFileAsync("wechatsdk");

        string fileName = "wp."+targetAppID;
        //if (await FileUtil.fileExists(fileName))
        //{
        //   await FileUtil.deleteFile(fileName);
        //}
        if (data.ValidateData(false))
        {
            try
            {
                TransactData.WriteToFile(data, fileName,"wechatsdk");
                this.sendOut(fileName, targetAppID);
                return true;
            }
            catch (Exception exception)
            {
                throw new WXException(0, exception.Message);
            }
        }
        return false;
    }

    private static string trimToEmpty(string s)
    {
        if (s != null)
        {
            return s.Trim();
        }
        return "";
    }

    //// Nested Types
    //[CompilerGenerated]
    //private struct <OpenWXApp>d__0 : IAsyncStateMachine
    //{
    //    // Fields
    //    public int <>1__state;
    //    public WXApiImplV1 <>4__this;
    //    public AsyncVoidMethodBuilder <>t__builder;
    //    private object <>t__stack;
    //    private TaskAwaiter<bool> <>u__$awaiter1;

    //    // Methods
    //    private void MoveNext()
    //    {
    //        try
    //        {
    //            TaskAwaiter<bool> awaiter;
    //            bool flag = true;
    //            if (this.<>1__state != 0)
    //            {
    //                awaiter = Launcher.LaunchUriAsync(new Uri("wechat:LaunchWechat?target=MainPage")).GetAwaiter<bool>();
    //                if (!awaiter.get_IsCompleted())
    //                {
    //                    this.<>1__state = 0;
    //                    this.<>u__$awaiter1 = awaiter;
    //                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WXApiImplV1.<OpenWXApp>d__0>(ref awaiter, ref this);
    //                    flag = false;
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                awaiter = this.<>u__$awaiter1;
    //                this.<>u__$awaiter1 = new TaskAwaiter<bool>();
    //                this.<>1__state = -1;
    //            }
    //            awaiter.GetResult();
    //            awaiter = new TaskAwaiter<bool>();
    //        }
    //        catch (Exception exception)
    //        {
    //            this.<>1__state = -2;
    //            this.<>t__builder.SetException(exception);
    //            return;
    //        }
    //        this.<>1__state = -2;
    //        this.<>t__builder.SetResult();
    //    }

    //    [DebuggerHidden]
    //    private void SetStateMachine(IAsyncStateMachine param0)
    //    {
    //        this.<>t__builder.SetStateMachine(param0);
    //    }
    //}

    //[CompilerGenerated]
    //private struct <sendOut>d__3 : IAsyncStateMachine
    //{
    //    // Fields
    //    public int <>1__state;
    //    public WXApiImplV1 <>4__this;
    //    public AsyncVoidMethodBuilder <>t__builder;
    //    private object <>t__stack;
    //    private TaskAwaiter<StorageFile> <>u__$awaiter7;
    //    private TaskAwaiter<bool> <>u__$awaiter8;
    //    public StorageFile <bqfile>5__5;
    //    public StorageFolder <local>5__4;
    //    public bool <success>5__6;
    //    public string filePath;

    //    // Methods
    //    private void MoveNext()
    //    {
    //        try
    //        {
    //            TaskAwaiter<StorageFile> awaiter;
    //            bool flag = true;
    //            switch (this.<>1__state)
    //            {
    //                case 0:
    //                    break;

    //                case 1:
    //                    goto Label_00E5;

    //                default:
    //                    this.<local>5__4 = ApplicationData.get_Current().get_LocalFolder();
    //                    awaiter = this.<local>5__4.GetFileAsync(this.filePath).GetAwaiter<StorageFile>();
    //                    if (awaiter.get_IsCompleted())
    //                    {
    //                        goto Label_0088;
    //                    }
    //                    this.<>1__state = 0;
    //                    this.<>u__$awaiter7 = awaiter;
    //                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<StorageFile>, WXApiImplV1.<sendOut>d__3>(ref awaiter, ref this);
    //                    flag = false;
    //                    return;
    //            }
    //            awaiter = this.<>u__$awaiter7;
    //            this.<>u__$awaiter7 = new TaskAwaiter<StorageFile>();
    //            this.<>1__state = -1;
    //        Label_0088:
    //            StorageFile introduced9 = awaiter.GetResult();
    //            awaiter = new TaskAwaiter<StorageFile>();
    //            StorageFile file = introduced9;
    //            this.<bqfile>5__5 = file;
    //            if (this.<bqfile>5__5 == null)
    //            {
    //                goto Label_013D;
    //            }
    //            TaskAwaiter<bool> awaiter3 = Launcher.LaunchFileAsync(this.<bqfile>5__5).GetAwaiter<bool>();
    //            if (awaiter3.get_IsCompleted())
    //            {
    //                goto Label_0104;
    //            }
    //            this.<>1__state = 1;
    //            this.<>u__$awaiter8 = awaiter3;
    //            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WXApiImplV1.<sendOut>d__3>(ref awaiter3, ref this);
    //            flag = false;
    //            return;
    //        Label_00E5:
    //            awaiter3 = this.<>u__$awaiter8;
    //            this.<>u__$awaiter8 = new TaskAwaiter<bool>();
    //            this.<>1__state = -1;
    //        Label_0104:
    //            bool introduced10 = awaiter3.GetResult();
    //            awaiter3 = new TaskAwaiter<bool>();
    //            bool flag2 = introduced10;
    //            this.<success>5__6 = flag2;
    //            bool flag1 = this.<success>5__6;
    //        }
    //        catch (Exception exception)
    //        {
    //            this.<>1__state = -2;
    //            this.<>t__builder.SetException(exception);
    //            return;
    //        }
    //    Label_013D:
    //        this.<>1__state = -2;
    //        this.<>t__builder.SetResult();
    //    }

    //    [DebuggerHidden]
    //    private void SetStateMachine(IAsyncStateMachine param0)
    //    {
    //        this.<>t__builder.SetStateMachine(param0);
    //    }
    //}
}


}
