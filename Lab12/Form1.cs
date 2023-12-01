namespace Lab12
{

    public partial class Form1 : Form
    {
        private Point PreviousPoint, point;
        private Bitmap bmp;
        private Pen blackPen;
        private Graphics g;
        private int[,] originalR;
        private int[,] originalG;
        private int[,] originalB;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            blackPen = new Pen(Color.Black, 4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RestoreOriginalColors();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int G = bmp.GetPixel(i, j).G;
                    Color p = Color.FromArgb(255, 0, G, 0);
                    bmp.SetPixel(i, j, p);
                }
            }

            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP,*.JPG," + ".*GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                int maxWidth = pictureBox1.Width;
                int maxHeight = pictureBox1.Height;
                float aspectRatio = (float)image.Width / image.Height;
                if (image.Width > image.Height)
                {
                    maxWidth = pictureBox1.Width;
                    maxHeight = (int)Math.Floor(maxWidth / aspectRatio);
                }
                else
                {
                    maxHeight = pictureBox1.Height;
                    maxWidth = (int)Math.Floor(maxHeight * aspectRatio);
                }

                // Установка размеров PictureBox
                pictureBox1.Width = maxWidth;
                pictureBox1.Height = maxHeight;


                bmp = new Bitmap(image, maxWidth, maxHeight);
                pictureBox1.Image = bmp;
                g = Graphics.FromImage(pictureBox1.Image);
                SaveOriginalColors();
            }
        }
        private void SaveOriginalColors()
        {
            originalR = new int[bmp.Width, bmp.Height];
            originalG = new int[bmp.Width, bmp.Height];
            originalB = new int[bmp.Width, bmp.Height];

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    originalR[i, j] = bmp.GetPixel(i, j).R;
                    originalG[i, j] = bmp.GetPixel(i, j).G;
                    originalB[i, j] = bmp.GetPixel(i, j).B;
                }
            }
        }

        private void RestoreOriginalColors()
        {
            if (originalR != null && originalG != null && originalB != null)
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        Color originalColor = Color.FromArgb(255, originalR[i, j], originalG[i, j], originalB[i, j]);
                        bmp.SetPixel(i, j, originalColor);
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            RestoreOriginalColors();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int R = bmp.GetPixel(i, j).R;
                    Color p = Color.FromArgb(255, R, 0, 0);
                    bmp.SetPixel(i, j, p);
                }
            }

            pictureBox1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RestoreOriginalColors();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int d = bmp.GetPixel(i, j).B;
                    Color p = Color.FromArgb(255, 0, 0, d);
                    bmp.SetPixel(i, j, p);
                }
            }
            pictureBox1.Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RestoreOriginalColors();
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
                "Bitmap File (*.bmp)|*.bmp|" +
                "Gif File(*.gif)|*.gif|" +
                "Jpeg File(*.jpg)|*.jpg|" +
                "PNG File(*.png)|*.png";
            if(savedialog.ShowDialog() == DialogResult.OK)
            {
                string filename = savedialog.FileName;
                string strFilExtn = filename.Remove(0, filename.Length - 3);
                switch(strFilExtn)
                {
                    case "bmp":
                        bmp.Save(filename,System.Drawing.Imaging.ImageFormat.Bmp); 
                        break;
                    case "jpg":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "png":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        MessageBox.Show("ЭЭЭ ОШИБКА");
                        break;

                }
            }
        }
    }
}