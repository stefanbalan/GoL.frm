using System.Windows.Forms;

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
