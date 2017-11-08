<?php 
if (isset($_GET["exportar"])&& $_GET["exportar"]=="e") {    
header("Content-Type:application/vnd.ms-excel");
header('Content-Disposition:attachment,filename="a.xls"');
}else if (isset($_GET["exportar"])&& $_GET["exportar"]=="w") {    
header("Content-Type:application/vnd.ms-word");
header('Content-Disposition:attachment,filename="a.doc"');
    
}
header('Cache-Control:max-age=');
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
        <?php
        
        if (isset($_POST["accion"])) {
            
   $fila=@$_POST["filas"]; 
   $columna=@$_POST["columna"];
   //$matriz[$fila][$columna];
   //echo $fila."<br>".$columna;
        }
        ?>
        
        <table  border="1">
            <?php
            for($i =0;$i<$fila;$i++){
                echo "<tr>";
                for($j = 0;$j<$columna;$j++ ){
                    echo "<td>algp</td>";
                }
                echo "</tr>";
            }
            
            ?>
        </table>
        <a href="?exportar=e">Exportar Excel</a>
        <a href="?exportar=w">Exportar Word</a>
    </body>
</html>
