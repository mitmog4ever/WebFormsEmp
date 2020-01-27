using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsEmployee
{
    public class DtoEmployee
    {
        public int id_emp { get; set; }
        public string nom_emp { get; set; }
        public string prenom_emp { get; set; }
        public double Salaire_emp { get; set; }
        public System.DateTime date_recrute_emp { get; set; }
        public double tele_emp { get; set; }
        public int id_dep { get; set; }
        public string nom_dep { get; set; }
    }
}