using System.Windows.Forms;
using GoL.frm.Infrastructure;

namespace GoL.frm
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            using (var app = new GameOfLifeGraphicsApp())
            {
                app.Run();
            }
        }
    }
}
