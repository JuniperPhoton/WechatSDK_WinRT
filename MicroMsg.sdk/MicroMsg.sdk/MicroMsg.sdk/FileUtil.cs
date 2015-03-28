using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MicroMsg
{
    public class FileUtil
{
    // Methods
    public  async static void appendToFile(string fileName,string folderName, byte[] data)
    {
        try
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName,CreationCollisionOption.OpenIfExists);
            var file = await folder.GetFileAsync(fileName);
            if(file!=null)
            {
                using (var fileStream = await file.OpenStreamForWriteAsync())
                {
                    fileStream.Seek(0L, SeekOrigin.End);
                    fileStream.Write(data, 0, data.Length);
                }
            }
           
        }
        catch (Exception)
        {
        }
    }

    public async static Task<bool> createDir(string strPath)
    {
        if (!string.IsNullOrEmpty(strPath))
        {
            try
            {
                var folder=await ApplicationData.Current.LocalFolder.CreateFolderAsync(strPath);
                
                return true;
            }
            catch (Exception)
            {
            }
        }
        return false;
    }

    public async static void deleteDir(StorageFile isf, string path, bool bDeleteDir = true)
    {
        var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(path);
        var files = await folder.GetFilesAsync();
        foreach(var file in files)
        {
            await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
        if(bDeleteDir)
        {
            await folder.DeleteAsync();
        }

    }

    public async static Task<bool> deleteFile(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);

                if (file != null)
                {
                    await file.DeleteAsync();

                    return true;
                }
              
               
            }
            catch (Exception)
            {
            }
        }
        return false;
    }

    public async static Task<bool> dirExists(string path)
    {
        try
        {
            var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(path);
            return folder != null;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public static bool emptyDir(string strPath)
    {
        if (!string.IsNullOrEmpty(strPath))
        {
            try
            {
                //using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                //{
                //    if (!file.DirectoryExists(strPath))
                //    {
                //        return false;
                //    }
                //    foreach (string str in file.GetFileNames(strPath + "/*"))
                //    {
                //        file.DeleteFile(strPath + "/" + str);
                //    }
                //    foreach (string str2 in file.GetDirectoryNames(strPath + "/*"))
                //    {
                //        deleteDir(file, strPath + "/" + str2, true);
                //    }
                //}
                return true;
            }
            catch (Exception)
            {
            }
        }
        return false;
    }

    public static bool emptyFile(string strPath)
    {
        if (!string.IsNullOrEmpty(strPath))
        {
            try
            {
                //using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                //{
                //    if (!file.DirectoryExists(strPath))
                //    {
                //        return false;
                //    }
                //    deleteDir(file, strPath, false);
                //}
                return true;
            }
            catch (Exception)
            {
            }
        }
        return false;
    }

    public async static Task<bool> fileExists(string path)
    {
        try
        {
            var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(path);
            return folder != null;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public async static Task<long> fileLength(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                using(var fileStream=await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    return (long)fileStream.Size;
                }
            }
            catch (Exception)
            {
            }
        }
        return 0L;
    }

    public async static Task<long> getFileExistTime(string path)
    {
        try
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
            if(file!=null)
            {
                var date=file.DateCreated;
                return (long) DateTime.Now.Subtract(date.Date).TotalSeconds;
            }
        }
        catch (Exception)
        {
        }
        return 0L;
    }

    public async static Task<byte[]> readFromFile(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                if (file == null) return null;

                    using (var fileStream=await file.OpenReadAsync())
                    {
                        Stream stream = WindowsRuntimeStreamExtensions.AsStreamForRead(fileStream.GetInputStreamAt(0));

                        byte[] buffer = new byte[stream.Length];

                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            return ms.ToArray();
                        } 

                    }
            
            }
            catch (Exception)
            {
            }
        }
        return null;
    }

    public async static Task<byte[]> readFromFile(string fileName, int offset, int count)
    {
        try
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);

            using (var fileStream = await file.OpenStreamForReadAsync())
            {

                fileStream.Seek((long)offset, SeekOrigin.Begin);
                byte[] buffer = new byte[count];
                if (fileStream.Read(buffer, 0, buffer.Length) != count)
                {
                    return null;
                }
                return buffer;

            }
        }
        catch (Exception)
        {
        }
        return null;
    }

    public async static Task<bool> writeToFile(string fileName,string folderName, byte[] data, bool bCreateDir = false)
    {
        if (!string.IsNullOrEmpty(fileName) && (data != null))
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                
                using (var fileStream=await file.OpenStreamForWriteAsync())
                {
                    fileStream.Position = 0;
                    fileStream.Write(data, 0, data.Length);
                }
                return true;
            }
            catch (Exception)
            {
            }
        }
        return false;
    }
}

 

 


 

}
