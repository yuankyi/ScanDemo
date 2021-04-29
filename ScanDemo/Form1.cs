using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge;
using AForge.Controls;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;

using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace ScanDemo
{
    public partial class Form1 : Form
    {
        #region 全局变量定义
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        VideoSourcePlayer VspContainer = new VideoSourcePlayer();
        public int selectedDeviceIndex = 0;
        System.Timers.Timer TmScanner;

        #endregion

        public Form1()
        {
            InitializeComponent();

            InitializeView();
        }

        #region 事件
        /// <summary>
        /// 启动
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            PbxScanner.Image = null;

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            selectedDeviceIndex = 0;
            videoSource = new VideoCaptureDevice(videoDevices[selectedDeviceIndex].MonikerString);//连接摄像头

            videoSource.NewFrame += new NewFrameEventHandler(VspContainerClone);//捕获画面事件

            videoSource.VideoResolution = videoSource.VideoCapabilities[selectedDeviceIndex];

            VspContainer.VideoSource = videoSource;
            VspContainer.Start();

            StartVideoSource();
        }

        /// <summary>
        /// 停止
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            CloseVideoSource();
        }

        /// <summary>
        /// 保存
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScanner_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
                return;
            Bitmap bitmap = VspContainer.GetCurrentVideoFrame();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".jpg";

            bitmap.Save(Application.StartupPath + "\\" + fileName, ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        /// <summary>
        /// 同步事件
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VspContainerClone(object sender, NewFrameEventArgs eventArgs)
        {
            PbxScanner.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        /// <summary>
        /// Timer定时器
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmScanner_Tick(object sender, EventArgs e)
        {
            if (PbxScanner.Image != null)
            {
                TmScanner.Enabled = false;
                Bitmap img = (Bitmap)PbxScanner.Image.Clone();
                if (DecodeByZxing(img))
                ///if (DecodeByZbar(img))   
                {
                    CloseVideoSource();
                }
                else
                {
                    TmScanner.Enabled = true;
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// 20190515 by hanfre
        /// </summary>
        private void InitializeView()
        {
            BtnScanner.Enabled = false;
            BtnStop.Enabled = false;
        }

        /// <summary>
        /// 启动
        /// 20190515 by hanfre
        /// </summary>
        private void StartVideoSource()
        {
            TmScanner.Enabled = true;
            this.TmScanner.Start();
            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
            BtnScanner.Enabled = true;
        }
        /// <summary>
        /// 关闭
        /// 20190515 by hanfre
        /// </summary>
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
            {
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            }

            VspContainer.SignalToStop();
            //videoSourcePlayer1.Stop();
            //videoSourcePlayer1.Dispose();

            TmScanner.Enabled = false;
            BtnScanner.Enabled = false;
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
        }
        #endregion

        #region 方法/Zxing&Zbar
        /// <summary>
        /// 解码
        /// 20190515 by hanfre
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool DecodeByZxing(Bitmap b)
        {
            try
            {
                BarcodeReader reader = new BarcodeReader();
                reader.AutoRotate = true;
                Result result = reader.Decode(b);

                if (result != null)
                {
                    var xx = 123;
                }

                ShowCode(result.Text);
            }
            catch (Exception e)
            {
                ShowCode(DateTime.Now.ToString() + e.Message);
                return false;
            }

            return true;
        }

        private void ShowCode(string code)
        {
            this.Invoke(new Action(
                    () =>
                    {
                        TxtScannerCode.Text = code;
                    }));
        }

        private bool DecodeByZbar(Bitmap b)
        {
            DateTime now = DateTime.Now;

            Bitmap pImg = ZbarMakeGrayscale3(b);
            using (ZBar.ImageScanner scanner = new ZBar.ImageScanner())
            {
                scanner.SetConfiguration(ZBar.SymbolType.None, ZBar.Config.Enable, 0);
                scanner.SetConfiguration(ZBar.SymbolType.CODE39, ZBar.Config.Enable, 1);
                scanner.SetConfiguration(ZBar.SymbolType.CODE128, ZBar.Config.Enable, 1);

                List<ZBar.Symbol> symbols = new List<ZBar.Symbol>();
                symbols = scanner.Scan((System.Drawing.Image)pImg);

                if (symbols != null && symbols.Count > 0)
                {
                    string result = string.Empty;
                    symbols.ForEach(s => result += "条码内容:" + s.Data + " 条码质量:" + s.Quality + Environment.NewLine);
                    MessageBox.Show(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 处理图片灰度
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap ZbarMakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //秒表
            this.TmScanner = new System.Timers.Timer();  //实例化Timer类，设置间隔时间为10000毫秒；
            this.TmScanner.Elapsed += new System.Timers.ElapsedEventHandler(TmScanner_Tick);  //到达时间的时候执行事件；
            this.TmScanner.AutoReset = true;  //设置是执行一次（false）还是一直执行(true)；
            this.TmScanner.Interval = 3000;
        }

    }
}
