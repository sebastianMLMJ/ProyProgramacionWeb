using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModels.ViewModels
{
    public class Kpis
    {
        public int PorConfirmar { get; set; }

        public int Confirmadas { get; set; }

        public int Enviadas { get; set; }

        public int Entregadas { get; set; }
    }
}
