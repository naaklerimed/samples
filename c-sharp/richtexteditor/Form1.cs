using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUIFinal
{
    
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog1;
        bool fileOpened = false;
        int currentLine = 0;
        int currentColumn = 0;
        const int WM_USER = 0x400;
        const int EM_GETSCROLLPOS = (WM_USER + 221);
        const int EM_SETSCROLLPOS = (WM_USER + 222);
        const int EM_LINESCROLL = 0x00B6;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);
        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar,
                                       int nPos, bool bRedraw);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg,
                                       int wParam, int lParam);
        public Form1()
        {
            InitializeComponent();
            
        }

        //open button        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fileOpened = true;
                           StreamReader reader = new StreamReader(myStream);
                            string text = reader.ReadToEnd();
                            richTextBox1.Text = text;
                            int index = richTextBox1.SelectionStart;
                            int currentLine = richTextBox1.GetLineFromCharIndex(index);

                            int firstChar = richTextBox1.GetFirstCharIndexFromLine(currentLine);
                            int currentColumn = index - firstChar;
                            
                            textBox2.Text = "File Name: " + openFileDialog1.FileName + "     Line:" + currentLine + "     Col:" + currentColumn;
 
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        //saving operation
        private void Save()
        {
            if (!fileOpened)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save As";
                dlg.Filter = "All Files *|*.txt";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText);
                }
            }
            else
            {
                richTextBox1.SaveFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                string Filename = openFileDialog1.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            string Filename = openFileDialog1.FileName;
        }

        private void SaveAs()
        {
            
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save As";
                dlg.Filter = "All Files *|*.txt";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText);
                }
            
            
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save As";
                dlg.Filter = "All Files *|*.txt";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText);
                }

        }

        
        //other simple ops like exit,undo,redo, copy etc.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.Show(this);
        }

        

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fileOpened = true;
                            StreamReader reader = new StreamReader(myStream);
                            string text = reader.ReadToEnd();
                            richTextBox1.Text = text;
                            int index = richTextBox1.SelectionStart;
                            int currentLine = richTextBox1.GetLineFromCharIndex(index);

                            int firstChar = richTextBox1.GetFirstCharIndexFromLine(currentLine);
                            int currentColumn = index - firstChar;
                            
                            textBox2.Text = "File Name: " + openFileDialog1.FileName + "     Line:" + currentLine + "     Col:" + currentColumn;
                            int lineNumber = richTextBox1.Lines.Length;
                            string suffixes = "";
                            for (int i = 0; i < lineNumber; i++)
                            {
                                suffixes = suffixes + "=====" + Environment.NewLine;
                            }
                            richTextBox2.Text = suffixes;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!fileOpened)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save As";
                dlg.Filter = "All Files *|*.txt";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText);
                }
            }
            else
            {
                richTextBox1.SaveFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                string Filename = openFileDialog1.FileName;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Font BoldFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font RegularFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (richTextBox1.SelectionFont.Bold)
            {
                richTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                richTextBox1.SelectionFont = BoldFont;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Font ItalicFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font RegularFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (richTextBox1.SelectionFont.Italic)
            {
                richTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                richTextBox1.SelectionFont = ItalicFont;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Font UnderlinedFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Underline);
            Font RegularFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (richTextBox1.SelectionFont.Underline)
            {
                richTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                richTextBox1.SelectionFont = UnderlinedFont;
            }
        }

        private void richTextBox1_CursorChanged(object sender, EventArgs e)
        {
            if(fileOpened)
            {
                int index = richTextBox1.SelectionStart;
                int currentLine = richTextBox1.GetLineFromCharIndex(index);

                int firstChar = richTextBox1.GetFirstCharIndexFromLine(currentLine);
                int currentColumn = index - firstChar;
                textBox2.Text = "File Name: " + openFileDialog1.FileName + " Line:" + currentLine + " Col:" + currentColumn;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Save") { Save(); }
            else if (textBox1.Text == "Save as") { SaveAs(); }
            else if (textBox1.Text == "Open") { Open(); }
            else if (textBox1.Text.Contains("Find")) { string n = textBox1.Text.ToString(); Find(n); }
            else if (textBox1.Text.Substring(0, 1) == "n") { string n1 = textBox1.Text.ToString(); n(n1); }
            else if (textBox1.Text.Contains("Up")) { string n = textBox1.Text.ToString(); up_n(n); }
            else if (textBox1.Text.Contains("Down")) { string n = textBox1.Text.ToString(); down_n(n); }
            else if (textBox1.Text == "Left") { Lefty(); }
            else if (textBox1.Text == "Right") { Righty(); }
            else if (textBox1.Text.Contains("Change")) { string n = textBox1.Text.ToString(); Change(n); }
            else if (textBox1.Text.Substring(0,5) == "Setcl") { string n1 = textBox1.Text.ToString(); SetclN(n1); }

        }
        //special features
        private void SetclN(string n1)
        {
            string[] n = n1.Split(' ');

            SetScrollPos(richTextBox1.Handle, 1, -1000000, true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, -1000000);
            SetScrollPos(richTextBox1.Handle, 1, Convert.ToInt32(n[1]), true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, Convert.ToInt32(n[1]));
            richTextBox1.SelectionStart = Convert.ToInt32(n[1]);

            textBox2.Text = "File Name: " + openFileDialog1.FileName + "     Line:" + richTextBox1.SelectionStart + "     Col:" + currentColumn;

            
        }

        private void Change(string n)
        {
            string n1 = n.Split('/').ElementAt(1);
            string n2 = n.Split('/').ElementAt(2);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace(n1, n2);
        }
        
        private void Righty()
        {
            Point point = default(Point);
            Point point2 = default(Point);
            SendMessage(richTextBox1.Handle, EM_GETSCROLLPOS, 0, ref point);
            point2.X = point.X + 60;
            point2.Y = point.Y;
            SendMessage(richTextBox1.Handle, EM_SETSCROLLPOS, 0, ref point2);
        }

        private void Lefty()
        {
            Point point = default(Point);
            Point point2 = default(Point);
            SendMessage(richTextBox1.Handle, EM_GETSCROLLPOS, 0, ref point);
            point2.X = point.X - 60;
            point2.Y = point.Y;
            SendMessage(richTextBox1.Handle, EM_SETSCROLLPOS, 0, ref point2);
        }

        private void down_n(string n)
        {
            n = n.Split(' ').Last();
            SetScrollPos(richTextBox1.Handle, 1, Convert.ToInt32(n), true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, Convert.ToInt32(n));
        }

        private void up_n(string n)
        {
            n = n.Split(' ').Last();
            SetScrollPos(richTextBox1.Handle, 1, -Convert.ToInt32(n), true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, -Convert.ToInt32(n));
        }

        private void Find(string n)
        {
            string[] word = n.Split(' ');
            int s_start = richTextBox1.SelectionStart, startIndex = 0, index;
            while ((index = richTextBox1.Text.IndexOf(word[1], startIndex)) != -1)
            {
                richTextBox1.Select(index, word[1].Length);
                richTextBox1.SelectionColor = Color.Red;
                startIndex = index + word[1].Length;
            }
            richTextBox1.SelectionStart = s_start;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.Focus();
        }

        private void n(string n1)
        {
            int lineNumber = 0;
            lineNumber = Convert.ToInt32(n1.Substring(1, n1.Length - 1));
            SetScrollPos(richTextBox1.Handle, 1, -1000000, true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, -1000000);
            SetScrollPos(richTextBox1.Handle, 1, lineNumber, true);
            SendMessage(richTextBox1.Handle, EM_LINESCROLL, 0, lineNumber);
            richTextBox1.SelectionStart = lineNumber;
            
            textBox2.Text = "File Name: " + openFileDialog1.FileName + "     Line:" + richTextBox1.SelectionStart + "     Col:" + currentColumn;

            
        }
        

        
        private void Open()
        {
            Stream myStream = null;
            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fileOpened = true;
                            StreamReader reader = new StreamReader(myStream);
                            string text = reader.ReadToEnd();
                            richTextBox1.Text = text;
                            int index = richTextBox1.SelectionStart;
                            int currentLine = richTextBox1.GetLineFromCharIndex(index);

                            int firstChar = richTextBox1.GetFirstCharIndexFromLine(currentLine);
                            int currentColumn = index - firstChar;

                            textBox2.Text = "File Name: " + openFileDialog1.FileName + "     Line:" + currentLine + "     Col:" + currentColumn;
                            int lineNumber = richTextBox1.Lines.Length;
                            string suffixes = "";
                            for (int i = 0; i < lineNumber; i++)
                            {
                                suffixes = suffixes + "=====" + Environment.NewLine;
                            }
                            richTextBox2.Text = suffixes;

                        }
                    }
                }
                

                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                
            }   
        }

        

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //check suffix here
        }
        
        
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
        
      
            int WM_USER = 1024;
            int EM_GETSCROLLPOS = WM_USER + 221;
            int EM_SETSCROLLPOS = WM_USER + 222;

            Point pt = new Point();

            SendMessage(richTextBox1.Handle, EM_GETSCROLLPOS, 0, ref pt);
            SendMessage(richTextBox2.Handle, EM_SETSCROLLPOS, 0, ref pt);
        }

   
        
    }
}

