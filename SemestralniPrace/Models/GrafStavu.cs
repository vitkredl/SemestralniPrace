namespace SemestralniPrace.Models
{
    public class GrafStavu
    {
        public bool Spravny { get; set; }

        public GrafStavu(bool spravny)
        {
            Spravny = spravny;
        }
    }
}