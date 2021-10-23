using GraMemory.Klasy;
using System.Windows.Forms;

namespace GraMemory
{
    public partial class FormMemory : Form
    {
        GameSettings settings;

        public FormMemory()
        {
            InitializeComponent();

            settings = new GameSettings();

            SetCards();
        }

        void SetCards()
        {
            panelKart.Height = settings.Size * settings.Columns;
            panelKart.Width = settings.Size * settings.Rows;

            Height = panelKart.Height + 100;
            Width = panelKart.Width + 30;

            labelStart.Text = "Gra rozpocznie sie za " + settings.ShowTime;
            labelCzas.Text = settings.GameTime.ToString();
            labelPunkty.Text = settings.Score.ToString();
        }
    }
}
