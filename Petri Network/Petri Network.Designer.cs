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
            this.инструментToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обычныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перенестиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеПозицииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеПереходаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавлениеДугиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // инструментToolStripMenuItem
            // 
            this.инструментToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обычныйToolStripMenuItem,
            this.перенестиToolStripMenuItem,
            this.добавлениеПозицииToolStripMenuItem,
            this.добавлениеПереходаToolStripMenuItem,
            this.добавлениеДугиToolStripMenuItem});
            this.инструментToolStripMenuItem.Name = "инструментToolStripMenuItem";
            this.инструментToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.инструментToolStripMenuItem.Text = "Инструмент";
            // 
            // обычныйToolStripMenuItem
            // 
            this.обычныйToolStripMenuItem.Name = "обычныйToolStripMenuItem";
            this.обычныйToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.обычныйToolStripMenuItem.Text = "Обычный";
            this.обычныйToolStripMenuItem.Click += new System.EventHandler(this.обычныйToolStripMenuItem_Click);
            // 
            // перенестиToolStripMenuItem
            // 
            this.перенестиToolStripMenuItem.Name = "перенестиToolStripMenuItem";
            this.перенестиToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.перенестиToolStripMenuItem.Text = "Перенести";
            this.перенестиToolStripMenuItem.Click += new System.EventHandler(this.перенестиToolStripMenuItem_Click);
            // 
            // добавлениеПозицииToolStripMenuItem
            // 
            this.добавлениеПозицииToolStripMenuItem.Name = "добавлениеПозицииToolStripMenuItem";
            this.добавлениеПозицииToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.добавлениеПозицииToolStripMenuItem.Text = "Добавление позиции";
            this.добавлениеПозицииToolStripMenuItem.Click += new System.EventHandler(this.добавлениеПозицииToolStripMenuItem_Click);
            // 
            // добавлениеПереходаToolStripMenuItem
            // 
            this.добавлениеПереходаToolStripMenuItem.Name = "добавлениеПереходаToolStripMenuItem";
            this.добавлениеПереходаToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.добавлениеПереходаToolStripMenuItem.Text = "Добавление перехода";
            this.добавлениеПереходаToolStripMenuItem.Click += new System.EventHandler(this.добавлениеПереходаToolStripMenuItem_Click);
            // 
            // добавлениеДугиToolStripMenuItem
            // 
            this.добавлениеДугиToolStripMenuItem.Name = "добавлениеДугиToolStripMenuItem";
            this.добавлениеДугиToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.добавлениеДугиToolStripMenuItem.Text = "Добавление дуги";
            this.добавлениеДугиToolStripMenuItem.Click += new System.EventHandler(this.добавлениеДугиToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(129, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 362);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 253);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Сохранить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // PetriNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PetriNetwork";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
    }
}

