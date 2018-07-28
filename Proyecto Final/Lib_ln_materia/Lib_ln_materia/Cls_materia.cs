using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LibConexionBD;
using LibLlenarGrid;
using System.Windows.Forms;
using LibLlenarCombos;

namespace Lib_ln_materia
{
    public class Cls_materia
    {
        #region atributos
        private string idMateria;
        private string nombreMateria;
        private string error;
        SqlDataReader objReader;
        #endregion

        #region propiedades
        public string gsIdMateria { get => idMateria; set => idMateria = value; }
        public string gsNombreMateria { get => nombreMateria; set => nombreMateria = value; }
        public string gsError { get => error; set => error = value; }
        public SqlDataReader ObjReader { get => objReader; set => objReader = value; }
        #endregion

        #region metodos publicos
        public Cls_materia()
        {
            idMateria = "";
            nombreMateria = "";
            error = "";
        }

        public bool grabarMateria()
        {
            string sentencia = "execute usp_insertarMateria '" + idMateria + "','" + nombreMateria + "'";
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

        public bool modificarMateria()
        {
            string sentencia = "usp_modificarMateria '" + idMateria + "','" + nombreMateria + "'";
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

        public bool eliminarMateria()
        {
            string sentencia = "execute usp_eliminarMateria '" + idMateria + "'";
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

        public bool buscarMateria()
        {
            string sentencia = "execute usp_buscarrMateria '" + idMateria + "'";
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

        public bool ListarMateriass(DataGridView datos)
        {
            ClsLlenarGrid objGrid = new ClsLlenarGrid();
            objGrid.NombreTabla = "datosProfes";
            objGrid.SQL = "listarMaterias";

            if (!objGrid.LlenarGrid(datos))
            {
                error = objGrid.Error;
                objGrid = null;
                return false;
            }
            return true;
        }

        public bool llenarMateria(ComboBox materia)
        {
            ClsLlenarCombos algunCombo = new ClsLlenarCombos();
            algunCombo.NombreTabla = "tablaMaterias";
            algunCombo.SQL = "execute traerMaterias";
            algunCombo.ColumnaValor = "id";
            algunCombo.ColumnaTexto = "materia";
            if (!algunCombo.LlenarCombo(materia))
            {
                error = algunCombo.Error;
                algunCombo = null;
                return false;
            }
            algunCombo = null;
            return true;
        }
        #endregion

        #region metodos privados

        #endregion
    }
}
