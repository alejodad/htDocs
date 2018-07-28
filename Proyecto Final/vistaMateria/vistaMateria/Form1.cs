using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Lib_ln_materia;

namespace vistaMateria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gurdarMateria()
        {
            Cls_materia una = new Cls_materia();
            try
            {
                una.gsIdMateria = txtId.Text;
                una.gsNombreMateria = txtName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                una = null;
                return;
            }
            if (!una.grabarMateria())
            {
                MessageBox.Show(una.gsError);
                una = null;
                return;
            }
            else
            {
                MessageBox.Show("Grabado con exito");
            }
        }

        private void modificarMateria()
        {
            Cls_materia una = new Cls_materia();
            try
            {
                una.gsIdMateria = txtId.Text;
                una.gsNombreMateria = txtName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                una = null;
                return;
            }
            if (!una.modificarMateria())
            {
                MessageBox.Show(una.gsError);
                una = null;
                return;
            }
            else
            {
                MessageBox.Show("Modificado con exito");
            }
        }

        private void eliminarMateria()
        {
            Cls_materia una = new Cls_materia();
            try
            {
                una.gsIdMateria = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                una = null;
                return;
            }
            if (!una.eliminarMateria())
            {
                MessageBox.Show(una.gsError);
                una = null;
                return;
            }
            else
            {
                MessageBox.Show("Eliminado con exito");
            }
        }

        private void buscarMateria(string id)
        {
            Cls_materia una = new Cls_materia();
            try
            {
                una.gsIdMateria = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!una.buscarMateria())
            {
                MessageBox.Show(una.gsError);
                una = null;
                return;
            }
            SqlDataReader lecturaDatos;
            lecturaDatos = una.ObjReader;
            if (lecturaDatos.HasRows)
            {
                lecturaDatos.Read();
                txtName.Text = lecturaDatos.GetString(1);
                lecturaDatos.Close();
            }
            else
            {
                MessageBox.Show("No existen datos");
            }
        }

        private void llenarGrd()
        {
            Cls_materia una = new Cls_materia();
            if (!una.ListarMateriass(grdDatos))
            {
                MessageBox.Show(una.gsError);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gurdarMateria();
            llenarGrd();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            modificarMateria();
            llenarGrd();
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            eliminarMateria();
            llenarGrd();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            buscarMateria(txtId.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llenarGrd();
        }
    }
}
