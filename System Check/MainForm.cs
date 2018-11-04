using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Timer = System.Timers.Timer;

namespace System_Check
{
    public partial class MainForm : Form
    {
        [Dl﻿lImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharS﻿et = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            recordFunction(); //start the record function

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer(); //create a timer
            t.Interval = 60000; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick); //
            t.Start(); //start the timer
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            saveFunction();
            recordFunction();
        }

        public void recordFunction()
        {
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        public void saveFunction()
        {
            string location = @"d:\\temp\" + DateTime.Now.ToString("ddMMyy") + @"\";
            bool folderExists = System.IO.Directory.Exists(location);
            if (!folderExists)
                System.IO.Directory.CreateDirectory(location);
            string date = DateTime.Now.ToString("hh.mm.ss");
            string txt = "save recsound " + location + date + ".wav";
            record(txt, "", 0, 0);
            record("close recsound", "", 0, 0);
        }
    }
}
