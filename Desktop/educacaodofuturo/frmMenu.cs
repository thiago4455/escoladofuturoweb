﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace educacaodofuturo
{
    public partial class frmMenu : Form
    {
        Action<Form> action;
        Resources.Funcionario funcionario;

        public frmMenu(Action<Form> act, Resources.Funcionario func)
        {
            InitializeComponent();
            action = act;
            funcionario = func;
            SetForm(new frmHome());
        }

        public void SetForm(Form form)
        {
            pnlDireita.Controls.Clear();
            form.TopLevel = false;
            form.AutoScroll = false;
            pnlDireita.Controls.Add(form);
            form.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetForm(new frmHome());
        }
    }
}
