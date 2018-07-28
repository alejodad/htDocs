using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Usar
using System.Data;
using System.Data.SqlClient;
using LibParametro;

namespace LibConexionBD
{
    public class ClsConexion
    {
        #region "Atributos"
        private string strError;//almacena un error 
        private bool blnBDAbierta;//dice si la conexione sta abierta o cerrada
        private string strCadenaCnx;// aca se almacena lo que entrega la libreria parametro
        private SqlConnection objCnnBD;// un objeto de esta clase para tener acceso a los metodos
        private SqlCommand objCmdBD;// un objeto de esta clase para tener acceso a los metodos
        private SqlDataReader objReader;//(cnx)una tyabla en la que me puedo desplazar pero no modificar
        private SqlDataAdapter dapGenerico; //Intermediario para llenar el dataset
        private DataSet dts;//(des_cnx) conjunto de tablas llenado por datadapter
        private string strVrUnico;
        private SqlParameter objParametro;//se va usar para mandar parametros de los procedure
        //la  mayoria de estas clases las da el framework 
        #endregion
        #region "Constructor"
        public ClsConexion()
        {
            objCnnBD = new SqlConnection();//arriba se hizo aca se instancia
            objCmdBD = new SqlCommand();
            dapGenerico = new SqlDataAdapter();
            strVrUnico = "";
            objParametro = new SqlParameter();
            strError = "";
        }
        #endregion
        #region "Propiedades"
        public SqlDataReader Reader
        {
            get { return objReader; }
        }
        public DataSet DataSet_Retornado
        {
            get { return dts; }
        }
        public string Error
        {
            set { strError = value; }
            get { return strError; }
        }
        public string ValorUnico
        {
            get { return strVrUnico; }
        }
        #endregion
        #region "Metodos Privados"
        private bool GenerarCadenaConexion()//recibe de la libreria parametro la cadena cnx 
        {
            ClsParametro objParam = new ClsParametro();//instancia un objeto de la libreria parametro
            if (objParam.CrearConexion())
            {
                strCadenaCnx = objParam.CadenaConexion;
                objParam = null; //Destruye el objeto
                return true;
            }
            else
            {
                strError = objParam.Error;
                objParam = null;
                return false;
            }
        }
        private bool AbrirConexion()
        {
            if (GenerarCadenaConexion())
            {
                try
                {
                    objCnnBD.ConnectionString = strCadenaCnx;//toma la cadena en una propiedad y la pasa al objeto
                    objCnnBD.Open();//abre la conexion 
                    blnBDAbierta = true;
                    return true;//retorna la conexion
                }
                catch (Exception ex)
                {
                    blnBDAbierta = false;
                    strError = "Error al abrir la conexion -" + ex.Message;
                    return false;
                }
            }
            else
                return false;
        }
        #endregion
        #region "Metodos Publicos"
        public bool Consultar(string SentenciaSQL, bool blnCon_Parametros)//consultar(lib cnx) siempre tendra un "select"
        {
            if (SentenciaSQL == "")
            {
                strError = "Error en instrucción SQL";
                return false;
            }
            if (blnBDAbierta == false)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }
            objCmdBD.Connection = objCnnBD;
            if (blnCon_Parametros)
                objCmdBD.CommandType = CommandType.StoredProcedure;
            else
                objCmdBD.CommandType = CommandType.Text;
            objCmdBD.CommandText = SentenciaSQL;
            try
            {
                objReader = objCmdBD.ExecuteReader();//permite ejecutar una consulta de un streProcedure
                return true;
            }
            catch (Exception ex)
            {
                strError = "Falla en ejecutar comando -" + ex.Message;
                return false;
            }
        }
        public bool EjecutarSentencia(string SentenciaSQL, bool blnCon_Parametros)
        {
            if (SentenciaSQL == "")
            {
                strError = "No se ha definido la sentencia a ejecutar ";
                return false;
            }
            if (blnBDAbierta == false)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }
            objCmdBD.Connection = objCnnBD;
            if (blnCon_Parametros)
                objCmdBD.CommandType = CommandType.StoredProcedure;
            else
                objCmdBD.CommandType = CommandType.Text;
            objCmdBD.CommandText = SentenciaSQL;
            try
            {
                objCmdBD.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                strError = "Error al ejecutar la instrucción -" + ex.Message;
                return false;
            }
        }
        public bool ConsultarValorUnico(string SentenciaSQL, bool blnCon_Parametros)//solo cuando se va a consultar un valor sirve con el "select"
        {
            if (SentenciaSQL == "")
            {
                strError = "No se ha definido la sentencia a ejecutar ";
                return false;
            }
            if (blnBDAbierta == false)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }
            objCmdBD.Connection = objCnnBD;
            if (blnCon_Parametros)
                objCmdBD.CommandType = CommandType.StoredProcedure;
            else
                objCmdBD.CommandType = CommandType.Text;
            objCmdBD.CommandText = SentenciaSQL;
            try
            {
                strVrUnico = Convert.ToString(objCmdBD.ExecuteScalar());
                return true;
            }
            catch (Exception ex)
            {
                strError = "Error al ejecutar instrucción -" + ex.Message;
                return false;
            }
        }
        public void CerrarConexion()
        {
            try
            {
                objCmdBD = null; //Destruye objeto Command
            }
            catch (Exception ex)
            {
                strError = "Falla en cerrar Command -" + ex.Message;
            }
            try
            {
                objCnnBD.Close(); //Cierra la conexión
                objCnnBD = null; //Destruye en la emmoria
            }
            catch (Exception ex)
            {
                strError = "Falla en cerrar conexión -" + ex.Message;
            }
        }
        public bool LlenarDataSet(string NombreTabla, string SentenciaSQL, bool blnCon_Parametros)
        {
            // con este booleano se sabe si hay conexion abierta o no
            if (blnBDAbierta == false)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }
            objCmdBD.Connection = objCnnBD;
            if (blnCon_Parametros)
                objCmdBD.CommandType = CommandType.StoredProcedure;
            else
                objCmdBD.CommandType = CommandType.Text;
            objCmdBD.CommandText = SentenciaSQL;
            try
            {
                dts = new DataSet();//se crea un objeto para llenar el datast
                dapGenerico.SelectCommand = objCmdBD;//recibe un select * from 
                dapGenerico.Fill(dts, NombreTabla); // fill es llenar se llena el objeto dts y se le da el nombre auna tabla es llenado por datadapter
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool AgregarParametro(ParameterDirection Direccion, string Nombre_En_SP,
        SqlDbType TipoDato, Int16 Tamaño, object Valor)
        {
            try
            {
                objParametro.Direction = Direccion;
                objParametro.ParameterName = Nombre_En_SP;
                objParametro.SqlDbType = TipoDato;
                objParametro.Size = Tamaño;
                objParametro.Value = Valor;
                objCmdBD.Parameters.Add(objParametro);
                objParametro = new SqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (false);
            }
        }
        #endregion
    }
}
