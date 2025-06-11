using Po1450_Klikacka.Models;

namespace SemestralniPrace.Models
{
    public class ZaznamKroku
    {
        public ZaznamKroku(string popis, Stav stav)
        {
            Popis = popis;
            Stav = stav;
        }

        public string Popis { get; }
        public Stav Stav { get; }
    }
}
