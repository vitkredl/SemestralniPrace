namespace SemestralniPrace.Models
{
    public class Polozka
    {
        public string Nazev { get; }
        public string Emoji { get; }
        public string Obrazek => $"Obrazky/{Nazev}.png";

        public Polozka(string nazev, string emoji)
        {
            Nazev = nazev;
            Emoji = emoji;
        }
    }
}
