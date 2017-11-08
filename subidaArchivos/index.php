<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title></title>
    </head>
    <body>
        <form action="" method="post" enctype="multipart/form-data">
            <input type="file" name="archivo" value="" />
            <input type="submit" value="Subir" name="accion"/>
        </form>
        <?php
        date_default_timezone_set("America/Bogota");// saca la fecha por default por zona 
        if (isset($_POST["accion"])) {// creo un condicional donde si en post se le dio al botn 
            $dir = "archivos/";//esta es la carpeta donde se aloja se debe crear con ese nombre omitiendo el slash 
            $ruta = $dir.basename($_FILES["archivo"]["name"]);//para que cualquier SO puede tner el nombre del archivo
            
            $n=$_FILES["archivo"]["name"];
            //echo $n;
            //
            $tipo = pathinfo($ruta,  PATHINFO_EXTENSION);// de aqui se sabe la extension del archivo
            //echo  ($tipo);
            $nombre=date("Y_m_d_h_i_s").  rand(1654324, 5465765); //para ponerle nombre a la variable usando el rand 
            if (file_exists($dir.$nombre.".".$tipo)) { //aca busca en la carpeta un archivo con un nombre que se haya creado 
                // esto -> $dir.$nombre.".".$tipo equivale a esto -> archivos/2017_10_09_07_41_171769127.jpg 
                echo "ya existe";
                $subido=false;
            }else{
                if (move_uploaded_file($_FILES["archivo"]["tmp_name"], $dir."/".$nombre.".".$tipo)) {
                    echo "ok";
                    header("location:".$dir."/".$nombre.".".$tipo);
                }else{
                    echo "nada";
                }
            }
           
        }      
        //print_r($_FILES);
        echo date("Y-m-d h:i:s");
        echo "<br>";
        echo rand(11452, 5462132)
        ?>
    </body>
</html>
