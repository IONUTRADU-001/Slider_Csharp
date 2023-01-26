using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider
{   

    public partial class GameForm : Form
    {
        TimeTracker timetracker = new TimeTracker(); //cream un obiect de tip TImeTracker

        //matricea in care se vor memora bucati din imaginea finala (ca piesele de puzzle)
        Bitmap[,] imgarray = new Bitmap[4, 4];
        //se initializeaza numarul de mutari cu 0
        int numberOfMoves = 0;
        //pentru a redimensiona imaginea sa se potriveasca in cadranul din joc, indiferent de dimensiunea pe care aceasta o are initial
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

      //se initializeaza componenta, se afiseaza nuamrul de mutari (initial 0) si se amesteca piesele din poze 
        public GameForm(TimeTracker tt)
        {
            timetracker = tt;
            InitializeComponent();
            labelMoves.Text += numberOfMoves;
            Shuffle();
        }

        private void labelTime_Click(object sender, EventArgs e)
        {

        }
        //functia care va fi e apelata la apasarea butonului de end game
        private void EndGame(object sender, FormClosingEventArgs e)
        {
            timetracker.setStopJoc(DateTime.Now);

            //se atentioneaza jucatorul ca s-a apasat butonul de end si se preia optiunea dorita
            DialogResult YesOrNO = MessageBox.Show("Sigur inchizi jocul?", "Slider Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sender as Button != btnEnd && YesOrNO == DialogResult.No) e.Cancel = true; //daca nu se doreste sa se inchida jocul se da cancel
            if (sender as Button == btnEnd && YesOrNO == DialogResult.Yes)
            {
                UpdateInFile();
                this.Close(); //se inchide jocul daca jucatorul a ales "da"
            }
        }
        //functia apelata la apasarea butonului de joc nou
        private void NewGame(object sender, EventArgs e)
        {
            timetracker.setStartJoc(DateTime.Now);

            //se atentioneaza ca s-a apasat butonul de joc nou si se preia optiunea jucatorului
            DialogResult dialogResult = MessageBox.Show("Esti sigur ca vrei sa incepi un joc nou?", "Slider Game", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) //daca se doreste inceperea unui joc nou se vor sterge toate imaginile 
            {
                btnOne.Image = null;
                btnTwo.Image = null;
                btnThree.Image = null;
                btnFour.Image = null;
                btnFive.Image = null;
                btnSix.Image = null;
                btnSeven.Image = null;
                btnEight.Image = null;
                btnNine.Image = null;
                btnTen.Image = null;
                btnEleven.Image = null;
                btnTwelve.Image = null;
                btnThirteen.Image = null;
                btnFourteen.Image = null;
                btnFifteen.Image = null;
                btnSixteen.Image = null;
                pictureBoxBg.Image = null;
                numberOfMoves = 0;
                labelMoves.Text = "Moves: 0";  //se actualizeaza numarul mutarilor cu 0
                btnShuffle.Enabled = false; //butonul de amestacat este ascuns (inca nu sunt piese de amestecat)
                btnNewGame.Enabled = false; //butonul pentru joc nou este ascuns (deja ne aflam intr-un joc nou, inca nu s-a ales imaginea)
                buttontnImage2.Enabled = true;  //butonul pentru alegerea unei noi imagini este acum disponibil
            }
        }

        //functia care face actualizarile in cazul in care jucatorul doreste sa amestece piesele
        private void checkShuffle(object sender, EventArgs e)
        {
            //se atentioneaza jucatorul ca amestecarea pieselor va genera un joc nou
            DialogResult dialogResult = MessageBox.Show("Esti sigur ca vrei sa amesteci piesele? Jocul se va restarta!", "Slider Game", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) //daca jucatorul vrea sa amestece piesele
            {
                numberOfMoves = 0;  //se actualizeaza numarul miscarilor (0-va fi joc nou)
                labelMoves.Text = "Moves: 0"; //se actualizeaza maesajul de pe forma
                Shuffle(); //se amesteca piesele
            }
           
        }
        //butonul de eng game apeleaza functia de end game
        private void btnEnd_Click(object sender, EventArgs e)
        {
            EndGame(sender, e as FormClosingEventArgs);
        }
        //butonul care deschide fereastra de selectare a unei imagini pentru un joc nou
        private void buttontnImage2_Click(object sender, EventArgs e)
        {
            
            //se deschide o fereastra noua pentru a cauta imaginea pe device-ul jucatorului
            OpenFileDialog chooseImage = new OpenFileDialog();
            //formatele pe care le suporta jocul pentru imaginea dorita
            chooseImage.Filter = "Image Files (*.jpg; *.jpeg; *.png;  *.bmp ) |.jpg; *.jpeg; *.png;  *.bmp";
            Image img ;  //daca s-a ales imaginea 

            //error handling
            try
            {
                if (chooseImage.ShowDialog() == DialogResult.OK)  //daca s-a selectat o imagine
                {
                    pictureBoxBg.Image = new Bitmap(chooseImage.FileName);  //salvam imaginea de tip bitmap pentru a o sparge in mai multe piese
                }
                img = Image.FromFile(chooseImage.FileName);

            }
            catch(Exception t)
            {
                MessageBox.Show("Alege o imagine!"); //se da un mesaj sugestiv 
                buttontnImage2_Click(sender, e);
                return;
            }


            img = resizeImage(img, new Size(380, 380)); //o redimensionam pentru a se potrivi cadranului din joc
            int widthThird = (int)((double)img.Width / 4.0);  //determinam latimea
            int heightThird = (int)((double)img.Height / 4.0); //si inaltimea corespunxzatoare pentru fiecare piesa din joc
            //se parcurg toate piesele (o matrice de 4x4)
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    imgarray[i, j] = new Bitmap(widthThird, heightThird); //se creeaza o noua bucata  din imaginea mare
                    Graphics g = Graphics.FromImage(imgarray[i, j]); //se preia partea corespunzatoare
                    //si se deseneaza
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);

                }
            //dupa ce imaginea mare a fost sparta in mai multe piese se actualizeaza butoanele de joc
            btnOne.Image = imgarray[0, 0];
            btnTwo.Image = imgarray[0, 1];
            btnThree.Image = imgarray[0, 2];
            btnFour.Image = imgarray[0, 3];
            btnFive.Image = imgarray[1, 0];
            btnSix.Image = imgarray[1, 1];
            btnSeven.Image = imgarray[1, 2];
            btnEight.Image = imgarray[1, 3];
            btnNine.Image = imgarray[2, 0];
            btnTen.Image = imgarray[2, 1];
            btnEleven.Image = imgarray[2, 2];
            btnTwelve.Image = imgarray[2, 3];
            btnThirteen.Image = imgarray[3, 0];
            btnFourteen.Image = imgarray[3, 1];
            btnFifteen.Image = imgarray[3, 2];
            btnSixteen.BackColor = Color.White; //piesa care ne va permite sa mutam restul pieselor
            btnShuffle.Enabled = true; //piesele se pot amesteca
            btnNewGame.Enabled = true;  //se poate incheia jocul
            buttontnImage2.Enabled = false; //nu se poate alege o alta imagine pana nu se va selecta joc nou
        }
        //functia care amesteca piesele
        public void Shuffle()
        {
            int i, j, randomized; 
            //se va memora intr-un array valoarea 27 de 16 ori (27 e arbitrar aleasa, poate fi orice valoare
            //atata timp cat nu e in intervalul 0-15 pentru ca acelea vor fi valori atribuite butoanelor
            //in a se vor memora mai tarziu numere random de la 0 la 15 care vor reprezenta pozitiile butoanelor cu imagini in joc
            int[] a = Enumerable.Repeat(27, 16).ToArray();
            Boolean added = false; //variabila cu care se va verifica daca nu s-a adaugat deja o pozitie in array (necasara
            //deoarece exista sanse ca random sa ne intoarca aceeasi valoare de mai multe ori)
            //cu i parcurgem elementele din a
            i = 0;
            do
            {
                Random rnd = new Random(); //se ia un random nou
                randomized = rnd.Next(16);
                for (j = 0; j <= i; j++) //se parcurc toate numerele din a de la 0 pana la i(pozitia curenta)
                {
                    if (a[j] == randomized) //daca avem deja numarul random adaugat
                    {
                        added = true; //semnalam ca e adaugat 
                        break; //si iesim din for
                    }

                }
                if (added == true) //daca s-a iesit fortat din for inseamna ca numarul e adaugat 
                {
                    added = false; //actualizam variabila pentru a continua verificarea pe alte numere
                }
                else //numarul nu a fost adaugat
                {
                    a[i] = randomized; //il adaugam in lista
                    i = i + 1; //si trecem la urmatoarea pozitie in array-ul a
                }
            }
            //cat timp mai sunt butoane de actualizat se actualizeaza in layoutbox (un tabl) pozitia fiecarui buton
            //utilizandu-ne de valorile din array-ul a, care sunt random
            // conventia /4 da randul si %4 da coloana
            while (i <= 15);
            layoutBox.SetRow(btnOne, a[0] / 4);
            layoutBox.SetColumn(btnOne, a[0] % 4);

            layoutBox.SetRow(btnTwo, a[1] / 4);
            layoutBox.SetColumn(btnTwo, a[1] % 4);

            layoutBox.SetRow(btnThree, a[2] / 4);
            layoutBox.SetColumn(btnThree, a[2] % 4);

            layoutBox.SetRow(btnFour, a[3] / 4);
            layoutBox.SetColumn(btnFour, a[3] % 4);

            layoutBox.SetRow(btnFive, a[4] / 4);
            layoutBox.SetColumn(btnFive, a[4] % 4);

            layoutBox.SetRow(btnSix, a[5] / 4);
            layoutBox.SetColumn(btnSix, a[5] % 4);

            layoutBox.SetRow(btnSeven, a[6] / 4);
            layoutBox.SetColumn(btnSeven, a[6] % 4);

            layoutBox.SetRow(btnEight, a[7] / 4);
            layoutBox.SetColumn(btnEight, a[7] % 4);

            layoutBox.SetRow(btnNine, a[8] / 4);
            layoutBox.SetColumn(btnNine, a[8] % 4);

            layoutBox.SetRow(btnTen, a[9] / 4);
            layoutBox.SetColumn(btnTen, a[9] % 4);

            layoutBox.SetRow(btnEleven, a[10] / 4);
            layoutBox.SetColumn(btnEleven, a[10] % 4);

            layoutBox.SetRow(btnTwelve, a[11] / 4);
            layoutBox.SetColumn(btnTwelve, a[11] % 4);

            layoutBox.SetRow(btnThirteen, a[12] / 4);
            layoutBox.SetColumn(btnThirteen, a[12] % 4);

            layoutBox.SetRow(btnFourteen, a[13] / 4);
            layoutBox.SetColumn(btnFourteen, a[13] % 4);

            layoutBox.SetRow(btnFifteen, a[14] / 4);
            layoutBox.SetColumn(btnFifteen, a[14] % 4);

            layoutBox.SetRow(btnSixteen, a[15] / 4);
            layoutBox.SetColumn(btnSixteen, a[15] % 4);

        }
        //functia care face interschimbarea intre butoane
        public void CheckButton(Button bttn1, Button bttn2)
        {
                TableLayoutPanelCellPosition cell1 = layoutBox.GetCellPosition(bttn1); //se preia pozitia primului buton
                layoutBox.SetCellPosition(bttn1, layoutBox.GetCellPosition(bttn2)); //pe butonul 1 se suprascrie butonul 2
                layoutBox.SetCellPosition(bttn2, cell1); //in locul butonului 2 se va pune butonul 1 
            //cell1 e variabila auxiliara
        }
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            checkShuffle(sender, e);
        }
        //functia care verifica daca se poate executa mutarea 
        //ca mutarea sa fie valida trebuie sa se selecteze un buton din optiunile:
        //imediat deasupra de butonul alb, imediat in stanga, imediat in drapta sau imediat sub butonul alb
        public int canMove(Button btn)
        {
            if (layoutBox.GetColumn(btn) + 1 == layoutBox.GetColumn(btnSixteen) && layoutBox.GetRow(btn) == layoutBox.GetRow(btnSixteen))
                return 1;
            if (layoutBox.GetColumn(btn) -1 == layoutBox.GetColumn(btnSixteen) && layoutBox.GetRow(btn) == layoutBox.GetRow(btnSixteen))
                return 1;
            if (layoutBox.GetColumn(btn)  == layoutBox.GetColumn(btnSixteen) && layoutBox.GetRow(btn)+1 == layoutBox.GetRow(btnSixteen))
                return 1;
            if (layoutBox.GetColumn(btn)  == layoutBox.GetColumn(btnSixteen) && layoutBox.GetRow(btn)-1 == layoutBox.GetRow(btnSixteen))
                return 1;
            return 0;

        }
        //functia care verifcia daca s-a castigat 
        //fiecare buton trebuie sa fie la pozitia corspunzatoare
        public int checkWin()
        {
            if((layoutBox.GetRow(btnOne)==0 && layoutBox.GetColumn(btnOne) == 0) &&
                (layoutBox.GetRow(btnTwo) == 0 && layoutBox.GetColumn(btnTwo) == 1) &&
                (layoutBox.GetRow(btnThree) == 0 && layoutBox.GetColumn(btnThree) == 2) &&
                 (layoutBox.GetRow(btnFour) == 0 && layoutBox.GetColumn(btnFour) == 3) &&
                 (layoutBox.GetRow(btnFive) == 1 && layoutBox.GetColumn(btnFive) == 0) &&
                 (layoutBox.GetRow(btnSix) == 1 && layoutBox.GetColumn(btnSix) == 1) &&
                 (layoutBox.GetRow(btnSeven) == 1 && layoutBox.GetColumn(btnSeven) == 2) &&
                 (layoutBox.GetRow(btnEight) == 1 && layoutBox.GetColumn(btnEight) == 3) &&
                 (layoutBox.GetRow(btnNine) == 2 && layoutBox.GetColumn(btnNine) == 0) &&
                 (layoutBox.GetRow(btnTen) == 2 && layoutBox.GetColumn(btnTen) == 1) &&
                 (layoutBox.GetRow(btnEleven) == 2 && layoutBox.GetColumn(btnEleven) == 2) &&
                 (layoutBox.GetRow(btnTwelve) == 2 && layoutBox.GetColumn(btnTwelve) == 3) &&
                 (layoutBox.GetRow(btnThirteen) == 3 && layoutBox.GetColumn(btnThirteen) == 0) &&
                 (layoutBox.GetRow(btnFourteen) == 3 && layoutBox.GetColumn(btnFourteen) == 1) &&
                 (layoutBox.GetRow(btnFifteen) == 3 && layoutBox.GetColumn(btnFifteen) == 2) &&
                 (layoutBox.GetRow(btnSixteen) == 3 && layoutBox.GetColumn(btnSixteen) == 3))
            {
                return 1;
            }
            return 0;
        }
        //functia care muta butoanele
        public void btnMove(Button btn)
        {
            if (canMove(btn) == 1)
            {
                CheckButton(btn, btnSixteen);
            }

        }
        //pentru fiecare buton in parte se va face mutarea (daca e posibil)
        //se actualizeaza mutarile
        //se verifica daca nu s-a incheiat jocul si dcaa s-a si castigat 
        //si se afiseaza mesaj corespunzator in caz afirmativ
        private void btnOne_Click(object sender, EventArgs e)
        {
            btnMove(btnOne);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin()==1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("You won! Yuhuuu :D, and you did it in " + numberOfMoves + "moves.");

            }
        }
        private void btnTwo_Click(object sender, EventArgs e)
        {
            btnMove(btnTwo);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("You won! Yuhuuu :D, and you did it in " + numberOfMoves + "moves.");
            }
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            btnMove(btnThree);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("You won! Yuhuuu :D, and you did it in " + numberOfMoves + "moves.");
            }
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            btnMove(btnFour);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }
        private void btnFive_Click(object sender, EventArgs e)
        {
            btnMove(btnFive);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            btnMove(btnSix);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            btnMove(btnSeven);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            btnMove(btnEight);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            btnMove(btnNine);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnTen_Click(object sender, EventArgs e)
        {
            btnMove(btnTen);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnEleven_Click(object sender, EventArgs e)
        {
            btnMove(btnEleven);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnTwelve_Click(object sender, EventArgs e)
        {
            btnMove(btnTwelve);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnThirteen_Click(object sender, EventArgs e)
        {
            btnMove(btnThirteen);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnFourteen_Click(object sender, EventArgs e)
        {
            btnMove(btnFourteen);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnFifteen_Click(object sender, EventArgs e)
        {
            btnMove(btnFifteen);
            labelMoves.Text = "Moves: " + (++numberOfMoves);
            if (checkWin() == 1)
            {
                btnSixteen.Image = imgarray[3, 3];
                MessageBox.Show("Ai castigat! Yuhuuu :D, si ai facut " + numberOfMoves + "miscari.");
            }
        }

        private void btnSixteen_Click(object sender, EventArgs e)
        {
           
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame(sender, e );
           

        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        public void UpdateInFile()
        {   
            string[] lines = File.ReadAllLines("score.txt");
            List<string> list = new List<string>(lines);

            string datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy, hh:mm:ss tt");
            string new_line = datetime + " Numarul de mutari: " + numberOfMoves.ToString() ;
       

            TimeSpan duration = new TimeSpan();
            
            duration = timetracker.getStopJoc() - timetracker.getStartJoc();

            new_line += "\tTimpul partidei: " + (duration.Minutes).ToString() + " minute si " + (duration.Seconds).ToString() + " secunde.";

            list.Add(new_line);

            File.WriteAllLines("score.txt", list.ToArray());

            
        }
    }
}
