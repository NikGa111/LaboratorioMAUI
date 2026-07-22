using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Models
{
    internal abstract class VoceBase
    {
        private string _descrizione;

        public string Descrizione
        {
            get => _descrizione;
            set => _descrizione = value;
        }

        public abstract string ToRiga();
        }

}

