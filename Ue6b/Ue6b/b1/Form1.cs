using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace b1 {
	public partial class MainForm : Form {


		Grid Grid;
        System.Windows.Forms.Timer timerInit = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerRun = new System.Windows.Forms.Timer();
		public MainForm() {
			InitializeComponent();
            Grid = new Grid(50, 12, pictureBox);
            timerInit.Interval = 1;
            timerInit.Tick += timerInit_Tick;
            timerInit.Start();
            timerRun.Interval = 100;
            timerRun.Tick += timerRun_Tick;
		}

        private void timerInit_Tick(object sender, EventArgs e) {
            Grid.Initialize();
            Grid.Draw();
            timerInit.Stop();
        }

        private void timerRun_Tick(object sender, EventArgs e) {
            Grid.Update();
            Grid.Draw();
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e) {
            int row = e.X / 12;
            int col = e.Y / 12;

            Grid.Cells[row, col].IsAlive = !Grid.Cells[row, col].IsAlive;
            Grid.Draw(row, col);
        }

		private void buttonClear_Click(object sender, EventArgs e) {
            timerRun.Stop();
            Grid.Clear();
            Grid.Draw();
		}

		private void buttonReset_Click(object sender, EventArgs e) {
            timerRun.Stop();
            Grid.Initialize();
            Grid.Draw();
        }

        private void buttonRun_Click(object sender, EventArgs e) {
            timerRun.Start();
        }

        private void buttonStep_Click(object sender, EventArgs e) {
            timerRun.Stop();
            Grid.Update();
            Grid.Draw();
        }

        private void buttonStop_Click(object sender, EventArgs e) {
            timerRun.Stop();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            Grid.WriteToFile();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            Grid.LoadFromFile();
            Grid.Draw();
        }
       
	}
}
