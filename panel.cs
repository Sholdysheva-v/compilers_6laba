using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_1_компиляторы
{
    internal class panel
    {
       
        //private void pictureBox9_Click(object sender, EventArgs e)
        //{
        //    AnalyzeCode();
        //}
        //private void InitializeLineNumberPanel()
        //{
        //    //// Создаем панель для отображения номеров строк
        //    //lineNumberPanel = new Panel
        //    //{
        //    //    Dock = DockStyle.Left,
        //    //    Width = 30, // Ширина панели
        //    //    BackColor = Color.LightGray,
        //    //    AutoScroll = true
        //    //}; splitContainer2.Panel1

        //    //richTextBox1.Controls.Add(lineNumberPanel);
        //    //lineNumberPanel.Location = new Point(0, 0); // Позиционируем панель в левом верхнем углу RichTextBox1
        //    //lineNumberPanel.Height = richTextBox1.Height; // Высота панели равна высоте RichTextBox1
        //    //splitContainer2.Panel1.Width = 20; // Высота панели равна высоте RichTextBox1

        //    //// Добавляем панель на форму
        //    //this.Controls.Add(lineNumberPanel);
        //    //lineNumberPanel.BringToFront();

        //    // Подписываемся на событие изменения текста
        //    richTextBox1.TextChanged += RichTextBox1_TextChanged;
        //    richTextBox1.VScroll += RichTextBox1_VScroll;
        //    richTextBox1.Resize += RichTextBox1_Resize;

        //    // Обновляем нумерацию строк
        //    UpdateLineNumbers();
        //}
        private void RichTextBox1_Resize(object sender, EventArgs e)
        {
            // При изменении размера RichTextBox1 обновляем размер панели
            //lineNumberPanel.Height = richTextBox1.Height;
            //UpdateLineNumbers();
        }

        //private void RichTextBox1_VScroll(object sender, EventArgs e)
        //{
        //    // Обновляем нумерацию строк при прокрутке
        //    UpdateLineNumbers();
        //}

        //private void RichTextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    // Обновляем нумерацию строк при изменении текста
        //    UpdateLineNumbers();
        //}

        //private void UpdateLineNumbers()
        //{
        //    // Очищаем панель
        //    lineNumberPanel.Controls.Clear();

        //    // Получаем количество строк
        //    int lineCount = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;

        //    // Получаем высоту одной строки
        //    int lineHeight = TextRenderer.MeasureText("а", richTextBox1.Font).Height;

        //    // Создаем метки для каждой строки
        //    for (int i = 1; i <= lineCount; i++)
        //    {
        //        Label lineLabel = new Label
        //        {
        //            Text = i.ToString(),
        //            Width = lineNumberPanel.Width - 5,
        //            TextAlign = ContentAlignment.TopRight,
        //            ForeColor = Color.Black,
        //            Font = richTextBox1.Font // Используем тот же шрифт, что и в RichTextBox
        //        };

        //        // Позиционируем метку
        //        int y = (i - 1) * lineHeight; // Вычисляем позицию Y для каждой строки
        //        lineLabel.Location = new Point(0, y);

        //        // Добавляем метку на панель
        //        lineNumberPanel.Controls.Add(lineLabel);
        //    }

        //    // Обновляем высоту панели, чтобы вместить все строки
        //    lineNumberPanel.Height = lineCount * lineHeight;
        //}
    }
}
