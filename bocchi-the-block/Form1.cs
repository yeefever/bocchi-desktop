using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bocchi_the_block
{
    public partial class Form1 : Form
    {

        private NotifyIcon trayIcon;
        Random rand;


        public Form1()
        {
            rand = new Random(30293);
            InitializeComponent();
            InitFreqBar();
            //InitTrayIcon();
        }
        private void InitFreqBar()
        {
            freqBar.Minimum = 1;
            freqBar.Maximum = 5;
            freqBar.Value = 3;
            freqBar.LargeChange = 1;
            freqBar.TickStyle = TickStyle.None;
            freqBar.AutoSize = false;
            freqBar.Scroll += freqBar_scroll;
        }

        private void InitTrayIcon()
        {
            // Create the NotifyIcon control
            trayIcon = new NotifyIcon();
            trayIcon.Icon = Icon.FromHandle(((Bitmap)Properties.Resources.bocchi_systray).GetHicon());
            trayIcon.Text = "Bocchi";
            trayIcon.Visible = true;

            // Context menu for the NotifyIcon
            ContextMenu contextMenu = new ContextMenu();
            MenuItem restoreMenuItem = new MenuItem("Restore");
            restoreMenuItem.Click += RestoreMenuItem_Click;
            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;

            contextMenu.MenuItems.Add(restoreMenuItem);
            contextMenu.MenuItems.Add(exitMenuItem);

            // Assign the context menu to the NotifyIcon
            trayIcon.ContextMenu = contextMenu;

            // Handle the FormClosing event to minimize to system tray instead of closing
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Main Form Closing");
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void RestoreMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Restore Menu Clicked");
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Exit Menu Clicked");
            trayIcon.Dispose();
            Close();
        }

        private void freqBar_scroll(object sender, EventArgs e)
        {
            freqLabel.Text = "Frequency: " + freqBar.Value.ToString();
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                int randomNumber = rand.Next(1, 3);
                 
                // if 1 start an idle dragon bocchi
                // if 2 start a dorito bocchi 
                // if 3 start a fire dragon bocchi that transitions into an idle bocchi.

                switch(randomNumber)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                

                using (ChildFormDragonBocchi cform = new ChildFormDragonBocchi(freqBar.Value))
                //using (ChildFormDoritoBocchi cform = new ChildFormDoritoBocchi(freqBar.Value))
                {
                    this.Hide();
                    /*
                    cform.StartPosition = FormStartPosition.Manual;
                    Rectangle screenBounds = Screen.PrimaryScreen.WorkingArea;
                    int x = screenBounds.Right - cform.Width;
                    int y = screenBounds.Bottom - cform.Height;
                    cform.Location = new Point(x, y);
                    */
                    // Show the dialog
                    cform.ShowDialog();
                }
                this.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void freqLabel_Click(object sender, EventArgs e)
        {
        }
    }
}