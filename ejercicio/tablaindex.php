<?php
 if (Isset($_POST['accion'])) {
    $fila=@$_POST["filas"]; 
    $columna=@$_POST["columnas"];
}
?>
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
        <form action="" method="post">
        Ingrese Filas<input type="text" name="filas" value="" />
        Ingrese Columnas<input type="text" name="columnas" value="" />
        <input type="submit" value="Crear" name="accion" />
        </form>
        
        <?php 
        if (@$fila==NULL || @$fila=="" && @$columna=NULL || @$columna=="") {
            echo "No hay tabla aun";
        }else{ ?>
        <table border='1'>
            <?php for($i=0;$i<$fila;$i++){ ?> 
            <tr>
        <?php for($j=0;$j<$columna;$j++){ ?> 
                <td>algo</td>
         <?php } ?> </tr> <?php } ?>
        </table>
        <?php
        
        }?>
    </body>
</html>
