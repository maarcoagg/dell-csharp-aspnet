using System.ComponentModel.DataAnnotations;

public class Especialidades{
        
        [Key]
        public int CodEspecialidade {get;set;}
        public string Nome {get;set;}
        public int ValorConsulta {get;set;}
        public string Descricao {get; set;}
}