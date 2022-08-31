using Cine.Domain.ObjectsR;

namespace Cine.Domain.Objects
{
    public class OSala
    {
        public int Id { get; set; }

        public string Sala { get; set; }

        ///////////////////////////////////////////////////
        
        public List<PeliculaSala> PeliculaSala { get; set; }
    }
}
