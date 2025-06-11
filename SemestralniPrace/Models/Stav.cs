namespace Po1450_Klikacka.Models
{
    public class Stav
    {
        public List<Polozka> LevyBreh { get; set; } = new();
        public List<Polozka> PravyBreh { get; set; } = new();
        public List<Polozka> Lod { get; set; } = new();
        public bool JeNaLevemBrehu { get; set; } = true;

        public Stav Kopie()
        {
            return new Stav
            {
                LevyBreh = new List<Polozka>(LevyBreh),
                PravyBreh = new List<Polozka>(PravyBreh),
                Lod = new List<Polozka>(Lod),
                JeNaLevemBrehu = JeNaLevemBrehu
            };
        }
    }
}
