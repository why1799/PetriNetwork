using System;
using System.Windows.Forms;

namespace Petri_Network
{
    /// <summary>
    /// Представляет регулятор Windows (также известный как элемент управления "вверх-вниз"), отображающий числовые значения.
    /// </summary>
    public class MyNumericUpDown : NumericUpDown
    {
        /// <summary>
        /// значение, назначенное регулятору (также известному как элемент управления "вверх-вниз").
        /// </summary>
        private int _value;

        /// <summary>
        /// Возвращает или задает значение, назначенное регулятору (также известному как элемент управления "вверх-вниз").
        /// </summary>
        public new int Value {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                UpdateEditText();
                if (ValueChanged != null)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs);
                }
            }
        }

        /// <summary>
        /// Возвращает или задает максимальное значение для регулятора (также известного как элемент управления "вверх-вниз").
        /// </summary>
        public new int Maximum { get; set; }

        /// <summary>
        /// Возвращает или задает минимальное значение для регулятора (также известного как элемент управления "вверх-вниз").
        /// </summary>
        public new int Minimum { get; set; }

        /// <summary>
        /// Происходит при Value свойства было изменено иным образом.
        /// </summary>
        public new event EventHandler ValueChanged;

        /// <summary>
        /// Отображает текущее значение регулятора (который также называется элементом управления "вверх-вниз") в соответствующем формате.
        /// </summary>
        protected override void UpdateEditText()
        {
            Text = Value.ToString();
        }

        /// <summary>
        /// Увеличивает значение регулятора (также называемого элементом управления "вверх-вниз").
        /// </summary>
        public override void UpButton()
        {
            if (Value + 1 <= Maximum)
            {
                Value++;
            }
        }

        /// <summary>
        /// Уменьшает значение регулятора (также называемого элементом управления "вверх-вниз").
        /// </summary>
        public override void DownButton()
        {
            if (Value - 1 >= Minimum)
            {
                Value--;
            }
        }

        /// <summary>
        /// Вызывается при изменении текста
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnTextChanged(EventArgs e)
        {
            int val;
            if(int.TryParse(Text, out val))
            {
                _value = val;
            }
            else
            {
                _value = 0;
            }
            if (ValueChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
    }
}
