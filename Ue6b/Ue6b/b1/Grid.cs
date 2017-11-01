using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace b1 {
    public class Grid {

        Graphics graphics;

        public Cell[,] Cells { get; set; }
        public int Size;
        public Brush AliveColor = Brushes.Black;
        public Brush DeadColor = Brushes.White;

        public Grid(int length, int size, PictureBox pictureBox) {
            Cells = new Cell[length, length];
            Size = size;
            graphics = pictureBox.CreateGraphics();
        }

        public void Initialize() {
            Random random = new Random();
            random.Next(0, 1);
            for (int i = 0; i < Cells.GetLength(0); ++i) {
                for (int j = 0; j < Cells.GetLength(1); ++j) {
                    bool b = random.Next(0, 2) == 0 ? false : true;
                    Cells[i, j] = new Cell(b);
                }
            }
        }

        public void Clear() {
            for (int i = 0; i < Cells.GetLength(0); ++i) {
                for (int j = 0; j < Cells.GetLength(1); ++j) {
                    Cells[i, j] = new Cell(false);
                }
            }
        }

        public void Draw() {
            for (int i = 0; i < Cells.GetLength(0); ++i) {
                for (int j = 0; j < Cells.GetLength(1); ++j) {
                    if (Cells[i, j].IsAlive) {
                        graphics.FillRectangle(AliveColor, i * Size + 1, j * Size + 1, Size - 1, Size - 1);
                    } else {
                        graphics.FillRectangle(DeadColor, i * Size + 1, j * Size + 1, Size - 1, Size - 1);
                    }
                }
            }
        }

        public void Draw(int row, int col) {
            if (Cells[row, col].IsAlive) {
                graphics.FillRectangle(AliveColor, row * 12 + 1, col * 12 + 1, Size - 1, Size - 1);
            } else {
                graphics.FillRectangle(DeadColor, row * 12 + 1, col * 12 + 1, Size - 1, Size - 1);
            }
        }

        public void Update() {
            Cell[,] cache = new Cell[50, 50];
            //Cell[,] caches = Cells; // reference!

            for (int i = 0; i < Cells.GetLength(0); ++i) {
                for (int j = 0; j < Cells.GetLength(1); ++j) {
                    bool isAlive = Cells[i, j].IsAlive;
                    int livingNeighbors = GetLivingNeighbors(i, j);
                    bool result = false;

                    if (isAlive && (livingNeighbors == 2 || livingNeighbors == 3)) {
                        result = true;
                    } else if (!isAlive && livingNeighbors == 3) {
                        result = true;
                    }

                    cache[i, j] = new Cell(result);

                }
            }
            Cells = cache;
        }

        public void WriteToFile() {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Cells.GetLength(0); ++i) {
                for (int j = 0; j < Cells.GetLength(1); ++j) {
                    if (Cells[i, j].IsAlive) {
                        stringBuilder.Append('1');
                    } else {
                        stringBuilder.Append('0');
                    }
                }
                stringBuilder.Append('\n');
            }
            string file;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                file = saveFileDialog.FileName;
                StreamWriter streamWriter = new StreamWriter(file);
                streamWriter.WriteLine(stringBuilder.ToString());
                streamWriter.Close();
            }
        }

        public void LoadFromFile() {
            string file;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                file = openFileDialog.FileName;
                StreamReader streamReader = new StreamReader(file);
                int row = 0;
                int col = 0;
                while (!streamReader.EndOfStream) {
                    char c = (char)streamReader.Read();
                    if (c == '0') {
                        Cells[row, col].IsAlive = false;
                        ++col;
                    } else if (c == '1') {
                        Cells[row, col].IsAlive = true;
                        ++col;
                    } else if (c == '\n') {
                        ++row;
                        col = 0;
                    }
                }
            }
        }

        private int GetLivingNeighbors(int row, int col) {
            int livingNeighbors = 0;
            int size = Cells.GetLength(0);

            if (row != size - 1) { // right
                if (Cells[row + 1, col].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (row != size - 1 && col != size - 1) { // bottom right
                if (Cells[row + 1, col + 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (col != size - 1) { // bottom
                if (Cells[row, col + 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (row != 0 && col != size - 1) { // bottom left
                if (Cells[row - 1, col + 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (row != 0) { // left
                if (Cells[row - 1, col].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (row != 0 && col != 0) { // top left
                if (Cells[row - 1, col - 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (col != 0) { // top
                if (Cells[row, col - 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            if (row != size - 1 && col != 0) { // top right
                if (Cells[row + 1, col - 1].IsAlive) {
                    ++livingNeighbors;
                }
            }
            return livingNeighbors;
        }
    
    }
}
