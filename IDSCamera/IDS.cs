using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDSCamera
{
    interface IAOI
    {
        OpenCvSharp.Mat Binarization(Bitmap bitmap);
    }

    class IDS: IAOI
    {
        private uEye.Camera m_Camera;
        private bool m_bLive = false;
        public IntPtr m_displayHandle = IntPtr.Zero;
        private const int m_cnNumberOfSeqBuffers = 3;
        
        private uEye.Defines.Status AllocImageMems()
        {
            uEye.Defines.Status statusRet = uEye.Defines.Status.SUCCESS;

            for (int i = 0; i < m_cnNumberOfSeqBuffers; i++)
            {
                statusRet = m_Camera.Memory.Allocate();

                if (statusRet != uEye.Defines.Status.SUCCESS)
                {
                    FreeImageMems();
                }
            }

            return statusRet;
        }
        private uEye.Defines.Status FreeImageMems()
        {
            int[] idList;
            uEye.Defines.Status statusRet = m_Camera.Memory.GetList(out idList);

            if (uEye.Defines.Status.SUCCESS == statusRet)
            {
                foreach (int nMemID in idList)
                {
                    do
                    {
                        statusRet = m_Camera.Memory.Free(nMemID);

                        if (uEye.Defines.Status.SEQ_BUFFER_IS_LOCKED == statusRet)
                        {
                            Thread.Sleep(1);
                            continue;
                        }

                        break;
                    }
                    while (true);
                }
            }

            return statusRet;
        }
        private uEye.Defines.Status InitSequence()
        {
            int[] idList;
            uEye.Defines.Status statusRet = m_Camera.Memory.GetList(out idList);

            if (uEye.Defines.Status.SUCCESS == statusRet)
            {
                statusRet = m_Camera.Memory.Sequence.Add(idList);

                if (uEye.Defines.Status.SUCCESS != statusRet)
                {
                    ClearSequence();
                }
            }

            return statusRet;
        }
        private uEye.Defines.Status ClearSequence()
        {
            return m_Camera.Memory.Sequence.Clear();
        }
        private void onFrameEvent(object sender, EventArgs e)
        {
            uEye.Camera Camera = sender as uEye.Camera;

            Int32 s32MemID;
            uEye.Defines.Status statusRet = Camera.Memory.GetLast(out s32MemID);

            if ((uEye.Defines.Status.SUCCESS == statusRet) && (0 < s32MemID))
            {
                if (uEye.Defines.Status.SUCCESS == Camera.Memory.Lock(s32MemID))
                {
                    Camera.Display.Render(s32MemID, m_displayHandle, uEye.Defines.DisplayRenderMode.FitToWindow);
                    Camera.Memory.Unlock(s32MemID);
                }
            }
        }

        public void InitCamera(int CameraID)
        {
            m_Camera = new uEye.Camera();
            uEye.Defines.Status statusRet = 0;

            // Open Camera
            statusRet = m_Camera.Init(CameraID);
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Camera initializing failed");
                Environment.Exit(-1);
            }

            //if (File.Exists(camera_parameter_file))
            //{
            //    //Load Camera Parameter File
            //    statusRet = m_Camera.Parameter.Load(camera_parameter_file);
            //    if (statusRet != uEye.Defines.Status.Success)
            //    {
            //        MessageBox.Show("Loading parameter failed: " + statusRet);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please confirm whether the camera file exists!");
            //}

            // Allocate Memory
            statusRet = AllocImageMems();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Allocate Memory failed");
                Environment.Exit(-1);
            }

            statusRet = InitSequence();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Add to sequence failed");
                Environment.Exit(-1);
            }

            // Start Live Video
            statusRet = m_Camera.Acquisition.Capture();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Start Live Video failed");
            }
            else
            {
                m_bLive = true;
            }

            // Connect Event
            m_Camera.EventFrame += onFrameEvent;
        }

        public void ContinuousAcquisition()
        {
            // Open Camera and Start Live Video
            if (m_Camera.Acquisition.Capture() == uEye.Defines.Status.Success)
            {
                m_bLive = true;
            }
        }

        public void StopContinuousAcquisition()
        {
            // Stop Live Video
            if (m_Camera.Acquisition.Stop() == uEye.Defines.Status.Success)
            {
                m_bLive = false;
            }
        }

        public void FreezeImage()
        {
            if (m_Camera.Acquisition.Freeze() == uEye.Defines.Status.Success)
            {
                m_bLive = false;
            }
        }

        public void LoadParameter(string camera_parameter_file)
        {
            uEye.Defines.Status statusRet = 0;

            m_Camera.Acquisition.Stop();

            ClearSequence();
            FreeImageMems();

            statusRet = m_Camera.Parameter.Load(camera_parameter_file);
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Loading parameter failed: " + statusRet);
            }

            // Allocate Memory
            statusRet = AllocImageMems();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Allocate Memory failed");
                Environment.Exit(-1);
            }

            statusRet = InitSequence();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Add to sequence failed");
                Environment.Exit(-1);
            }

            if (m_bLive == true)
            {
                m_Camera.Acquisition.Capture();
            }

        }

        public void Exit()
        {
            m_Camera.Exit();
        }

        public void CameraParameter(int value)
        {
            #region Gamma
            //m_Camera.Gamma.Software.Set(value);
            //int gamma_max;
            //int gamma_min;
            //int gamma_inc;
            //m_Camera.Gamma.Software.GetRange(out gamma_min, out gamma_max, out gamma_inc);
            //Console.WriteLine(gamma_min);
            //Console.WriteLine(gamma_max);
            #endregion
            #region Framerate
            //m_Camera.Timing.Framerate.Set(value);
            //double framerate_max;
            //double framerate_min;
            //double framerate_inc;
            //m_Camera.Timing.Framerate.GetFrameRateRange(out framerate_min, out framerate_max, out framerate_inc);
            //Console.WriteLine(framerate_min);
            //Console.WriteLine(framerate_max);
            #endregion
            #region Exposure.
            //m_Camera.Timing.Exposure.Set(value);
            //double exposure_max;
            //double exposure_min;
            //double exposure_inc;
            //m_Camera.Timing.Exposure.GetRange(out exposure_max, out exposure_min, out exposure_inc);
            //Console.WriteLine(exposure_min);
            //Console.WriteLine(exposure_max);
            #endregion
            //m_Camera.Gain.Hardware.Factor.SetMaster(value);
        }

        public void SaveImage(string image_file, string dip)
        {
            // Save Image Method One
            //m_Camera.Image.Save(@"E:\1.bmp");

            // Save Image Method Two
            Bitmap bitmap = new Bitmap(2456, 2054, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            if (m_Camera.Memory.ToBitmap(m_cnNumberOfSeqBuffers - 1, out bitmap) == uEye.Defines.Status.Success)
            {
                switch (dip)
                {
                    case "None":
                        {
                            OpenCvSharp.Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
                            OpenCvSharp.Cv2.ImWrite(image_file, src);
                            break;
                        }
                    case "Binarization":
                        {
                            OpenCvSharp.Mat Binary = Binarization(bitmap);
                            OpenCvSharp.Cv2.ImWrite(image_file, Binary);
                            break;
                        }

                }
                
            }
        }

        public OpenCvSharp.Mat Binarization(Bitmap bitmap)
        {
            OpenCvSharp.Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
            OpenCvSharp.Mat Binary = src.Threshold(100, 255, OpenCvSharp.ThresholdTypes.Binary);
            return Binary;
        }

        






    }







}
