using System.ComponentModel.DataAnnotations;

public class Medicos{

        [Key]
        public long CRM {get;set;}
        public string Nome {get;set;}
        public int CodEspecialidade {get;set;}

}