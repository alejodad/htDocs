using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Usar:
using System.Windows.Forms;//para aplicaciones windows 
using System.Web.UI.WebControls;//para aplicaciones web 
using LibConexionBD;//requerida por que debe traer la info correspondiente 


namespace LibLlenarGrids
{
    public class ClsLLenarGrids
    {
        #region"Constructor"
            public ClsLLenarGrids()
            {
                strNombreTabla = "";
                strSQL = "";
                strError = "";
            }
        #endregion


        #region"Atributos"
            private string strNombreTabla;//nombre de tabla requerido para almacenar info temp. con dataset 
            private string strSQL; //la sentencia 
            private string strError;//
        #endregion


        #region"Propiedades"
            public string NombreTabla
            {
                get { return strNombreTabla; }
                set { strNombreTabla = value; }
            }
            public string SQL
            {
                get { return strSQL; }
                set { strSQL = value; }
            }
            public string ERROR
            {
                get { return strError; }
            }
        #endregion


        #region "Metodos Publicos"
            public bool LlenarGridWeb(System.Web.UI.WebControls.GridView grdGenerico)//tipo web 
            {
                if (strSQL == "")
                {
                    strError = "Debe definir una instruccion SQL";
                    return false;
                }
                ClsConexion objConexionBD = new ClsConexion();
                if (strNombreTabla == "")
                { 
                    strNombreTabla = "DatosGridWeb"; 
                }
                if (objConexionBD.LlenarDataSet(strNombreTabla, strSQL, false))
                {
                    grdGenerico.DataSource = objConexionBD.DataSet_Retornado.Tables[strNombreTabla];
                    grdGenerico.DataBind();
                    objConexionBD.CerrarConexion();
                    objConexionBD = null;
                    return true;
                }
                else
                {
                    strError = objConexionBD.Error;
                    objConexionBD.CerrarConexion();
                    objConexionBD = null;
                    return false;
                }
            }



            public bool LlenarGrid(DataGridView grdGenerico)//para windows  se pide un parametro tipo matriz
            {
                if (strSQL == "")
                { 
                    strError = "Debe definir una instruccion SQL"; 
                }

                ClsConexion objConexionBD = new ClsConexion();
                if (strNombreTabla == "")
                { 
                    strNombreTabla = "LlenarGrid"; //se le puede poner el nombre que quiera
                }
            //el metodo dataset con sus tres parametros el nombre de la tabla el name de sql usp_listaremp y que sea true 
            if (objConexionBD.LlenarDataSet(strNombreTabla, strSQL, false))
           
            {
                grdGenerico.DataSource = objConexionBD.DataSet_Retornado.Tables[strNombreTabla]; //este grd generico method datasoruce se le envia el dataset_retornado que tiene una tabla la del sql procedure
                objConexionBD.CerrarConexion();//cerrar ccnx
                    objConexionBD = null;
                    return true;
                }
                else
                {
                    strError = objConexionBD.Error;
                    objConexionBD.CerrarConexion();
                    objConexionBD = null;
                    return false;
                }
            }
        #endregion
    }
}
      
     