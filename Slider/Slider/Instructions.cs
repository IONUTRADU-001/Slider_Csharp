using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider
{
    public partial class Instructions : Form
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblInstr_Click(object sender, EventArgs e)
        {

        }

        private void Instructions_Load(object sender, EventArgs e)
        {
            lblInstr.Text = "Welcome to Slider Game.\n" +
                "1. You can move pieces that contain a part of the big image by clicking it. " +
                " ATTENTION! You can only move pieces that are above, below, on the left or " +
                "on the right part of the black piece with exactly one position!Any other move is not " +
                "allowed.\n" +
                "2. Click the <Image> button to choose the picture you want to start the game with.\n" +
                "3. If you get stucked, you can click anytime on the <Shuffle> button to rearrange the pieces.\n" +
                "4. You can quit the game whenever you want by clicking the <Quit> button.\n" +
                "5. You will see the time and number of moves in which you managed to solve the puzzle.\n" +
                "6. After every piece is at its own place you will be announced that you won!\n" +
                "Good luck! :)";
        }
    }
}
