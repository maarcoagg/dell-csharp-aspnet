using System.ComponentModel.DataAnnotations;
public class Pacientes{
        [Key]
        public long CPF {get;set;}
        public string Nome {get;set;}
        public char Sexo {get;set;}
}