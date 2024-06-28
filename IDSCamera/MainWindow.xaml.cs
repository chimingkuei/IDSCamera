using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;

namespace IDSCamera
{
    public class Parameter
    {
        public string Camera_Parameter_File_val { get; set; }
        public string Save_Image_Path_val { get; set; }
        public int Gamma_val { get; set; }
        public int Gain_val { get; set; }
    }
    
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Function
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("Do you want to close the window?", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ChangeIcon(string path, Image name, string tip)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(path);
            bitmapImage.EndInit();
            name.Source = bitmapImage;
            name.ToolTip = tip;
        }
        #endregion

        #region Parameter and Init
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UI.m_displayHandle = Display_Windows.Handle;
            //Load Config
            List<Parameter> Parameter_info = Config.Load();
            Camera_Parameter_File.Text = Parameter_info[0].Camera_Parameter_File_val;
            Save_Image_Path.Text = Parameter_info[0].Save_Image_Path_val;
            Gamma.Value = Parameter_info[0].Gamma_val;
            Gain.Value = Parameter_info[0].Gain_val;
        }
        BaseLogRecord Logger = new BaseLogRecord();
        BaseConfig<Parameter> Config = new BaseConfig<Parameter>();
        private IDS UI = new IDS();
        bool Freeze_state = true;
        bool Continuous_Acquisition_state = false;
        #endregion

        #region Main Screen
        private void Main_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case nameof(Continuous_Acquisition):
                    {
                        if (!Continuous_Acquisition_state)
                        {
                            UI.InitCamera(0);
                            Logger.WriteLog("Continuous Acquisition!", 1, richTextBoxGeneral);
                            Continuous_Acquisition_state = true;
                            ChangeIcon(AppDomain.CurrentDomain.BaseDirectory + @"Icon\Stop.png", Continuous_Acquisition_Icon, "停止連續取像");
                        }
                        else if (Continuous_Acquisition_state)
                        {
                            UI.StopContinuousAcquisition();
                            UI.Exit();
                            Display_Windows.Image = null;
                            Logger.WriteLog("Stop Continuous Acquisition!", 1, richTextBoxGeneral);
                            Continuous_Acquisition_state = false;
                            ChangeIcon(AppDomain.CurrentDomain.BaseDirectory + @"Icon\Start.png", Continuous_Acquisition_Icon, "連續取像");
                        }
                        break;
                    }
                case nameof(Freeze):
                    {
                        if (Continuous_Acquisition_state)
                        {
                            if (Freeze_state)
                            {
                                UI.FreezeImage();
                                Logger.WriteLog("Freeze Image!", 1, richTextBoxGeneral);
                                Freeze_state = false;
                                ChangeIcon(AppDomain.CurrentDomain.BaseDirectory + @"Icon\Unfreeze.png", Freeze_Icon, "恢復取像畫面");
                            }
                            else if (!Freeze_state)
                            {
                                UI.ContinuousAcquisition();
                                Logger.WriteLog("Unfreeze Image!", 1, richTextBoxGeneral);
                                Freeze_state = true;
                                ChangeIcon(AppDomain.CurrentDomain.BaseDirectory + @"Icon\Freeze.png", Freeze_Icon, "凍結取像畫面");
                            }

                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show("Please check to turn on the camera!", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
                        } 
                        break;
                    }
                case nameof(Save_Image):
                    {
                        if (Continuous_Acquisition_state)
                        {
                            if (!string.IsNullOrEmpty(Save_Image_Path.Text))
                            {
                                UI.SaveImage(System.IO.Path.Combine(Save_Image_Path.Text, DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".bmp"),"None");
                                Logger.WriteLog("Save Image "+ DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".bmp", 1, richTextBoxGeneral);
                            }
                            else
                            {
                                HandyControl.Controls.MessageBox.Show("Please set the save image path!", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show("Please check to turn on the camera!", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    }
                case nameof(Load_Parameter):
                    {
                        if (Continuous_Acquisition_state)
                        {
                            UI.LoadParameter(Camera_Parameter_File.Text);
                            Logger.WriteLog("Load Parameter!", 1, richTextBoxGeneral);
                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show("Please check to turn on the camera!", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    }
            }
        }
        #endregion

        #region  Camera Parameter Screen
        private void Camera_Parameter_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case nameof(Open_Camera_Parameter_File):
                    {
                        OpenFileDialog camera_parameter_file = new OpenFileDialog();
                        camera_parameter_file.Title = "Choose Camera Parameter File";
                        camera_parameter_file.Filter = "Camera Config Files(*.ini)|*.ini";
                        Nullable<bool> result = camera_parameter_file.ShowDialog();
                        if (result == true)
                        {
                            Camera_Parameter_File.Text = camera_parameter_file.FileName;
                            Logger.WriteLog("Set Camera Parameter Path!", 1, richTextBoxGeneral);
                        }
                        break;
                    }
                case nameof(Open_Save_Image_Path):
                    {
                        System.Windows.Forms.FolderBrowserDialog save_image_path = new System.Windows.Forms.FolderBrowserDialog();
                        save_image_path.Description = "Choose Save Image Path";
                        save_image_path.ShowDialog();
                        Save_Image_Path.Text = save_image_path.SelectedPath;
                        break;
                    }
                case nameof(Save_Config):
                    {
                        List<Parameter> Parameter_config = new List<Parameter>()
                        {
                            new Parameter() {Camera_Parameter_File_val=Camera_Parameter_File.Text,
                                             Save_Image_Path_val=Save_Image_Path.Text,
                                             Gamma_val=Convert.ToInt32(Gamma.Value),
                                             Gain_val = Convert.ToInt32(Gain.Value)
                                            }
                        };
                        Config.Save(Parameter_config);
                        Logger.WriteLog("Save Parameter!", 1, richTextBoxGeneral);
                        break;
                    }

            }
        }
        #endregion

        
    }
}
