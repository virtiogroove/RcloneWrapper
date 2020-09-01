
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
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;

            var cPath = "C:\\rclone\\";
            var cParams = "mount blob:poc c:\\Azure";
            string filename = "rclone.exe";
            try
            {
                var proc = System.Diagnostics.Process.Start(filename, cParams);
            }
            catch {
                WriteToFile("Error");
            }
            //WriteToFile(proc.ExitCode.ToString());

            /*const string ex1 = "mount";
            const string ex2 = "blob:poc";
            const string ex3 = "c:\\Azure";

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "rclone.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = ex1 + " " + ex2 +" " +ex3;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }*/
        }
        protected override void OnStop()
        {

            WriteToFile("Service is stopped at " + DateTime.Now);
           // var cPath = "C:\\rclone\\";
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
            // Use ProcessStartInfo class
            /* ProcessStartInfo startInfo = new ProcessStartInfo();
             startInfo.CreateNoWindow = false;
             startInfo.UseShellExecute = true;
             startInfo.FileName = "rclone.exe";
             startInfo.WindowStyle = ProcessWindowStyle.Hidden;
             startInfo.Arguments = ex1 + " " + ex2 + " " + ex3;

             try
             {
                 // Start the process with the info we specified.
                 // Call WaitForExit and then the using statement will close.
                 using (Process exeProcess = Process.Start(startInfo))
                 {
                     WriteToFile(startInfo.ErrorDialog.ToString() + DateTime.Now);
                     exeProcess.WaitForExit();



                 }
             }
             catch
             {
                 // Log error.
             }*/
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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
