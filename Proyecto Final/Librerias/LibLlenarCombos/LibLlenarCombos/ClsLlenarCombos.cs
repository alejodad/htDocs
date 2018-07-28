using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Referenciar
using LibConexionBD;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace LibLlenarCombos
{
    public class ClsLlenarCombos
    {
        #region "Constructor"
            public ClsLlenarCombos()
            {
                strNombreTabla = "";
                strSQL = "";
                strError = "";
                strColumnaTexto = "";
                strColumnaValor = "";
            }
        #endregion


        #region "Atributos"
            private string strNombreTabla;// variable para trabajar con dataset
            private string strSQL;//la sentencia 
            private string strError;// la variable error 
            private string strColumnaTexto;// cuando se le da a listar sale un texto este 
            private string strColumnaValor;// internamente representa cada linea de texto como cada columna representada por num 
        #endregion 


        #region "Propiedades"
            public string NombreTabla
            {   get { return strNombreTabla; }
                set { strNombreTabla = value;  }
            }
            public string SQL
            {
                get { return SQL; }
                set { strSQL = value; }
            }
            public string ColumnaTexto
            {
                get { return strColumnaTexto;  }
                set { strColumnaTexto = value; }
            }
            public string ColumnaValor
            {
                get { return strColumnaValor; }
                set { strColumnaValor = value; }
            }
            public string Error
            { 
                get { return strError; } 
            }
        #endregion


        #region "Metodos Publicos"
            public bool LlenarCombo(ComboBox cboGenerico)// cuado se trabajae en Windows form aca se recibe el combbox
            {//el la capa de presentacion se llama este pero se debe instanciar 
                if (Validar())//si esta correcto 
                {
                    ClsConexion objConexionBD = new ClsConexion();//instancia la calse cnx 
                    if (objConexionBD.LlenarDataSet(strNombreTabla, strSQL, false))//se le envian parametros 
                    {
                        cboGenerico.DataSource = objConexionBD.DataSet_Retornado.Tables[strNombreTabla];//permite recibir la consulta de la tabla 
                        cboGenerico.DisplayMember = strColumnaTexto;// cual es el campo que se listara como texto. 
                        cboGenerico.ValueMember = strColumnaValor;//el valor de la columna que quiere que se liste
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
                else
                    return false;
            }


            public bool LlenarComboWeb(DropDownList cboGenerico)//metodo cuando se eeste trabajando en web 
            {
                if (Validar())
                {
                    ClsConexion objConexionBD = new ClsConexion();
                    if (objConexionBD.LlenarDataSet(strNombreTabla, strSQL, false))
                    {
                        cboGenerico.DataSource = objConexionBD.DataSet_Retornado.Tables[strNombreTabla];
                        cboGenerico.DataTextField = strColumnaTexto;
                        cboGenerico.DataValueField = strColumnaValor;
                        cboGenerico.DataBind();
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
                else
                    return false;
            }
        #endregion


        #region "Metodos Privados"
            private bool Validar()
            {if (strSQL == "")// valida que se ingrese el procedimiento almacenado 
            {  strError = "No definio la instrucción SQL";
                return false;
            }
            if (strColumnaTexto == "")// sin ingrese la columna de texto 
            {
                strError = "No definio el nombre de la columna para el texto del Combobox";
                return false;
            }

            if (strColumnaValor == "")
            {
                strError = "No definio el nombre de la columna para el valor del combobox";
                return false;
            }

            if (strNombreTabla == "")//que ingresen el nombre de la tabla 
                strNombreTabla = "Tabla";
                return true;
            }
        #endregion





    }
}
