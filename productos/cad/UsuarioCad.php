<?php

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 * Description of UsuarioCad
 *
 * @author Ale
 */
class UsuarioCad {
    public static function Acceder($user, $pass) {
        
        $query = "select * from  tbl_usuario  where contraseña = md5('$pass') and idUsuario='{$user}'";
        require_once 'conexion.php';        
        return conexion::conectar()->query($query);  
    }
}
