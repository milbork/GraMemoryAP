using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraMemory.Klasy
{
    public class MemoryCard : Label
    {
        public Guid ID;
        public Image Back;
        public Image Front;

        public MemoryCard(Guid iD, Image back, Image front)
        {
            ID = iD;
            Back = back;
            Front = front;

            BackgroundImageLayout = ImageLayout.Stretch;
        }

        //TODO: Metody zakrywania i odkrywania kart
    }
}
