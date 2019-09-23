namespace Petri_Network
{
    partial class PetriNetwork
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обычныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перенестиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеПозицииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеПереходаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеДугиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьДугуПоВершинамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AddZoom = new System.Windows.Forms.Button();
            this.MinusZoom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new Petri_Network.MyPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(789, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.toolStripSeparator1,
            this.выйтиToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // выйтиToolStripMenuItem
            // 
            this.выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            this.выйтиToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.выйтиToolStripMenuItem.Text = "Выйти";
            this.выйтиToolStripMenuItem.Click += new System.EventHandler(this.выйтиToolStripMenuItem_Click);
            // 
            // инструментToolStripMenuItem
            // 
            this.инструментToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обычныйToolStripMenuItem,
            this.перенестиToolStripMenuItem,
            this.добавлениеПозицииToolStripMenuItem,
            this.добавлениеПереходаToolStripMenuItem,
            this.добавлениеДугиToolStripMenuItem,
            this.выбратьДугуПоВершинамToolStripMenuItem});
            this.инструментToolStripMenuItem.Name = "инструментToolStripMenuItem";
            this.инструментToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.инструментToolStripMenuItem.Text = "Инструмент";
            // 
            // обычныйToolStripMenuItem
            // 
            this.обычныйToolStripMenuItem.Name = "обычныйToolStripMenuItem";
            this.обычныйToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.обычныйToolStripMenuItem.Text = "Обычный";
            this.обычныйToolStripMenuItem.Click += new System.EventHandler(this.обычныйToolStripMenuItem_Click);
            // 
            // перенестиToolStripMenuItem
            // 
            this.перенестиToolStripMenuItem.Name = "перенестиToolStripMenuItem";
            this.перенестиToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.перенестиToolStripMenuItem.Text = "Перенести";
            this.перенестиToolStripMenuItem.Click += new System.EventHandler(this.перенестиToolStripMenuItem_Click);
            // 
            // добавлениеПозицииToolStripMenuItem
            // 
            this.добавлениеПозицииToolStripMenuItem.Name = "добавлениеПозицииToolStripMenuItem";
            this.добавлениеПозицииToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.добавлениеПозицииToolStripMenuItem.Text = "Добавление позиции";
            this.добавлениеПозицииToolStripMenuItem.Click += new System.EventHandler(this.добавлениеПозицииToolStripMenuItem_Click);
            // 
            // добавлениеПереходаToolStripMenuItem
            // 
            this.добавлениеПереходаToolStripMenuItem.Name = "добавлениеПереходаToolStripMenuItem";
            this.добавлениеПереходаToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.добавлениеПереходаToolStripMenuItem.Text = "Добавление перехода";
            this.добавлениеПереходаToolStripMenuItem.Click += new System.EventHandler(this.добавлениеПереходаToolStripMenuItem_Click);
            // 
            // добавлениеДугиToolStripMenuItem
            // 
            this.добавлениеДугиToolStripMenuItem.Name = "добавлениеДугиToolStripMenuItem";
            this.добавлениеДугиToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.добавлениеДугиToolStripMenuItem.Text = "Добавление дуги";
            this.добавлениеДугиToolStripMenuItem.Click += new System.EventHandler(this.добавлениеДугиToolStripMenuItem_Click);
            // 
            // выбратьДугуПоВершинамToolStripMenuItem
            // 
            this.выбратьДугуПоВершинамToolStripMenuItem.Name = "выбратьДугуПоВершинамToolStripMenuItem";
            this.выбратьДугуПоВершинамToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.выбратьДугуПоВершинамToolStripMenuItem.Text = "Выбрать дугу по вершинам";
            this.выбратьДугуПоВершинамToolStripMenuItem.Click += new System.EventHandler(this.выбратьДугуПоВершинамToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AddZoom
            // 
            this.AddZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddZoom.Location = new System.Drawing.Point(754, 425);
            this.AddZoom.Name = "AddZoom";
            this.AddZoom.Size = new System.Drawing.Size(23, 23);
            this.AddZoom.TabIndex = 7;
            this.AddZoom.Text = "+";
            this.AddZoom.UseVisualStyleBackColor = true;
            this.AddZoom.Click += new System.EventHandler(this.AddZoom_Click);
            // 
            // MinusZoom
            // 
            this.MinusZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MinusZoom.Location = new System.Drawing.Point(694, 425);
            this.MinusZoom.Name = "MinusZoom";
            this.MinusZoom.Size = new System.Drawing.Size(23, 23);
            this.MinusZoom.TabIndex = 8;
            this.MinusZoom.Text = "-";
            this.MinusZoom.UseVisualStyleBackColor = true;
            this.MinusZoom.Click += new System.EventHandler(this.MinusZoom_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(723, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "100";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Сеть петри|*.json|Все файлы|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Сеть петри|*.json|Все файлы|*.*";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.AutoScroll = true;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(765, 392);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pictureBox1_Scroll);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // PetriNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinusZoom);
            this.Controls.Add(this.AddZoom);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PetriNetwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petri Networks";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инструментToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обычныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перенестиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавлениеПереходаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавлениеПозицииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавлениеДугиToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem выбратьДугуПоВершинамToolStripMenuItem;
        private MyPanel pictureBox1;
        private System.Windows.Forms.Button AddZoom;
        private System.Windows.Forms.Button MinusZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

