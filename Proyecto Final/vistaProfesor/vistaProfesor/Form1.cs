using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Llib_ln_maestro;
using System.Data.SqlClient;

namespace vistaProfesor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llenarGrd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gurdarProfesor();
            llenarGrd();
        }

        private void gurdarProfesor()
        {
            Cls_profesor profe = new Cls_profesor();
            try
            {
                profe.gsIdProfesor = txtId.Text;
                profe.gsNombre = txtName.Text;
                profe.gsApellido = txtApellido.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                profe = null;
                return;
            }
            if (!profe.grabarProfesor())
            {
                MessageBox.Show(profe.gsError);
                profe = null;
                return;
            }
            else
            {
                MessageBox.Show("Grabado con exito");
            }
        }

        private void modificarProfesor()
        {
            Cls_profesor profe = new Cls_profesor();
            try
            {
                profe.gsIdProfesor = txtId.Text;
                profe.gsNombre = txtName.Text;
                profe.gsApellido = txtApellido.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                profe = null;
                return;
            }
            if (!profe.modificarProfesor())
            {
                MessageBox.Show(profe.gsError);
                profe = null;
                return;
            }
            else
            {
                MessageBox.Show("Modificado con exito");
            }
        }

        private void eliminarrProfesor()
        {
            Cls_profesor profe = new Cls_profesor();
            try
            {
                profe.gsIdProfesor = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                profe = null;
                return;
            }
            if (!profe.eliminarProfesor())
            {
                MessageBox.Show(profe.gsError);
                profe = null;
                return;
            }
            else
            {
                MessageBox.Show("Eliminado con exito");
            }
        }

        private void buscarProfesor(string id)
        {
            Cls_profesor profe = new Cls_profesor();
            try
            {
                profe.gsIdProfesor = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!profe.buscarProfesor())
            {
                MessageBox.Show(profe.gsError);
                profe = null;
                return;
            }
            SqlDataReader lecturaDatos;
            lecturaDatos = profe.ObjReader;
            if (lecturaDatos.HasRows)
            {
                lecturaDatos.Read();
                txtName.Text = lecturaDatos.GetString(1);
                txtApellido.Text = lecturaDatos.GetString(2);
                lecturaDatos.Close();
            }
            else
            {
                MessageBox.Show("No existen datos");
            }
        }

        private void llenarGrd()
        {
            Cls_profesor alguien = new Cls_profesor();
            if (!alguien.ListarPersonas(grdDatos))
            {
                MessageBox.Show(alguien.gsError);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscarProfesor(txtId.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eliminarrProfesor();
            llenarGrd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarProfesor();
            llenarGrd(); 
        }
    }
}
