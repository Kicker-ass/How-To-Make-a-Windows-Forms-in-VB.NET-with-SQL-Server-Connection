Create Database Examen
Use Examen

-------------- Tabla ------------------------------
Create Table Genero(
ID_Genero int Not null identity (1,1),
Genero Varchar (20),
Constraint PK_Clave_Gen primary key (ID_Genero),
Check (Genero = 'MASCULINO' or Genero = 'FEMENINO'))

Create Table Persona(
ID_Persona		int not null ,
Nombre	varchar (25) Not Null,
Edad	int not null,
ID_Genero	int ,
Constraint PK_Clave_ID Primary Key (ID_Persona),
Constraint FK_Genero foreign Key (ID_Genero) references Genero (ID_Genero),
Check (Edad > 0 And Edad < 125))

-------------- Prodedimientos Almacenados ------------------------------

----- Insertar -----

Create Procedure sp_Ingresa_Persona
@ID int,
@Nombre Varchar (25),
@Edad int,
@Genero int 
As 
	Begin 
		Insert into Persona (ID_Persona,Nombre,Edad,ID_Genero) Values
			(@ID,@Nombre,@Edad,@Genero)
End

----- Consultar -----

Create Procedure sp_Consulta_Persona
As
	Begin
		Select ID_Persona as ID,Nombre,Edad,Genero from Persona Inner Join Genero 
		on Persona.ID_Genero = Genero.ID_Genero
End

----- Buscar -----

Create Procedure sp_Busca_Persona 
@ID int 
As 
	Begin 
		Select * from persona
		Where ID_Genero = @ID
End

----- Eliminar -----

Create Procedure sp_Elimina_Persona 
@ID int 
As 
	Begin 
		Delete from Persona 
		Where ID_Genero = @ID
End 

----- Actualizar Nombre -----

Create Procedure sp_Actualiza_Nombre_Persona  
@ID int,
@Nombre Varchar (25)
As 
	Begin 
		Update Persona 
		Set Nombre = @Nombre 
		Where ID_Persona = @ID
End 

----- Actualizar Edad -----

Create Procedure Sp_Actualiza_Edad_Persona 
@ID int, 
@Edad int
As
	Begin 
		Update Persona 
		Set Edad = @Edad
		Where ID_Persona = @ID
End 

----- Actualizar Genero -----

Create Procedure Sp_Actualiza_Genero_Persona
@ID int,
@Genero int
As
	Begin 
		Update Persona 
		Set ID_Genero = @Genero
		Where  ID_Persona = @ID
End 

----- Disparador Para evitar duplicados -----

Create Trigger Tr_Duplicados
On Persona
For insert,update
As
	Begin 

		Declare @ID int
		Declare @Nombre Varchar (25)
		Declare @Edad int
		Declare @Genero varchar (20)
		
		Select 	@ID = P.ID_Persona,
		@Nombre = P.Nombre,
		@edad = P.Edad,
		@Genero = P.ID_Genero
		From Persona P

		If Exists (Select * from Persona	
		Where @Nombre	= Persona.Nombre
		And	  @Edad		= Persona.Edad
		And   @Genero	= Persona.ID_Genero
		And	  @Id		!= Persona.ID_Persona)

		Begin 
			
			Print 'La persona ya esta registrada'
			RollBack Transaction 
		End 

		Else 
			Begin 
				Print 'Registro almacenado correctamente'

				Update Persona 
				Set Nombre	= Upper(Nombre)
		End

		Execute sp_Consulta_Persona
End