﻿using System;
using System.Collections.Generic;

namespace dell_hospital.Models
{
    public partial class Medicos
    {
        public Medicos()
        {
            Consultas = new HashSet<Consultas>();
        }

        public string Crm { get; set; }
        public string Nome { get; set; }
        public int CodEspecialidade { get; set; }

        public virtual Especialidades CodEspecialidadeNavigation { get; set; }
        public virtual ICollection<Consultas> Consultas { get; set; }
    }
}
