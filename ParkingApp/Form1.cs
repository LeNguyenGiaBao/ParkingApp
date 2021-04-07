using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class Form1 : Form
    {

        private Image[] list_image = new Image[1000];
        int list_image_count = 0;

        int _WIDTH_SCREEN = Screen.PrimaryScreen.Bounds.Width;
        int _HEIGHT_SCREEN = Screen.PrimaryScreen.Bounds.Height;

        string ip1 = @"http://192.168.43.42:8080/videofeed";///video/mjpegfeed?1920x1080";
        string ip2 = @"http://192.168.43.1:8080/videofeed";///mjpegfeed?1920x1080";
        string ip3 = @"http://192.168.1.7:4747/video/mjpegfeed?1920x1080";
        string ip4 = @"http://192.168.1.2:4747/video/mjpegfeed?1920x1080";



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

        #region Cam1
        void LoadCam1()
        {
            MJPEGStream stream1 = new MJPEGStream(ip1);
            stream1.NewFrame += Stream1_NewFrame;
            stream1.Start();
        }
        private void Stream1_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam1.Image = bmp;
        }
        #endregion

        #region Cam2
        void LoadCam2()
        {
            MJPEGStream stream2 = new MJPEGStream(ip2);
            stream2.NewFrame += Stream2_NewFrame;
            stream2.Start();
        }

        private void Stream2_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam2.Image = bmp;
        }
        #endregion

        #region Cam3
        void LoadCam3()
        {
            MJPEGStream stream3 = new MJPEGStream(ip3);
            stream3.NewFrame += Stream3_NewFrame;
            stream3.Start();
        }

        private void Stream3_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam3.Image = bmp;
        }
        #endregion

        #region Cam4
        void LoadCam4()
        {
            MJPEGStream stream4 = new MJPEGStream(ip4);
            stream4.NewFrame += Stream4_NewFrame;
            stream4.Start();
        }

        private void Stream4_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            cam4.Image = bmp;
        }
        #endregion

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

            ThreadStart ts4 = new ThreadStart(LoadCam4);
            Thread t4 = new Thread(ts4);
            t4.IsBackground = true;
            t4.Start();
        }

        public void save_image_to_disk(string folder_path)
        {
            if (list_image_count == 0 || list_image.Length == 0)
            {
                return;
            }
            for (int i = 0; i < list_image_count; i++)
            {
                try
                {
                    System.Drawing.Image img = (Image)list_image[i];
                    //MessageBox.Show(img.ToString());
                    var img_save = new Bitmap(img);
                    img_save.Save(folder_path + i.ToString() + ".jpg", ImageFormat.Jpeg);
                }
                catch { }
            }
        }

        public void save_temp_image_by_cam(object cam)
        {
            try
            {
                this.list_image[list_image_count] = ((PictureBox)cam).Image;
                list_image_count++;
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            save_temp_image_by_cam(cam1);
            save_temp_image_by_cam(cam2);
            //MessageBox.Show(cam1.Image.Size.ToString());
            //byte[] ar = imageToByteArray(cam1.Image);
            //object img = cam1.Image;

            //this.list_image[list_image_count] = cam1.Image;
            //list_image_count++;
            //MessageBox.Show(dem.ToString());
            //Image img = byteArrayToImage(ar);
            //img.Save(@"E:\2.png", ImageFormat.Png);
        }

        //public Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}


        //public byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    return ms.ToArray();
        //}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            save_image_to_disk(@"E:\Python\Hinh\");
        }
    }
}
