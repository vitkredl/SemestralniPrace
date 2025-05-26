namespace SemestralniPrace.Models
{
    public class Stav
    {
        public List<Polozka> LevyBreh { get; set; } = new();
        public List<Polozka> PravyBreh { get; set; } = new();
        public List<Polozka> Lod { get; set; } = new();
        public bool NaLevemBrehu { get; set; } = true;

        public Stav Kopie()
        {
            return new Stav
            {
                LevyBreh = LevyBreh.ToList(),
                PravyBreh = PravyBreh.ToList(),
                Lod = Lod.ToList(),
                NaLevemBrehu = NaLevemBrehu
            };
        }
    }
}
