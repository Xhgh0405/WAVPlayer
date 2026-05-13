using System;
using System.Windows.Forms;

namespace HW3_WAVPlayer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmWAVPlayer());
        }
    }
}
