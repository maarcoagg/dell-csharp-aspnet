using System.ComponentModel.DataAnnotations;

public class Consultas{

        [Key]
        public int CodConsulta {get;set;}
        public long CPF {get;set;}
        public long CRM {get;set;}
        public long Coren {get;set;}
        public string DataConsulta {get;set;}
        public int CodTriagem {get;set;}
}