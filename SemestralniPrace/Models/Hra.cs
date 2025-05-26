namespace SemestralniPrace.Models
{
    public class Hra
    {
        public Stav AktualniStav { get; private set; }
        public List<ZaznamKroku> Historie { get; private set; } = new();
        public string Zprava { get; private set; } = string.Empty;

        private Polozka? vybrana;

        private List<Polozka> vsechny = new()
        {
            new Polozka("koza", "🐐"),
            new Polozka("vlk", "🐺"),
            new Polozka("zeli", "🥬"),
            new Polozka("prevoznik", "🧔")
        };

        public Hra()
        {
            AktualniStav = new Stav
            {
                LevyBreh = vsechny.ToList()
            };

            Historie.Add(new ZaznamKroku("Start", AktualniStav.Kopie()));
        }

        public void VybratPolozku(Polozka polozka)
        {
            if (AktualniStav.Lod.Contains(polozka))
                AktualniStav.Lod.Remove(polozka);
            else if (AktualniStav.NaLevemBrehu && AktualniStav.LevyBreh.Contains(polozka))
                AktualniStav.Lod.Add(polozka);
            else if (!AktualniStav.NaLevemBrehu && AktualniStav.PravyBreh.Contains(polozka))
                AktualniStav.Lod.Add(polozka);
        }

        public void Presunout()
        {
            if (!AktualniStav.Lod.Any(p => p.Nazev == "prevoznik"))
            {
                Zprava = "Prevoznik musi byt na lodi!";
                return;
            }

            // Presun
            if (AktualniStav.NaLevemBrehu)
            {
                foreach (var p in AktualniStav.Lod)
                    AktualniStav.LevyBreh.Remove(p);
                AktualniStav.PravyBreh.AddRange(AktualniStav.Lod);
            }
            else
            {
                foreach (var p in AktualniStav.Lod)
                    AktualniStav.PravyBreh.Remove(p);
                AktualniStav.LevyBreh.AddRange(AktualniStav.Lod);
            }

            AktualniStav.NaLevemBrehu = !AktualniStav.NaLevemBrehu;
            var novaKopie = AktualniStav.Kopie();
            Historie.Add(new ZaznamKroku("Presun", novaKopie));
            Zprava = "";

            // Kontrola pravidel
            if (JeChybnyStav())
            {
                Zprava = "Chyba! Vlk nesmí být s kozou bez prevoznika, nebo koza se zelím!";
            }
            else if (AktualniStav.PravyBreh.Count == 4)
            {
                Zprava = "Gratulujeme! Vyřešeno.";
            }

            AktualniStav.Lod.Clear();
        }

        private bool JeChybnyStav()
        {
            var breh = AktualniStav.NaLevemBrehu ? AktualniStav.PravyBreh : AktualniStav.LevyBreh;
            bool jeTamPrevoznik = breh.Any(p => p.Nazev == "prevoznik");

            if (!jeTamPrevoznik)
            {
                bool jeKoza = breh.Any(p => p.Nazev == "koza");
                bool jeVlk = breh.Any(p => p.Nazev == "vlk");
                bool jeZeli = breh.Any(p => p.Nazev == "zeli");

                if ((jeKoza && jeVlk) || (jeKoza && jeZeli))
                    return true;
            }

            return false;
        }
    }
}
