﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Petri_Network
{
    /// <summary>
    /// Класс для редактирования дуг
    /// </summary>
    public partial class EditLink : Form
    {
        /// <summary>
        /// Доступные дуги
        /// </summary>
        private List<Link> links;

        /// <summary>
        /// Класс сетей Петри, в которых обновляется picturebox
        /// </summary>
        private PetriNetwork petri;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="links">Доступные дуги</param>
        /// <param name="petri">Класс сетей Петри, откуда вызывался конструктор</param>
        public EditLink(List<Link> links, PetriNetwork petri)
        {
            InitializeComponent();
            this.links = links;
            this.petri = petri;

            foreach (var link in links)
            {
                if(link.FromPlace)
                {
                    comboBox1.Items.Add("Дуга ведущая в переход");
                }
                else
                {
                    comboBox1.Items.Add("Дуга ведущая в позицию");
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Событие, которое вызывается при изменение значения изогнутости
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            links[comboBox1.SelectedIndex].Сurvature = numericUpDown1.Value;
            petri.PictureboxIndalidate();
        }

        /// <summary>
        /// Событие, которые вызывается при выборе другой дуги
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = links[comboBox1.SelectedIndex].Сurvature;
            petri.ChangingLink = links[comboBox1.SelectedIndex];
            petri.PictureboxIndalidate();
        }

        /// <summary>
        /// Удаление дуги
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if(petri.RemoveLink(links[comboBox1.SelectedIndex]))
            {
                if(comboBox1.Items.Count == 1)
                {
                    Close();
                }
                else if(comboBox1.Items.Count == 2)
                {
                    links.RemoveAt(comboBox1.SelectedIndex);
                    comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
    }
}
