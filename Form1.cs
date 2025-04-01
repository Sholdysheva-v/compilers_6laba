using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_1_компиляторы
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; 
        private bool isTextChanged = false;
        //private Panel lineNumberPanel; // Панель для отображения номеров строк
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeFontSizeComboBox();
            //InitializeLineNumberPanel(); 
        }

        //private void AnalyzeCode()
        //{
        //    dataGridView1.Rows.Clear(); // Очищаем таблицу перед новым анализом

        //    string code = richTextBox1.Text;
        //    if (string.IsNullOrEmpty(code))
        //        return;

        //    // Определяем шаблоны для лексем
        //    var tokenPatterns = new Dictionary<string, string>
        //    {
        //        { "Keyword", @"\b(struct|string|int|bool|void|char|double|float|long|short|byte)\b" },
        //        { "Identifier", @"\$?[a-zA-Z_][a-zA-Z0-9_]*" },
        //        { "Integer", @"\b\d+\b" },
        //        { "Operator", @"[+\-*/%=!<>]=?|&&|\|\||!" },
        //        { "Separator", @"[(),;:.{}]" },
        //        { "Whitespace", @"\s+" },
        //        { "Assignment", @"=" },
        //        { "Comment", @"//.*|/\*.*?\*/" },
        //        { "StringLiteral", @"""[^""]*""|'[^']*'" }
        //    };

        //    // Общий шаблон для всех лексем
        //    string allPatterns = string.Join("|", tokenPatterns.Values);
        //    var regex = new Regex(allPatterns);

        //    int lineNumber = 1;
        //    int position = 0;

        //    foreach (Match match in regex.Matches(code))
        //    {
        //        // Пропускаем пробелы и комментарии
        //        if (Regex.IsMatch(match.Value, tokenPatterns["Whitespace"]))
        //        {
        //            position += match.Length;
        //            // Учитываем переходы на новую строку
        //            lineNumber += match.Value.Split('\n').Length - 1;
        //            continue;
        //        }
        //        if (Regex.IsMatch(match.Value, tokenPatterns["Comment"]))
        //        {
        //            position += match.Length;
        //            continue;
        //        }

        //        // Определяем тип лексемы
        //        string tokenType = "Invalid";
        //        int tokenCode = 0;

        //        foreach (var pattern in tokenPatterns)
        //        {
        //            if (Regex.IsMatch(match.Value, $"^{pattern.Value}$") && pattern.Key != "Whitespace" && pattern.Key != "Comment")
        //            {
        //                tokenType = pattern.Key;
        //                break;
        //            }
        //        }

        //        // Устанавливаем условные коды для типов лексем
        //        switch (tokenType)
        //        {
        //            case "Keyword":
        //                tokenCode = 14;
        //                break;
        //            case "Identifier":
        //                tokenCode = 2;
        //                break;
        //            case "Integer":
        //                tokenCode = 1;
        //                break;
        //            case "Operator":
        //                tokenCode = 12;
        //                break;
        //            case "Separator":
        //                tokenCode = 16;
        //                if (match.Value == ";") tokenCode = 16; // конец оператора
        //                else if (match.Value == "{") tokenCode = 17; // начало блока
        //                else if (match.Value == "}") tokenCode = 18; // конец блока
        //                break;
        //            case "Assignment":
        //                tokenCode = 10;
        //                break;
        //            case "StringLiteral":
        //                tokenCode = 3;
        //                break;
        //            default:
        //                tokenCode = 99; // недопустимый символ
        //                break;
        //        }

        //        // Добавляем строку в таблицу
        //        dataGridView1.Rows.Add(
        //            tokenCode,
        //            GetRussianTokenType(tokenType),
        //            match.Value,
        //            $"Строка {lineNumber}, позиция {position + 1}-{position + match.Length}"
        //        );

        //        position += match.Length;
        //    }
        //}

        //private string GetRussianTokenType(string englishType)
        //{
        //    switch (englishType)
        //    {
        //        case "Keyword": return "ключевое слово";
        //        case "Identifier": return "идентификатор";
        //        case "Integer": return "целое число";
        //        case "Operator": return "оператор";
        //        case "Separator": return "разделитель";
        //        case "Assignment": return "оператор присваивания";
        //        case "StringLiteral": return "строковый литерал";
        //        case "Invalid": return "недопустимый символ";
        //        default: return englishType;
        //    }
        //}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }

        
        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(currentFilePath) ? "Компилятор" : Path.GetFileName(currentFilePath) ;
        }
        
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
            SaveFile();
        }
        private void InitializeFontSizeComboBox()
        {
            // Добавляем размеры шрифта в комбобокс
            comboBoxFontSize.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });

            // Устанавливаем начальный размер шрифта
            comboBoxFontSize.SelectedIndex = 4; // Например, 12

            // Подписываемся на событие изменения выбора
            comboBoxFontSize.SelectedIndexChanged += ComboBoxFontSize_SelectedIndexChanged;
        }

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный размер шрифта
            if (comboBoxFontSize.SelectedItem != null)
            {
                int fontSize = (int)comboBoxFontSize.SelectedItem;

                // Применяем размер шрифта к richTextBox1 и richTextBox2
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize);
                //richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, fontSize);
            }
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyzeCode();
        }

        private void AnalyzeCode()
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            var tokens = analyzer.Analyze(richTextBox1.Text);

            StringBuilder result = new StringBuilder();
            foreach (var token in tokens)
            {
                result.AppendLine(token.ToString());
            }

            richTextBox2.Text = result.ToString();
        }
    }
}
