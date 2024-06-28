using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace IDSCamera
{
    public class BaseLogRecord
    {
        public void WriteLog(string message,int level, RichTextBox rtb)
        {
            string DIRNAME = null;
            string FILENAME = null;
            switch (level)
            {
                case 1:
                    DIRNAME = AppDomain.CurrentDomain.BaseDirectory + @"\Logger\General\";
                    FILENAME = DIRNAME + "GeneralLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    Display(message, Colors.Black, rtb);
                    Log(DIRNAME, FILENAME, message);
                    break;
                case 2:
                    DIRNAME = AppDomain.CurrentDomain.BaseDirectory + @"\Logger\Warning\";
                    FILENAME = DIRNAME + "WarningLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    Display(message, Colors.Orange, rtb);
                    Log(DIRNAME, FILENAME, message);
                    break;
                case 3:
                    DIRNAME = AppDomain.CurrentDomain.BaseDirectory + @"\Logger\Debug\";
                    FILENAME = DIRNAME + "DebugLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    Display(message, Colors.Blue, rtb);
                    Log(DIRNAME, FILENAME, message);
                    break;
                case 4:
                    DIRNAME = AppDomain.CurrentDomain.BaseDirectory + @"\Logger\Error\";
                    FILENAME = DIRNAME + "ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    Display(message, Colors.Red, rtb);
                    Log(DIRNAME, FILENAME, message);
                    break;
            }
        }

        private void Log(string DIRNAME , string FILENAME, string logMessage)
        {
            if (!Directory.Exists(DIRNAME))
                Directory.CreateDirectory(DIRNAME);
            if (!File.Exists(FILENAME))
                File.Create(FILENAME).Close();
            using (StreamWriter sw = File.AppendText(FILENAME))
            {
                sw.WriteLine($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} - {logMessage}");
            }
            
        }

        private void Display(string message, Color color, RichTextBox rtb)
        {
            rtb.AppendText(DateTime.Now.ToLongDateString() + "," + DateTime.Now.ToLongTimeString() + ">" + message+"\n");
            rtb.Foreground = new SolidColorBrush(color);
            rtb.ScrollToEnd();
            rtb.UpdateLayout();
            //Run run = new Run() { Text = DateTime.Now.ToLongDateString() + "," + DateTime.Now.ToLongTimeString() + ">" + message, Foreground = new SolidColorBrush(color) };
            //Paragraph paragraph = new Paragraph();
            //paragraph.Inlines.Add(run);
            //rtb.Document.Blocks.Add(paragraph);
            //rtb.ScrollToEnd();
            //rtb.UpdateLayout();
        }
    }
}
