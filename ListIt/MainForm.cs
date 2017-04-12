using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ListIt
{
    public partial class MainForm : Form
    {
        public static bool LoggedIn = false;
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //remove item
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add item
            listBox1.Items.Insert(0, textBox1.Text);
            textBox1.Clear();
        }
        private void textBox1_Enter(object sender, KeyEventArgs e)
        {
            //add when enter is pressed, instead of clicking "Add" button
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear list from listbox
            listBox1.Items.Clear();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //save file w/ dialog
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName);
                foreach (var item in listBox1.Items)
                    sw.WriteLine(item.ToString());
                sw.Close();
            };
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Open List";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All Files (*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String text;
                try
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName))
                    {
                        text = sr.ReadToEnd();
                        string[] tokens = text.Split('\n');
                        for (int i = 0; i < tokens.Length; i++)
                        {
                            listBox1.Items.Add(tokens[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //print
            printDocument1.PrintPage += new PrintPageEventHandler(this.DrawPage);
            printDialog1.Document = printDocument1;
            DialogResult printResult = printDialog1.ShowDialog(this);
            if (printResult == DialogResult.Cancel)
                return;
            else
                printDocument1.Print();
            printDocument1.Dispose();
        }
        private void DrawPage(object sender, PrintPageEventArgs e)
        {
            //retrieve text from listbox and draw the page
            String text = "";
            foreach (var item in listBox1.Items)
            {
                if (!(item.ToString().Equals("")))
                    text += item.ToString() + "\n";
            }
            int y;
            Font font = new Font("Times New Roman", 16);
            y = e.MarginBounds.Y;
            e.Graphics.DrawString(text, font, Brushes.Black, e.MarginBounds.X, y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //logging out
            label1.Text = "Working locally";
            button7.Show();
            button8.Hide();
        }
    }
}
