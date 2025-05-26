namespace SemestralniPrace.Models
{
    public class ZaznamKroku
    {
        public string Popis { get; set; }
        public Stav Stav { get; set; }

        public ZaznamKroku(string popis, Stav stav)
        {
            Popis = popis;
            Stav = stav;
        }
    }
}

