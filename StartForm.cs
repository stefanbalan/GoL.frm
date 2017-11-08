using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using Configuration = GoL.Infrastructure.Configuration;

namespace GoL
{
    public partial class StartForm : Form
    {
        private GameOfLifeApp _app;
        private GameOfLifeBase<CellWorld> _game;
        private Task _appTask;
        private Task _gameTask;

        public StartForm()
        {
            InitializeComponent();
            _game = new GameOfLifeFinal();
            _app = new GameOfLifeApp(new Configuration(), _game);

            txtBack.Text = _app.Configuration.BackColor.ToAbgr().ToString("X8");
            txtLive.Text = _app.Configuration.LiveColor.ToAbgr().ToString("X8");
            txtBorn.Text = _app.Configuration.BornColor.ToAbgr().ToString("X8");
            txtDead.Text = _app.Configuration.DeadColor.ToAbgr().ToString("X8");



            _appTask = new Task(_app.Run);
            _appTask.Start();

        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
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

        #region  settings
        private void txtBackground_LostFocus(object sender, System.EventArgs e)
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

        private void txtLive_LostFocus(object sender, System.EventArgs e)
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

        private void txtBorn_LostFocus(object sender, System.EventArgs e)
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

        private void txtDead_LostFocus(object sender, System.EventArgs e)
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

        #endregion

    }
}
