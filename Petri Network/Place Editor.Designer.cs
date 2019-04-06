namespace Petri_Network
{
    partial class PlaceEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_amount = new System.Windows.Forms.TextBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_wait = new System.Windows.Forms.Label();
            this.textBox_wait = new System.Windows.Forms.TextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_amount
            // 
            this.textBox_amount.Location = new System.Drawing.Point(172, 6);
            this.textBox_amount.Name = "textBox_amount";
            this.textBox_amount.Size = new System.Drawing.Size(56, 20);
            this.textBox_amount.TabIndex = 0;
            this.textBox_amount.TextChanged += new System.EventHandler(this.textBox_amount_TextChanged);
            // 
            // label_amount
            // 
            this.label_amount.AutoSize = true;
            this.label_amount.Location = new System.Drawing.Point(57, 9);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(109, 13);
            this.label_amount.TabIndex = 1;
            this.label_amount.Text = "Количество фишек: ";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(150, 71);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "Ок";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_wait
            // 
            this.label_wait.AutoSize = true;
            this.label_wait.Location = new System.Drawing.Point(12, 31);
            this.label_wait.Name = "label_wait";
            this.label_wait.Size = new System.Drawing.Size(154, 13);
            this.label_wait.TabIndex = 3;
            this.label_wait.Text = "Время задержки в секундах:";
            // 
            // textBox_wait
            // 
            this.textBox_wait.Location = new System.Drawing.Point(172, 28);
            this.textBox_wait.Name = "textBox_wait";
            this.textBox_wait.Size = new System.Drawing.Size(56, 20);
            this.textBox_wait.TabIndex = 1;
            this.textBox_wait.TextChanged += new System.EventHandler(this.textBox_wait_TextChanged);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(69, 71);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // PlaceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 106);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.textBox_wait);
            this.Controls.Add(this.label_wait);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.textBox_amount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PlaceEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор позиции";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_amount;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_wait;
        private System.Windows.Forms.TextBox textBox_wait;
        private System.Windows.Forms.Button button_cancel;
    }
}