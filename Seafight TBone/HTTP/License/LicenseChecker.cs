using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Seafight_TBone.HTTP.License
{
    public class LicenseChecker
    {
        public static string name;
        public static string email;
        public static string key;
        public static void checkLicense(string name1, string email1, string key1, int popup = 1)
        {
            name = name1;
            email = email1;
            key = key1;
            Thread _t = new Thread( new ParameterizedThreadStart(chck));
            _t.IsBackground = true;
            _t.Start(popup);
        }
        public static void chck(object popup)
        {
            int popup1 = (int)popup;
            if (name != "" && email != "" && key != "")
            {
                if (name.Length >= 2 && email.Length >= 5 && key.Length >= 12)
                {
                    string isOK002 = HttpOptions.GetPage("http://tbone.unknownrs.net78.net/?a=chck&k=" + key);
                    if (isOK002.Contains("OK002"))
                    {
                        string[] isOK003 = isOK002.Split('*');
                        if (isOK003[3] == name)
                        {
                            if (isOK003[2] == email)
                            {
                                if (popup1 == 1)
                                {
                                    MessageBox.Show("Your license will expire on: " + isOK003[1] + "!", "INF001", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                if (Directory.Exists(Environment.CurrentDirectory + "/license/"))
                                {
                                    TextWriter tw = new StreamWriter(Environment.CurrentDirectory + "/license/information.txt");
                                    tw.WriteLine(isOK002);
                                    tw.Close();
                                }
                                else
                                {
                                    Directory.CreateDirectory(Environment.CurrentDirectory + "/license/");
                                    TextWriter tw = new StreamWriter(Environment.CurrentDirectory + "/license/information.txt");
                                    tw.WriteLine(isOK002);
                                    tw.Close();
                                }
                                Form1.isLegal = true;
                            }
                            else
                            {
                                if (popup1 == 1)
                                {
                                    MessageBox.Show("Email doesn't match with the key!", "ERR003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            if (popup1 == 1)
                            {
                                MessageBox.Show("Name doesn't match with the key!", "ERR002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        if (popup1 == 1)
                        {
                            MessageBox.Show("License key doesn't exist!", "ERR001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
