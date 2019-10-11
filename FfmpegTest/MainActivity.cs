using Android.App;
using Android.OS;
using Android.Widget;
using System.Runtime.InteropServices;
using System;
using Android.Util;
using System.IO;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using Android.Runtime;

namespace FfmpegTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        string H264_savePath;
        string AAC_savePath;
        string MP4_savepath;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, 10);
            }
            Button btn_transform = FindViewById<Button>(Resource.Id.button1);
            H264_savePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,  "20191010092459.h264");
            AAC_savePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "20191010092459.mp4a");
            MP4_savepath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "success.aac");          
           
            btn_transform.Click += Btn_transform_Click;
        }

        private void Btn_transform_Click(object sender, EventArgs e)
        {
            if (!File.Exists(MP4_savepath))
            {
                FileStream fileStream = new FileStream(MP4_savepath, FileMode.Create);
                fileStream.Close();
            }
            if (!File.Exists(AAC_savePath))
            {
                Console.WriteLine("file not exit！");
                return;
            }
            try
            {
                FFmpeg ffmpeg = new FFmpeg();
                // string command = "ffmpeg###-y###–i###" + H264_savePath + "###–i###" + AAC_savePath + "###–vcodec###copy###–acodec###copy###" + MP4_savepath + "";
                string command = "ffmpeg###-i###"+ AAC_savePath + "###-vn###-y###-acodec###copy###"+ MP4_savepath + "";
              //  string cmd = " ffmpeg###-i###/storage/emulated/0/20191010092459.mp4a###-vn###-y###-acodec###copy###/storage/emulated/0/success.aac";
                var result = ffmpeg.ffmpegRunCommad(command);
                Console.WriteLine("result:" + result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("fail");
            }
           
           
        }
    }
    public class FFmpeg
    {
        public int ffmpegRunCommad(string command)
        {
           // nativetest(IntPtr.Zero,11, "helloworld");
            if (string.IsNullOrEmpty(command))
            {
                return -1;
            }
            string[] comm = command.Split("###");
            for(int i = 0; i < comm.Length; i++)
            {
                Log.Debug("ffmpeg-jni", comm[i]);
            }
           
             return nativerun(IntPtr.Zero, comm.Length, comm);
           
        }
     
        [DllImport("libffmpegjni.so", EntryPoint = "Java_org_mqstack_ffmpegjni_FFmpegJni_run")]
        private static extern int nativerun(IntPtr intPtr, int commLength, string[] command);
        [DllImport("libffmpegjni.so", EntryPoint = "Java_org_mqstack_ffmpegjni_FFmpegJni_test")]
        private static extern int nativetest(IntPtr intPtr,int commLength, string command);
    }
}