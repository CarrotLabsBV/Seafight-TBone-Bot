using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Seafight_TBone.HTTP.License;
using System.IO;
using System.Threading;

namespace Seafight_TBone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread _t1 = new Thread(new ThreadStart(chckb4launch));
            _t1.IsBackground = true;
            _t1.Start();
        }
        public static bool isLegal = false;
        public static void chckb4launch()
        {
            while (true)
            {
                if (Directory.Exists(Environment.CurrentDirectory + "/license/"))
                {
                    TextReader tw = new StreamReader(Environment.CurrentDirectory + "/license/information.txt");
                    string line = tw.ReadLine();
                    if (line != "")
                    {
                        string[] splittedLine = line.Split('*');
                        LicenseChecker.checkLicense(splittedLine[3], splittedLine[2], splittedLine[4], 0);
                    }
                    tw.Close();
                }
                Thread.Sleep(15000);
            }
        }
        private void chromeButton1_Click(object sender, EventArgs e)
        {
            LicenseChecker.checkLicense(textBox1.Text, textBox2.Text, textBox3.Text + "-" + textBox4.Text + "-" + textBox5.Text + "-" + textBox6.Text);
        }
    }
}
