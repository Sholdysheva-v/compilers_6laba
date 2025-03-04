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

namespace Лабораторная_1_компиляторы
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; // Текущий путь к файлу
        private bool isTextChanged = false; // Флаг, указывающий на изменения в тексте
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
        }

        // Обновление заголовка окна в зависимости от текущего файла
        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(currentFilePath) ? "Компилятор" : Path.GetFileName(currentFilePath) ;
        }
        // Подтверждение сохранения изменений перед выходом или открытием нового файла
        private bool ConfirmSaveChanges()
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                return result != DialogResult.Cancel;
            }
            return true;
        }

        // Сохранение файла
        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs(); // Если путь пуст, вызываем "Сохранить как"
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                isTextChanged = false;
            }
        }

        // Сохранение файла под другим именем
        private void SaveFileAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                    currentFilePath = saveFileDialog.FileName;
                    isTextChanged = false;
                    UpdateTitle();
                }
            }
        }

        // Открытие файла
        private void OpenFile()
        {
            if (ConfirmSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        isTextChanged = false;
                        UpdateTitle();
                    }
                }
            }
        }
        private void NewFile()
        {
            if (ConfirmSaveChanges())
            {
                richTextBox1.Clear();
                currentFilePath = string.Empty;
                isTextChanged = false;
                UpdateTitle();
            }
        }

        // Выход из приложения
        private void ExitApplication()
        {
            if (ConfirmSaveChanges())
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}
