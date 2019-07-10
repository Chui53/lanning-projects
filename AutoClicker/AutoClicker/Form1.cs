using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        static Thread AutoClick;
        static int militime;
        public static int WM_HOTKEY = 0x312;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private static readonly Random rnd = new Random();

        public void AClick()
        {
            while (true)
            {
                uint x = (uint)Cursor.Position.X;
                uint y = (uint)Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                Thread.Sleep(rnd.Next(0,militime));

            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoClick = new Thread(AClick);
            RegisterHotKey(this.Handle, (int)Keys.F1, 0, (uint)Keys.F1);
            AutoClick.IsBackground = true;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                try
                {
                    militime = int.Parse(textBox1.Text);
                }
                catch
                {
                    militime = 0;
                }
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                if (key == Keys.F1)
                {
                    if (!AutoClick.IsAlive)
                    {
                        AutoClick.Start();
                        lstate.Text = "Clicking";
                        lstate.ForeColor = System.Drawing.Color.Green;
                        Controls.Add(lstate);
                    }
                    else
                    {
                        AutoClick.Abort();
                        AutoClick = new Thread(AClick);
                        lstate.Text = "Not Clicking";
                        lstate.ForeColor = System.Drawing.Color.Red;
                        Controls.Add(lstate);
                    }
                }
            }

        }

    }
}
