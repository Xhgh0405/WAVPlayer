using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace HW3_WAVPlayer
{
    public class frmWAVPlayer : Form
    {
        private GroupBox grpPath;
        private GroupBox grpButton;
        private TextBox txtPath;
        private Button btnBrowse;
        private Button btnPlay;
        private Button btnLoop;
        private Button btnStop;
        private Button btnEnd;
        private OpenFileDialog ofdWAVFile;
        private SoundPlayer player;

        private Panel pnlPiano;
        private Panel[] pianoKeys;
        private Timer pianoTimer;
        private Random rnd;

        public frmWAVPlayer()
        {
            InitializeComponent();
            player = new SoundPlayer();
            rnd = new Random();

            string defaultFile = Path.Combine(Application.StartupPath, "sample_media", "XMAS.WAV");
            if (File.Exists(defaultFile))
            {
                txtPath.Text = defaultFile;
                player.SoundLocation = defaultFile;
            }
        }

        private void InitializeComponent()
        {
            this.Text = "WAV 音效檔播放器";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(760, 330);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.FormClosing += frmWAVPlayer_FormClosing;

            grpPath = new GroupBox();
            grpPath.Text = "音效位置";
            grpPath.Location = new Point(12, 12);
            grpPath.Size = new Size(720, 62);

            txtPath = new TextBox();
            txtPath.Location = new Point(15, 25);
            txtPath.Size = new Size(560, 22);
            txtPath.ReadOnly = true;

            btnBrowse = new Button();
            btnBrowse.Text = "瀏覽";
            btnBrowse.Location = new Point(600, 23);
            btnBrowse.Size = new Size(75, 26);
            btnBrowse.Click += btnBrowse_Click;

            grpPath.Controls.Add(txtPath);
            grpPath.Controls.Add(btnBrowse);

            grpButton = new GroupBox();
            grpButton.Text = "播放按鈕";
            grpButton.Location = new Point(12, 82);
            grpButton.Size = new Size(720, 60);

            btnPlay = new Button();
            btnPlay.Text = "播放一次";
            btnPlay.Location = new Point(18, 23);
            btnPlay.Size = new Size(100, 28);
            btnPlay.Click += btnPlay_Click;

            btnLoop = new Button();
            btnLoop.Text = "重複播放";
            btnLoop.Location = new Point(145, 23);
            btnLoop.Size = new Size(100, 28);
            btnLoop.Click += btnLoop_Click;

            btnStop = new Button();
            btnStop.Text = "停止播放";
            btnStop.Location = new Point(272, 23);
            btnStop.Size = new Size(100, 28);
            btnStop.Click += btnStop_Click;

            btnEnd = new Button();
            btnEnd.Text = "結束程式";
            btnEnd.Location = new Point(399, 23);
            btnEnd.Size = new Size(100, 28);
            btnEnd.Click += btnEnd_Click;

            grpButton.Controls.Add(btnPlay);
            grpButton.Controls.Add(btnLoop);
            grpButton.Controls.Add(btnStop);
            grpButton.Controls.Add(btnEnd);

            pnlPiano = new Panel();
            pnlPiano.Location = new Point(12, 160);
            pnlPiano.Size = new Size(720, 130);
            pnlPiano.BorderStyle = BorderStyle.FixedSingle;
            pnlPiano.BackColor = Color.DimGray;

            pianoKeys = new Panel[10];
            for (int i = 0; i < pianoKeys.Length; i++)
            {
                pianoKeys[i] = new Panel();
                pianoKeys[i].Size = new Size(65, 105);
                pianoKeys[i].Location = new Point(i * 70 + 8, 12);
                pianoKeys[i].BackColor = Color.White;
                pianoKeys[i].BorderStyle = BorderStyle.FixedSingle;
                pnlPiano.Controls.Add(pianoKeys[i]);
            }

            Label lblInfo = new Label();
            lblInfo.Text = "播放時鋼琴鍵會隨音樂閃動";
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.White;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Location = new Point(10, 5);
            // 文字會被琴鍵擋到，所以這裡不加入面板，只保留鋼琴動畫畫面

            pianoTimer = new Timer();
            pianoTimer.Interval = 150;
            pianoTimer.Tick += PianoTimer_Tick;

            ofdWAVFile = new OpenFileDialog();
            ofdWAVFile.DefaultExt = "wav";
            ofdWAVFile.FileName = "";
            ofdWAVFile.Filter = "WAV Files (*.wav)|*.wav";

            this.Controls.Add(grpPath);
            this.Controls.Add(grpButton);
            this.Controls.Add(pnlPiano);
        }

        private void PianoTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < pianoKeys.Length; i++)
            {
                pianoKeys[i].BackColor = Color.White;
                pianoKeys[i].Top = 12;
            }

            int count = rnd.Next(1, 4);
            for (int i = 0; i < count; i++)
            {
                int index = rnd.Next(0, pianoKeys.Length);
                pianoKeys[index].BackColor = Color.LightSkyBlue;
                pianoKeys[index].Top = 18;
            }
        }

        private void ResetPianoKeys()
        {
            pianoTimer.Stop();
            for (int i = 0; i < pianoKeys.Length; i++)
            {
                pianoKeys[i].BackColor = Color.White;
                pianoKeys[i].Top = 12;
            }
        }

        private bool PreparePlayer()
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text) || !File.Exists(txtPath.Text))
            {
                MessageBox.Show("請先選擇 WAV 音效檔。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            player.Stop();
            player.SoundLocation = txtPath.Text;
            player.Load();
            return true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdWAVFile.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = ofdWAVFile.FileName;
                player.SoundLocation = txtPath.Text;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (PreparePlayer())
            {
                player.Play();
                pianoTimer.Start();
            }
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            if (PreparePlayer())
            {
                player.PlayLooping();
                pianoTimer.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
            ResetPianoKeys();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWAVPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) e.Cancel = true;
            else
            {
                player.Stop();
                ResetPianoKeys();
            }
        }
    }
}
