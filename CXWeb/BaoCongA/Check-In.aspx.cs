//using AForge.Video;
//using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ZXing;


namespace CXWeb.BaoCong
{
    public partial class Check_In : System.Web.UI.Page
    {
        //FilterInfoCollection filterInfoCollection;
        //VideoCaptureDevice videoCaptureDevice;

        protected void Page_Load(object sender, EventArgs e)
        {
            //filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //foreach (FilterInfo Device in filterInfoCollection)
            //    ddlSheet.Items.Add(Device.Name);
            //if (ddlSheet.Items.Count > 0)
            //{
            //    ddlSheet.SelectedIndex = 0;
            //    videoCaptureDevice = new VideoCaptureDevice();
            //}
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {


            //if (btnLoad.Text == "Bắt đầu")
            //{
            //    videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            //    videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            //    videoCaptureDevice.Start();
            //    cancellationToken = new CancellationTokenSource();
            //    var sourcetoken = cancellationToken.Token;
            //    onStartScan(sourcetoken);
            //    btnLoad.Text = "Dừng lại!";
            //}
            //else
            //{
            //    btnLoad.Text = "Bắt đầu";
            //    cancellationToken.Cancel();
            //    if (videoCaptureDevice != null)
            //        if (videoCaptureDevice.IsRunning == true)
            //            videoCaptureDevice.Stop();
            //}

        }

        //private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //   // pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        //}

        public void onStartScan(CancellationToken sourcetoken)
        {
            //Task.Factory.StartNew(new Action(() =>
            //{
            //    while (true)
            //    {
            //        if (sourcetoken.IsCancellationRequested)
            //        {
            //            return;
            //        }
            //        Thread.Sleep(50);
            //        BarcodeReader Reader = new BarcodeReader();
            //        pictureBox1.BeginInvoke(new Action(() =>
            //        {
            //            if (pictureBox1.Image != null)
            //            {
            //                try
            //                {
            //                    var results = Reader.DecodeMultiple((Bitmap)pictureBox1.Image);
            //                    if (results != null)
            //                    {
            //                        foreach (Result result in results)
            //                        {
            //                            lbl_result.Text = result.ToString() + $"- Type: { result.BarcodeFormat.ToString()}";

            //                            SystemSounds.Beep.Play();
            //                            if (result.ResultPoints.Length > 0)
            //                            {
            //                                var offsetX = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
            //                                   ? (pictureBox1.Width - pictureBox1.Image.Width) / 2 :
            //                                   0;
            //                                var offsetY = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
            //                                   ? (pictureBox1.Height - pictureBox1.Image.Height) / 2 :
            //                                   0;
            //                                var rect = new Rectangle((int)result.ResultPoints[0].X + offsetX, (int)result.ResultPoints[0].Y + offsetY, 1, 1);
            //                                foreach (var point in result.ResultPoints)
            //                                {
            //                                    if (point.X + offsetX < rect.Left)
            //                                        rect = new Rectangle((int)point.X + offsetX, rect.Y, rect.Width + rect.X - (int)point.X - offsetX, rect.Height);
            //                                    if (point.X + offsetX > rect.Right)
            //                                        rect = new Rectangle(rect.X, rect.Y, rect.Width + (int)point.X - (rect.X - offsetX), rect.Height);
            //                                    if (point.Y + offsetY < rect.Top)
            //                                        rect = new Rectangle(rect.X, (int)point.Y + offsetY, rect.Width, rect.Height + rect.Y - (int)point.Y - offsetY);
            //                                    if (point.Y + offsetY > rect.Bottom)
            //                                        rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + (int)point.Y - (rect.Y - offsetY));
            //                                }
            //                                using (var g = pictureBox1.CreateGraphics())
            //                                {
            //                                    using (Pen pen = new Pen(Color.Green, 5))
            //                                    {
            //                                        g.DrawRectangle(pen, rect);

            //                                        pen.Color = Color.Yellow;
            //                                        pen.DashPattern = new float[] { 5, 5 };
            //                                        g.DrawRectangle(pen, rect);
            //                                    }
            //                                    g.DrawString(result.ToString(), new Font("Tahoma", 16f), Brushes.Blue, new Point(rect.X - 60, rect.Y - 50));
            //                                }
            //                            }


            //                        }

            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                }
            //            }

            //        }));

            //    }
            //}), sourcetoken);
        }

    }
}