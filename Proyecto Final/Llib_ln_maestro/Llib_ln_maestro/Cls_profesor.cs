using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LibConexionBD;
using LibLlenarGrid;
using System.Windows.Forms;
using LibLlenarCombos;

namespace Llib_ln_maestro
{
    public class Cls_profesor
    {
        #region atributos
        private string idProfesor;
        private string nombre;
        private string apellido;
        private string error;
        SqlDataReader objReader;


        #endregion

        #region propiedades

        public string gsIdProfesor { get => idProfesor; set => idProfesor = value; }
        public string gsNombre { get => nombre; set => nombre = value; }
        public string gsApellido { get => apellido; set => apellido = value; }
        public string gsError { get => error; set => error = value; }
        public SqlDataReader ObjReader { get => objReader; set => objReader = value; }
        #endregion

        #region metodos privados

        #endregion

        #region metodos publicos 
        public Cls_profesor()
        {
            idProfesor = "";
            nombre = "";
            apellido = "";
            error = "";

        }

        public bool grabarProfesor()
        {
            string sentencia = "execute usp_insertarProfesor '"+idProfesor+"','"+nombre+"','"+apellido + "'";
            ClsConexion objcnx = new ClsConexion();
            if (!objcnx.EjecutarSentencia(sentencia, false))
            {
                error = objcnx.Error;
                objcnx = null;
                return false;
            }
            else
            {
                objcnx = null;
                return true;

            }

        }

        public bool modificarProfesor()
        {
            string sentencia = "usp_modificarrProfesor '" + idProfesor + "','" + nombre + "','" + apellido + "'";
            ClsConexion objcnx = new ClsConexion();
            if (!objcnx.EjecutarSentencia(sentencia, false))
            {
                error = objcnx.Error;
                objcnx = null;
                return false;
            }
            else
            {
                objcnx = null;
                return true;

            }

        }

        public bool eliminarProfesor()
        {
            string sentencia = "execute usp_eliminarProfesor '" + idProfesor + "'";
            ClsConexion objcnx = new ClsConexion();
            if (!objcnx.EjecutarSentencia(sentencia, false))
            {
                error = objcnx.Error;
                objcnx = null;
                return false;
            }
            else
            {
                objcnx = null;
                return true;
            }

        }

        public bool buscarProfesor()
        {
            string sentencia = "execute usp_buscarProfesor '" + idProfesor+"'";
            ClsConexion objcnx = new ClsConexion();
            if (objcnx.Consultar(sentencia, false))
            {
                objReader = objcnx.Reader;
                objcnx = null;
                return true;
            }
            else
            {
                error = objcnx.Error;
                objcnx = null;
                return false;
            }
        }

        public bool ListarPersonas(DataGridView datos)
        {
            ClsLlenarGrid objGrid = new ClsLlenarGrid();
            objGrid.NombreTabla = "datosProfes";
            objGrid.SQL = "usp_listarProfesores";

            if (!objGrid.LlenarGrid(datos))
            {
                error = objGrid.Error;
                objGrid = null;
                return false;
            }
            return true;
        }

        public bool llenarProfe(ComboBox profe)
        {
            ClsLlenarCombos algunCombo = new ClsLlenarCombos();
            algunCombo.NombreTabla = "tablaCiudad";
            algunCombo.SQL = "execute traerMaestros";
            algunCombo.ColumnaValor = "id";
            algunCombo.ColumnaTexto = "nombre";
            if (!algunCombo.LlenarCombo(profe))
            {
                error = algunCombo.Error;
                algunCombo = null;
                return false;
            }
            algunCombo = null;
            return true;
        }
        #endregion
    }
}
