
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace RcloneWrapper
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service started " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 20000; 
            timer.Enabled = true;

          
            var cParams = "mount blob:poc c:\\Azure";
            string filename = "rclone.exe";
            try
            {
                var proc = System.Diagnostics.Process.Start(filename, cParams);
            }
            catch {
                WriteToFile("Error");
            }
            
        }
        protected override void OnStop()
        {

            WriteToFile("Service stopped " + DateTime.Now);
       
            var cParams = "/F /IM rclone.exe";
            string filename = "taskkill.exe";
            try
            {
                var proc = System.Diagnostics.Process.Start(filename, cParams);
            }
            catch
            {
                WriteToFile("Error");
            }
          
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is still alive " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\RcloneWrapper_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
