using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using Configuration = GoLife.Infrastructure.Configuration;

namespace GoLife
{
    public partial class StartForm : Form
    {
        private readonly GameOfLifeApp _app;
        private readonly GameOfLifeBase<CellWorld> _game;
        private Task _gameTask;

        public StartForm()
        {
            InitializeComponent();
            _game = new GameOfLife();
            //_game = new GameOfLifeTemplate();

            _app = new GameOfLifeApp(new Configuration(), _game);

            txtBack.Text = _app.Configuration.BackColor.ToAbgr().ToString("X8");
            txtLive.Text = _app.Configuration.LiveColor.ToAbgr().ToString("X8");
            txtBorn.Text = _app.Configuration.BornColor.ToAbgr().ToString("X8");
            txtDead.Text = _app.Configuration.DeadColor.ToAbgr().ToString("X8");
            txtDelay.Text = _app.Configuration.TargetMs.ToString("D");
            trkDelay.Value = 4;

            if (Directory.Exists("patterns"))
            {
                var di = new DirectoryInfo("patterns");
                lstPatterns.Items.AddRange(di.GetFiles());
            }

            var appTask = new Task(_app.Run);
            appTask.Start();

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_gameTask?.Status != TaskStatus.Running)
            {
                _game.Stop = false;
                _gameTask = new Task(_game.Run);
                _gameTask.Start();
                btnStartStop.Text = @"Stop";
            }
            else
            {
                _game.Stop = true;
                btnStartStop.Text = @"Start";
            }
        }
        private void btnStep_Click(object sender, EventArgs e)
        {
            _game.Stop = true;
            btnStartStop.Text = @"Start";

            if (_gameTask?.Status != TaskStatus.Running)
            {
                _gameTask = new Task(_game.Run);
                _gameTask.Start();
            }
        }

        #region  settings
        private void txtBackground_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtBack.Text,
                NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture,
                out int color))
            {
                _app.Configuration.BackColor = Color.FromAbgr(color);
            }

            txtBack.Text = _app.Configuration.BackColor.ToAbgr().ToString("X8");
        }

        private void txtLive_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtLive.Text,
                NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture,
                out int color))
            {
                _app.Configuration.LiveColor = Color.FromAbgr(color);
            }

            txtLive.Text = _app.Configuration.LiveColor.ToAbgr().ToString("X8");
        }

        private void txtBorn_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtBorn.Text,
                NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture,
                out int color))
            {
                _app.Configuration.BornColor = Color.FromAbgr(color);
            }

            txtBorn.Text = _app.Configuration.BornColor.ToAbgr().ToString("X8");
        }

        private void txtDead_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtDead.Text,
                NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture,
                out int color))
            {
                _app.Configuration.DeadColor = Color.FromAbgr(color);
            }

            txtDead.Text = _app.Configuration.DeadColor.ToAbgr().ToString("X8");
        }

        private void txtDelay_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtDelay.Text, NumberStyles.None, CultureInfo.InvariantCulture, out int delay))
            {
                _app.Configuration.TargetMs = delay;
            }

            txtDelay.Text = _app.Configuration.TargetMs.ToString();
        }

        private void trkDelay_Scroll(object sender, EventArgs e)
        {
            var delay = 0;
            switch (trkDelay.Value)
            {
                case 1: delay = 0; break;
                case 2: delay = 50; break;
                case 3: delay = 100; break;
                case 4: delay = 200; break;
                case 5: delay = 1000; break;
            }
            _app.Configuration.TargetMs = delay;

            txtDelay.Text = _app.Configuration.TargetMs.ToString();
        }

        private void chkHighlight_CheckedChanged(object sender, EventArgs e)
        {
            _game.HighlightChanges = chkHighlight.Checked;
        }


        #endregion

        private void lstPatterns_DoubleClick(object sender, EventArgs e)
        {
            LoadPattern();
        }

        private void btnLoadPattern_Click(object sender, EventArgs e)
        {
            LoadPattern();
        }

        private void LoadPattern()
        {
            if (lstPatterns.SelectedItem == null) return;
            var sr = ((FileInfo)lstPatterns.SelectedItem).OpenText();
            var world = CellWorld.FromRLE(sr);
            _game.Initialize(new Generation<CellWorld>
            {
                Live = world
            });
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            stsLabelAverageTime.Text = $@"{_game.AverageTimeMs}ms";
        }
    }
}
