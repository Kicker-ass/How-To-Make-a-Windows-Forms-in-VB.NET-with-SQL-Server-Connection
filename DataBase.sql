Create Database db_Example
Use db_Example

-------------- Tabla ------------------------------

Create Table Persona(
ID		int not null ,
Nombre	varchar (25) Not Null,
Edad	int not null,
Genero	varchar (20),
Constraint PK_Clave_ID Primary Key (ID),
Check (Genero = 'Masculino' or Genero = 'Femenino'),
Check (Edad > 0))

-------------- Prodedimientos Almacenados ------------------------------

----- Insertar -----

Create Procedure sp_Ingresa_Persona
@ID int,
@Nombre Varchar (25),
@Edad int,
@Genero Varchar (20)
As 
	Begin 
		Insert into Persona (Id,Nombre,Edad,Genero) Values
			(@ID,@Nombre,@Edad,@Genero)
End

----- Consultar -----

Create Procedure sp_Consulta_Persona
As
	Begin
		Select * from Persona
End

----- Buscar -----

Create Procedure sp_Busca_Persona 
@ID int 
As 
	Begin 
		Select * from persona
		Where ID = @ID
End

----- Eliminar -----

Create Procedure sp_Elimina_Persona 
@ID int 
As 
	Begin 
		Delete from Persona 
		Where ID = @ID
End 

----- Actualizar Nombre -----

Create Procedure sp_Actualiza_Nombre_Persona  
@ID int,
@Nombre Varchar (25)
As 
	Begin 
		Update Persona 
		Set Nombre = @Nombre 
		Where ID = @ID
End 

----- Actualizar Edad -----

Create Procedure Sp_Actualiza_Edad_Persona 
@ID int, 
@Edad int
As
	Begin 
		Update Persona 
		Set Edad = @Edad
		Where ID = @ID
End 

----- Actualizar Genero -----

Create Procedure Sp_Actualiza_Genero_Persona
@ID int,
@Genero Varchar (20)
As
	Begin 
		Update Persona 
		Set Genero = @Genero
		Where  ID = @ID
End 

---

Select *
From SysObjects 
Where xtype = 'P'

---

Execute sp_Ingresa_Persona 
2,'Gerardo',22,'Masculino'

Execute sp_Consulta_Persona

Truncate Table Persona 

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
		
		Select 	@ID = P.ID,
		@Nombre = P.Nombre,
		@edad = P.Edad,
		@Genero = P.Genero
		From Persona P

		If Exists (Select * from Persona	
		Where @Nombre	= Persona.Nombre
		And	  @Edad		= Persona.Edad
		And   @Genero	= Persona.Genero
		And	  @Id		!= Persona.ID)

		Begin 
			
			Print 'La persona ya esta registrada'
			RollBack Transaction 
		End 

		Else 
			Begin 
				Print 'Registro almacenado correctamente'

				Update Persona 
				Set Nombre	= Upper(Nombre),
				Genero		= UPPER (Genero)
		End

		Execute sp_Consulta_Persona
End