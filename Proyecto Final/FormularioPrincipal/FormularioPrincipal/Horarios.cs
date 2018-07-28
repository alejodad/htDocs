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
using Lib_ln_materia;
using Lib_ln_horario;
using System.Data.SqlClient;

namespace FormularioPrincipal
{
    public partial class Horarios : Form
    {
        public Horarios()
        {
            InitializeComponent();
        }

        private void Horarios_Load(object sender, EventArgs e)
        {
            llebnarcmbPerson();
            llenarcmbMaterias();
            llenarGrdProfes();
            llenarGrdMaterias();
            llenarGrdHorarios();
        }

        private void guardaHorario()
        {
            Cls_horario uno = new Cls_horario();
            try
            {
                uno.gsIdHorario = txtId.Text;
                uno.gsIdProfesor = cmbProfe.Text;
                uno.gsIdMateria= cmbMateria.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!uno.grabarHorario())
            {
                MessageBox.Show(uno.gsError);
                return;
            }
            else
            {
                MessageBox.Show("Grabado con exito");
            }
        }

        private void modificarHorario()
        {
            Cls_horario uno = new Cls_horario();
            try
            {
                uno.gsIdHorario = txtId.Text;
                uno.gsIdProfesor = Convert.ToString(cmbProfe.Text);
                uno.gsIdMateria = Convert.ToString(cmbMateria.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!uno.modificarHorario())
            {
                MessageBox.Show(uno.gsError);
                return;
            }
            else
            {
                MessageBox.Show("Modificado  con exito");
            }
        }

        private void eliminarHoriario()
        {
            Cls_horario uno = new Cls_horario();
            try
            {
                uno.gsIdHorario = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!uno.eliminarHorario())
            {
                MessageBox.Show(uno.gsError);
                return;
            }
            else
            {
                MessageBox.Show("Eliminado con exito");
            }
        }
        private void buscar(string id)
        {
            Cls_horario uno = new Cls_horario();
            try
            {
                uno.gsIdHorario = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!uno.buscarHorario())
            {
                MessageBox.Show(uno.gsError);
                uno = null;
                return;
            }
            SqlDataReader lecturaDatos;
            lecturaDatos = uno.ObjReader;
            if (lecturaDatos.HasRows)
            {
                lecturaDatos.Read();
                cmbProfe.Text= lecturaDatos.GetString(1);
                cmbMateria.Text = lecturaDatos.GetString(2);
                lecturaDatos.Close();
            }
        }

        private void llebnarcmbPerson()
        {
            Cls_profesor alguno = new Cls_profesor();
            if (!alguno.llenarProfe(cmbProfe))
            {
                lblError.Text = alguno.gsError;
                return;
            }

        }

        private void llenarcmbMaterias()
        {
            Cls_materia alguno = new Cls_materia();
            if (!alguno.llenarMateria(cmbMateria))
            {
                lblError.Text = alguno.gsError;
                return;
            }

        }

        private void llenarGrdProfes()
        {
            Cls_profesor alguno = new Cls_profesor();
            if (!alguno.ListarPersonas(datosProfes))
            {
                MessageBox.Show(alguno.gsError);
                return;
            }
        }

        private void llenarGrdMaterias()
        {
            Cls_materia una = new Cls_materia();
            if (!una.ListarMateriass(datosMaterias))
            {
                MessageBox.Show(una.gsError);
                return;
            }
        }

        private void llenarGrdHorarios()
        {
            Cls_horario una = new Cls_horario();
            if (!una.ListarHorarios(datosHorario))
            {
                MessageBox.Show(una.gsError);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardaHorario();
            llenarGrdHorarios();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarHorario();
            llenarGrdHorarios();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscar(txtId.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eliminarHoriario();
            llenarGrdHorarios();
        }
    }
}
