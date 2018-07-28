create database profesorMaterias

create table tbl_profesor(
idProfesor varchar(15) primary key,
nombreProfesor varchar (30),
apellidoProfesor varchar (30)
);

create table tbl_materia(
idMateria varchar (15) primary key,
nombreMateria varchar (15)
);
ALTER TABLE tbl_materia ALTER COLUMN nombreMateria varchar (15)

create table tbl_detalle_profesor_materia(
idDetalleProfesorMateria varchar(15) primary key,
idProfesor varchar(15) ,
idMateria varchar (15),
foreign key (idProfesor) references tbl_profesor(idProfesor),
foreign key (idMateria) references tbl_materia(idMateria)
);

--crear profesor-
create procedure usp_insertarProfesor(
@idProfesor varchar(15) ,
@nombreProfesor varchar (30),
@apellidoProfesor varchar (30)
)
as
begin
insert into tbl_profesor values (@idProfesor,@nombreProfesor,@apellidoProfesor);
end
execute usp_insertarProfesor '123','juan','lopez'
--

--modificar profesor--
create procedure usp_modificarrProfesor(
@idProfesor varchar(15) ,
@nombreProfesor varchar (30),
@apellidoProfesor varchar (30)
)
as
begin
update tbl_profesor set nombreProfesor=@nombreProfesor,apellidoProfesor=@apellidoProfesor where idProfesor=@idProfesor;
end
execute usp_modificarrProfesor '123','lidia','lopez'
--
--eliminar profesor--
create procedure usp_eliminarProfesor(
@idProfesor varchar(15)
)
as
begin
delete from  tbl_profesor where idProfesor=@idProfesor;
end
execute usp_eliminarProfesor '123'
--
--buscarprofesor--
create procedure usp_buscarProfesor(
@idProfesor varchar(15)
)
as
begin
select * from  tbl_profesor where idProfesor=@idProfesor;
end
execute usp_buscarProfesor '123'
--
--listar profesores--
create procedure usp_listarProfesores
as
begin
select idProfesor as 'Cedula', nombreProfesor+' '+apellidoProfesor as 'Nombre tutor' from tbl_profesor
end
execute usp_listarProfesores
 --
 --insertar materia--
 create procedure usp_insertarMateria(
@idMateria varchar (15) ,
@nombreMateria varchar (15)
 )
 as
 begin
 insert into tbl_materia values (@idMateria,@nombreMateria);
 end
 execute usp_insertarMateria '002','Filosofia'
 --
 --modificar materia --
create procedure usp_modificarMateria(
@idMateria varchar (15) ,
@nombreMateria varchar (15)
 )
 as 
 begin
 update tbl_materia set nombreMateria=@nombreMateria where idMateria=@idMateria;
 end
 select * from tbl_materia;
 execute usp_modificarMateria '002','Quimica';
 --
 --elmimnar materia --
 create procedure usp_eliminarMateria(
@idMateria varchar (15)
 )
 as 
 begin 
 delete from tbl_materia where idMateria=@idMateria;
 end
select * from tbl_materia;
execute usp_eliminarMateria '002';
--
--buscar materia --
create procedure usp_buscarrMateria(
@idMateria varchar (15)
 )
 as 
 begin 
select * from tbl_materia where idMateria=@idMateria;
end
execute usp_buscarrMateria '002';
--

--listar materias --
create procedure listarMaterias
as
begin
select idMateria as 'Cod. Materia', nombreMateria as 'Nombre' from tbl_materia;
end
execute listarMaterias
--
--treaer maestros--
create procedure traerMaestros
as
begin
select '0' as 'id','Seleccione nombre' as nombre 
union 
select idProfesor,nombreProfesor+' '+ apellidoProfesor from tbl_profesor;
end
execute traerMaestros;

--traer materias--
create procedure traerMaterias
as
begin
select '0' as 'id', 'Seleccione Materia' as materia
union
select * from tbl_materia;
end
execute traerMaterias

--traer materias nombres detalle--
create procedure listarHorario
as 
begin
select idDetalleProfesorMateria Hora,tbl_profesor.nombreProfesor+' '+tbl_profesor.apellidoProfesor Tutor,tbl_materia.nombreMateria Materia from tbl_detalle_profesor_materia 
inner join tbl_profesor on tbl_detalle_profesor_materia.idProfesor=tbl_profesor.idProfesor 
inner join tbl_materia on tbl_detalle_profesor_materia.idMateria=tbl_materia.idMateria ;
end
execute listarHorario
select * from tbl_detalle_profesor_materia
--
--insertar horario esta no se usa en el programa --
create procedure usp_insertarHorario(
@idDetalleProfesorMateria varchar(15),
@idProfesor varchar(15) ,
@idMateria varchar (15)
)
as 
begin
insert into tbl_detalle_profesor_materia values (@idDetalleProfesorMateria,@idProfesor,@idMateria);
end
execute usp_insertarHorario '7','123','002'
--
--insertando horario--
create procedure usp_insertarHorarioSubConsultas(
@idDetalleProfesorMateria varchar(15),
@nombreProfesor varchar(30) ,
@nombreMateria varchar (15)
)
as 
begin
insert into tbl_detalle_profesor_materia values (@idDetalleProfesorMateria,
(select idProfesor from tbl_profesor where nombreProfesor+' '+ apellidoProfesor =@nombreProfesor ),
(select idMateria from tbl_materia where nombreMateria=@nombreMateria ));
end
execute usp_insertarHorarioSubConsultas '8','juan lopez','Filosofia'
select * from tbl_detalle_profesor_materia
--}

--modificando horario--
create procedure usp_modificarHorario(
@idDetalleProfesorMateria varchar(15),
@nombreProfesor varchar(30) ,
@nombreMateria varchar (15)
)
as 
begin
update tbl_detalle_profesor_materia set 
idProfesor=(select idProfesor from tbl_profesor where nombreProfesor+' '+ apellidoProfesor =@nombreProfesor ),
idMateria=(select idMateria from tbl_materia where nombreMateria=@nombreMateria ) where idDetalleProfesorMateria=@idDetalleProfesorMateria;
end
--

--eliminar horario--
create procedure usp_eliminarHorario(
@idDetalleProfesorMateria varchar(15)
)
as 
begin
delete from tbl_detalle_profesor_materia where idDetalleProfesorMateria=@idDetalleProfesorMateria;
end
--

--buscar horario--
create procedure usp_buscarHorario(
@idDetalleProfesorMateria varchar(15)
)
as 
begin
select idDetalleProfesorMateria, tbl_profesor.nombreProfesor+' '+tbl_profesor.apellidoProfesor,tbl_materia.nombreMateria from tbl_detalle_profesor_materia
inner join tbl_profesor on tbl_profesor.idProfesor=tbl_detalle_profesor_materia.idProfesor
inner join tbl_materia on tbl_materia.idMateria=tbl_detalle_profesor_materia.idMateria where idDetalleProfesorMateria=@idDetalleProfesorMateria;
end
execute usp_buscarHorario '8'