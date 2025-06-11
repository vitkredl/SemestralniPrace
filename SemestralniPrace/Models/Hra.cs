namespace Po1450_Klikacka.Models
{
    public class Hra
    {
        public Stav AktualniStav { get; private set; }
        public string Zprava { get; private set; } = "";
        private bool konecna = false;

        public bool JeKonecna() => konecna;

        private List<Polozka> vsechny = new()
        {
            new Polozka("koza", "🐐"),
            new Polozka("vlk", "🐺"),
            new Polozka("zeli", "🥬"),
            new Polozka("prevoznik", "🧔")
        };

        public Hra()
        {
            AktualniStav = new Stav();
            Restartuj();
        }

        public void Restartuj()
        {
            AktualniStav = new Stav
            {
                LevyBreh = new List<Polozka>(vsechny),
                JeNaLevemBrehu = true
            };
            AktualniStav.Lod.Clear();
            Zprava = "";
            konecna = false;
        }

        public void VybratPolozku(Polozka polozka)
        {
            if (konecna) return;

           if (AktualniStav.Lod.Contains(polozka))
            {
                AktualniStav.Lod.Remove(polozka);
                return;
            }
           
            bool jePrevoznik = polozka.Nazev == "prevoznik";
            
            if (AktualniStav.Lod.Count >= 2)
                return;
            
            if (!jePrevoznik && AktualniStav.Lod.Any(p => p.Nazev != "prevoznik"))
                return;
            
            if (jePrevoznik && AktualniStav.Lod.Any(p => p.Nazev == "prevoznik"))
                return;
          
            if (AktualniStav.JeNaLevemBrehu && AktualniStav.LevyBreh.Contains(polozka))
                AktualniStav.Lod.Add(polozka);
            else if (!AktualniStav.JeNaLevemBrehu && AktualniStav.PravyBreh.Contains(polozka))
                AktualniStav.Lod.Add(polozka);
        }

        public void Presunout(Action<bool>? zaznamGrafu = null)
        {
            if (konecna) return;

            if (!AktualniStav.Lod.Any(p => p.Nazev == "prevoznik"))
            {
                Zprava = "Převozník musí být v loďce!";
                zaznamGrafu?.Invoke(false);
                return;
            }
            
            int pocetVeci = AktualniStav.Lod.Count(p => p.Nazev != "prevoznik");
            if (pocetVeci > 1)
            {
                Zprava = "Převozník může převézt jen jednu věc!";
                zaznamGrafu?.Invoke(false);
                return;
            }

            List<Polozka> zdroj = AktualniStav.JeNaLevemBrehu ? AktualniStav.LevyBreh : AktualniStav.PravyBreh;
            List<Polozka> cil = AktualniStav.JeNaLevemBrehu ? AktualniStav.PravyBreh : AktualniStav.LevyBreh;

            foreach (var p in AktualniStav.Lod)
                zdroj.Remove(p);

            cil.AddRange(AktualniStav.Lod);
            AktualniStav.JeNaLevemBrehu = !AktualniStav.JeNaLevemBrehu;

            bool chybny = JeChybnyStav();

            if (chybny)
            {
                Zprava = "Chyba! Vlk a koza nebo koza a zelí bez převozníka!";
                konecna = true;
                zaznamGrafu?.Invoke(false);
            }
            else if (AktualniStav.PravyBreh.Count == 4)
            {
                Zprava = "Gratulace! Vyřešeno!";
                konecna = true;
                zaznamGrafu?.Invoke(true);
            }
            else
            {
                Zprava = "";
                zaznamGrafu?.Invoke(true);
            }

            AktualniStav.Lod.Clear();
        }

        private bool JeChybnyStav()
        {
            var breh = AktualniStav.JeNaLevemBrehu ? AktualniStav.PravyBreh : AktualniStav.LevyBreh;
            bool jePrevoznik = breh.Any(p => p.Nazev == "prevoznik");

            if (jePrevoznik) return false;

            bool jeKoza = breh.Any(p => p.Nazev == "koza");
            bool jeVlk = breh.Any(p => p.Nazev == "vlk");
            bool jeZeli = breh.Any(p => p.Nazev == "zeli");

            return (jeKoza && jeVlk) || (jeKoza && jeZeli);
        }
    }
}


