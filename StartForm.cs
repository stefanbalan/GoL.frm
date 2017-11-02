using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using Configuration = GoL.frm.Infrastructure.Configuration;

namespace GoL.frm
{
    public partial class StartForm : Form
    {
        private GameOfLifeGraphicsApp _app;
        private Task _appTask;

        public StartForm()
        {
            InitializeComponent();
            _app = new GameOfLifeGraphicsApp(new Configuration());
            txtBack.Text = _app.Configuration.BackColor.ToAbgr().ToString("X8");
            txtLive.Text = _app.Configuration.LiveColor.ToAbgr().ToString("X8");
            txtBorn.Text = _app.Configuration.BornColor.ToAbgr().ToString("X8");
            txtDead.Text = _app.Configuration.DeadColor.ToAbgr().ToString("X8");

            _appTask = new Task(_app.Run);
            _appTask.Start();
        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
        {

        }

        #region color settings
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
        #endregion
    }
}
