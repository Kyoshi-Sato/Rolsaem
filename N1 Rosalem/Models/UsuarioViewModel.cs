namespace N1_Rosalem.Models
{
    public class UsuarioViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string EstadoCivil { get; set; }
        public int Idade { get; set; }
        public string Senha { get; set; }   
        public bool Admin { get; set; }
    }
}

/*
  use BarAppDb

go

drop table Usuarios

create table Usuarios (
	Id int Identity(1,1) primary key,
	Nome nvarchar(100) not null,
    EstadoCivil nvarchar(100) not null,
	Idade int not null,
	Senha nvarchar(100) not null)


ALTER TABLE Usuarios ADD Admin bit NOT NULL DEFAULT 0;
select * from Usuarios

UPDATE Usuarios
SET Admin = 1
WHERE Nome = 'Lucas'; 
 */
