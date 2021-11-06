using GraMemory.Klasy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraMemory
{
    public partial class FormMemory : Form
    {
        GameSettings settings;
        MemoryCard firstCard = null;
        MemoryCard secondCard = null;

        public FormMemory()
        {
            InitializeComponent();

            settings = new GameSettings();

            SetBoard();

            SetCards();

            timerStart.Start();
        }

        void SetBoard()
        {
            panelKart.Height = settings.Size * settings.Columns;
            panelKart.Width = settings.Size * settings.Rows;

            Height = panelKart.Height + 100;
            Width = panelKart.Width + 30;

            labelStart.Text = "Gra rozpocznie sie za " + settings.ShowTime;
            labelCzas.Text = settings.GameTime.ToString();
            labelPunkty.Text = settings.Score.ToString();
        }

        void SetCards()
        {
            // pobieramy do tablicy ścieżki do plików z obrazkami
            string[] cardsFiles = Directory.GetFiles(settings.FrontPicFolder);

            // tworzymy listę na karty
            List<MemoryCard> cards = new List<MemoryCard>();

            foreach (var file in cardsFiles)
            {
                Guid id = Guid.NewGuid();

                MemoryCard card1 = new MemoryCard(id, settings.BackPic, file);
                cards.Add(card1);

                MemoryCard card2 = new MemoryCard(id, settings.BackPic, file);
                cards.Add(card2);
            }
            // generator liczb losowych
            Random generator = new Random();

            for (int rows = 0; rows < settings.Rows; rows++)
            {
                for (int columns = 0; columns < settings.Columns; columns++)
                {
                    int index = generator.Next(0, cards.Count);

                    var card = cards[index];

                    int margin = 2;

                    card.Location = new Point(rows * settings.Size + rows * margin, columns * settings.Size + columns * margin);

                    card.Width = settings.Size;
                    card.Height = settings.Size;
                    card.Click += BtnClicked;

                    // odkrywamy kartę
                    card.ShowCard();
                    // i dodajemy do panelu
                    panelKart.Controls.Add(card);

                    cards.Remove(card);
                }
            }


        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            settings.ShowTime--;
            labelStart.Text = $"Gra rozpocznie się za {settings.ShowTime}";

            if (settings.ShowTime <= 0)
            {
                labelStart.Visible = false;

                foreach (var control in panelKart.Controls)
                {
                    MemoryCard card = (MemoryCard)control;
                    card.HideCard();
                }
                timerStart.Stop();
                timerGame.Start();
            }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            settings.GameTime--;
            labelCzas.Text = settings.GameTime.ToString();

            if (settings.GameTime <= 0 || settings.Score == settings.MaxScore)
            {
                timerGame.Stop();
                timerShow.Stop();

                MessageBox.Show(
                    $"Zdobyte punkty: {settings.Score}",
                    "Koniec Gry",
                    MessageBoxButtons.OK
                    );

                Application.Exit();
            }
        }

        private void labelCzas_Click(object sender, EventArgs e)
        {

        }

        private void BtnClicked(object sender, EventArgs e)
        {
            MemoryCard btn = (MemoryCard)sender;

            if (firstCard == null)
            {
                firstCard = btn;
                firstCard.ShowCard();
            }
            else
            {
                secondCard = btn;
                secondCard.ShowCard();
                panelKart.Enabled = false;

                if (firstCard.ID == secondCard.ID)
                {
                    settings.Score++;
                    labelPunkty.Text = settings.Score.ToString();

                    firstCard = null;
                    secondCard = null;

                    panelKart.Enabled = true;
                }
                else
                {
                    timerShow.Start();
                }
            }
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            firstCard.HideCard();
            secondCard.HideCard();

            firstCard = null;
            secondCard = null;

            panelKart.Enabled = true;

            timerShow.Stop();
        }
    }
}
