using System;
using System.Windows.Forms;

namespace Petri_Network
{
    /// <summary>
    /// Статус выполнения
    /// </summary>
    public enum Status { Ok, Error, Cancel}

    /// <summary>
    /// Окно редакцирования позиции
    /// </summary>
    public partial class PlaceEditor : Form
    {
        /// <summary>
        /// Позиция для которой открыто меню
        /// </summary>
        public Place Place { get; set; }

        /// <summary>
        /// Возможно ли распознать число в поле для количества фишек
        /// </summary>
        private bool amountok;

        /// <summary>
        /// Возможно ли распознать число в после для задержки
        /// </summary>
        private bool waitok;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="place">Позиция</param>
        public PlaceEditor(Place place)
        {
            InitializeComponent();
            Place = place;
            textBox_amount.Text = place.Amount.ToString();
            textBox_wait.Text = place.Await.ToString();
            amountok = waitok = true;
        }

        /// <summary>
        /// Срабатывает при нажатии кнопки отмена
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Срабатывает при нажатии кнопки ок
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            Place.Amount = uint.Parse(textBox_amount.Text);
            Place.Await = uint.Parse(textBox_wait.Text);
            Close();
        }

        /// <summary>
        /// Срабатывает каждый раз при изменении теста в поле для количества фишек
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void textBox_amount_TextChanged(object sender, EventArgs e)
        {
            uint res;
            amountok = uint.TryParse(textBox_amount.Text, out res);
            button_OK.Enabled = amountok && waitok;
        }

        /// <summary>
        /// Срабатывает каждый раз при изменении задержки
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void textBox_wait_TextChanged(object sender, EventArgs e)
        {
            uint res;
            waitok = uint.TryParse(textBox_wait.Text, out res);
            button_OK.Enabled = amountok && waitok;
        }
    }
}
