using System;
using System.Linq;

namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new HospitalContext()){	
	            Console.WriteLine("Inserindo...");
	            db.Add(new Medicos{CRM=824531, Nome="Dr. Lucas", CodEspecialidade=10});
                db.Add(new Medicos{CRM=645145, Nome="Dra. Maria", CodEspecialidade=15});
                var count = db.SaveChanges();
                //Console.WriteLine(count);

                Console.WriteLine("Consultando...");
                var Meds = db.Medicos.OrderBy(m => m.CRM);

                foreach(var m in Meds)
                {
                    Console.WriteLine(m.CRM);
                    Console.WriteLine(m.Nome);
                    Console.WriteLine(m.CodEspecialidade);
                }
                
            }
        }
    }
}
