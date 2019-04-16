namespace Petri_Network
{
    /// <summary>
    /// Класс для корректрого отображения текста
    /// </summary>
    public class MyNumericUpDown : System.Windows.Forms.NumericUpDown
    {
        /// <summary>
        /// Происходит обновления текста
        /// </summary>
        protected override void UpdateEditText()
        {
            Text = System.Convert.ToInt64(base.Value).ToString();
        }
    }
}
