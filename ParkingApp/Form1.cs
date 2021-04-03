using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class Form1 : Form
    {

        int _WIDTH_SCREEN = Screen.PrimaryScreen.Bounds.Width;
        int _HEIGHT_SCREEN = Screen.PrimaryScreen.Bounds.Height;

        string ip1 = @"http://192.168.1.2:4747/video/mjpegfeed?1920x1080";
        string ip2 = @"http://192.168.1.5:4747/video/mjpegfeed?1920x1080";
        string ip3 = @"http://192.168.1.7:4747/video/mjpegfeed?1920x1080";
        //string ip4 = @"http://192.168.1.2:4747/video/mjpegfeed?1920x1080";



        //MJPEGStream stream;


        public Form1()
        {
            InitializeComponent();
            setup();
            start_camera();
        }

        public void setup()
        {
            // form full screen 
            this.MaximumSize = new Size(_WIDTH_SCREEN, _HEIGHT_SCREEN);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.pnNameApp.Location = new Point(0, 0);
            this.pnNameApp.Size = new Size(_WIDTH_SCREEN, 70);  // 70 is the size of this picture
            this.pnNameApp.Dock = DockStyle.Top;
            
            this.lbName.Location = new Point((_WIDTH_SCREEN - this.lbName.Width) / 2, (this.pnNameApp.Height - this.lbName.Height) / 2);

            int slit = 20;

            int _picture_size = Convert.ToInt32((_WIDTH_SCREEN - 5 * slit) / 4);

            this.cam1.Size = new Size(_picture_size, _picture_size);
            this.cam2.Size = new Size(_picture_size, _picture_size);
            this.cam3.Size = new Size(_picture_size, _picture_size);
            this.cam4.Size = new Size(_picture_size, _picture_size);
            this.pic1.Size = new Size(_picture_size, _picture_size);
            this.pic2.Size = new Size(_picture_size, _picture_size);
            this.pic3.Size = new Size(_picture_size, _picture_size);
            this.pic4.Size = new Size(_picture_size, _picture_size);

            this.cam1.Location = new Point(slit, slit);
            this.cam2.Location = new Point(_picture_size + 2 * slit, slit);
            this.cam3.Location = new Point(2 * _picture_size + 3 * slit, slit);
            this.cam4.Location = new Point(3 * _picture_size + 4 * slit, slit);

            this.pic1.Location = new Point(slit, _picture_size + 2 *slit);
            this.pic2.Location = new Point(_picture_size + 2 * slit, _picture_size + 2 * slit);
            this.pic3.Location = new Point(2 * _picture_size + 3 * slit, _picture_size + 2 * slit);
            this.pic4.Location = new Point(3 * _picture_size + 4 * slit, _picture_size + 2 * slit);
        }

        void LoadCam1()
        {
            MJPEGStream stream1 = new MJPEGStream(ip1);
            stream1.NewFrame += Stream1_NewFrame;
            stream1.Start();
        }

        void LoadCam2()
        {
            MJPEGStream stream2 = new MJPEGStream(ip2);
            stream2.NewFrame += Stream2_NewFrame;
            stream2.Start();
        }

        void LoadCam3()
        {
            MJPEGStream stream3 = new MJPEGStream(ip3);
            stream3.NewFrame += Stream3_NewFrame;
            stream3.Start();
        }

        private void Stream1_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam1.Image = bmp;
        }

        private void Stream2_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam2.Image = bmp;
        }

        private void Stream3_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam3.Image = bmp;
        }

        private void start_camera()
        {
            ThreadStart ts1 = new ThreadStart(LoadCam1);
            Thread t1 = new Thread(ts1);
            t1.IsBackground = true;
            t1.Start();

            ThreadStart ts2 = new ThreadStart(LoadCam2);
            Thread t2 = new Thread(ts2);
            t2.IsBackground = true;
            t2.Start();

            ThreadStart ts3 = new ThreadStart(LoadCam3);
            Thread t3 = new Thread(ts3);
            t3.IsBackground = true;
            t3.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(LoadCam1);
            Thread t = new Thread(ts);
            t.IsBackground = true;
            t.Start();
        }
    }
}
