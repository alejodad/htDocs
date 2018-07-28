using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LibConexionBD;
using LibLlenarGrid;
using System.Windows.Forms;

namespace Lib_ln_horario
{
    public class Cls_horario
    {
        #region atributos
        private string idHorario;
        private string idProfesor;
        private string idMateria;
        private string error;
        SqlDataReader objReader;
        #endregion

        #region propiedades
        public string gsIdHorario { get => idHorario; set => idHorario = value; }
        public string gsIdProfesor { get => idProfesor; set => idProfesor = value; }
        public string gsIdMateria { get => idMateria; set => idMateria = value; }
        public string gsError { get => error; set => error = value; }
        public SqlDataReader ObjReader { get => objReader; set => objReader = value; }
        #endregion

        #region metodos publicos
        public Cls_horario()
        {
            idHorario = "";
            idProfesor = "";
            idMateria = "";
            error = "";
        }
        public bool grabarHorario()
        {
            string sentencia = "execute usp_insertarHorarioSubConsultas '" + idHorario + "','" + idProfesor + "','" + idMateria + "'";
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

        public bool modificarHorario()
        {
            string sentencia = "usp_modificarHorario '" + idHorario + "','" + idProfesor + "','" + idMateria + "'";
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

        public bool eliminarHorario()
        {
            string sentencia = "execute usp_eliminarHorario '" + idHorario + "'";
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

        public bool buscarHorario()
        {
            string sentencia = "execute usp_buscarHorario '" + idHorario + "'";
            ClsConexion objcnx = new ClsConexion();
            if (objcnx.Consultar(sentencia, false))
            {
                ObjReader = objcnx.Reader;
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

        public bool ListarHorarios(DataGridView datos)
        {
            ClsLlenarGrid objGrid = new ClsLlenarGrid();
            objGrid.NombreTabla = "datosHorarios";
            objGrid.SQL = "listarHorario";

            if (!objGrid.LlenarGrid(datos))
            {
                error = objGrid.Error;
                objGrid = null;
                return false;
            }
            return true;
        }
        #endregion

        #region metodos privados

        #endregion
    }
}
