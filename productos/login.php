

<?php 
session_start();
if (isset($_SESSION['usuario'])) {
    header("location:index.php");
}
if (isset($_POST['accion'])) {
    $u= trim($_POST['user']);
    $pass = trim($_POST['pass']);
    
    if ($u == NULL or $u=="") {
        $msj="<p style='color:red'> debe ingresar usuario</p>" ;
    }
    require_once 'cad/UsuarioCad.php';
    $datos = UsuarioCad::Acceder($u, $pass);
    if($datos->num_rows==0){
        $msj="Usuario o clave incorecto";
    }else{
        $campo=$datos->fetch_object();
        print_r($campo);
        $_SESSION['nombre'] =  $campo->nombre;
        $_SESSION['usuario'] =  $campo->idUsuario;
        header("location:index.php");
    }
    
}
?>
<html>
    <head>
        <meta charset="UTF-8">
        <title>Login</title>
    </head>
    <h1>Aqui el login </h1>
    <body>
        <form action="" method="post">
            <table border="1">
                <thead>
                    <tr>
                <th colspan="2">Iniciar Sesion </th>
                
            </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Usuario</td>
                        <td> <input type="text" name="user" id="iduser" value="" autofocus>  </td>
                    </tr>
                    <tr>
                        <td>clave </td>
                        <td> <input type="password" name="pass" id="idpass" vaalue=""> </td>
                    </tr> 
                    <tr >
                        <td colspan="2"><input type="submit" value="acceder" name="accion" /></td> 
                    </tr>                   
                    <tr>
                        <td colspan="2"> <?=  @$msj; ?> </td>
                    </tr>
                </tbody>
        </table>
        </form>
    </body>
</html>
