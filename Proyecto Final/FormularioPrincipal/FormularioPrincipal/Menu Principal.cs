using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormularioPrincipal
{
    public partial class Form1 : Form
    {
        private Profesores forma;
        private Materias formaMaterias;
        private Horarios formaHorarios;
        public Form1()
        {
            InitializeComponent();
        }

               

        private void profesoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (forma == null)
            {
                forma = new Profesores();
                forma.MdiParent = this;
                forma.FormClosed += new FormClosedEventHandler(CerrarForma);
                forma.Show();
            }
            else
            {
                forma.Activate();
            }
        }
        void CerrarForma(object sender, FormClosedEventArgs e)
        {
            forma = null;
            formaMaterias = null;
            formaHorarios = null;
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formaMaterias == null)
            {
                formaMaterias = new Materias();
                formaMaterias.MdiParent = this;
                formaMaterias.FormClosed += new FormClosedEventHandler(CerrarForma);
                formaMaterias.Show();
            }
            else
            {
                formaMaterias.Activate();
            }
        }

        private void horariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formaHorarios == null)
            {
                formaHorarios = new Horarios();
                formaHorarios.MdiParent = this;
                formaHorarios.FormClosed += new FormClosedEventHandler(CerrarForma);
                formaHorarios.Show();
            }
            else
            {
                formaHorarios.Activate();
            }
        }
    }
}
