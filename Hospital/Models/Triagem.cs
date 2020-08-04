using System.ComponentModel.DataAnnotations;
public class Triagem{
        [Key]
        public int CodTriagem {get;set;}
        public long CPF {get;set;}
        public long Coren {get;set;}
        public string DataConsulta {get;set;}
        public string DescricaoPaciente {get;set;}
        public string Prioridade {get;set;}
}