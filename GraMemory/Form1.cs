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

        public FormMemory()
        {
            InitializeComponent();

            settings = new GameSettings();

            SetBoard();

            SetCards();
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
                Guid id = new Guid();

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

                    // odkrywamy kartę
                    card.ShowCard();
                    // i dodajemy do panelu
                    panelKart.Controls.Add(card);

                    cards.Remove(card);
                }
            }
        }
    }
}

