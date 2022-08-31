using System.Collections.Generic;

namespace Cine.Domain.Objects
{
    public class OUsuario
    {
        public int Id { get; set; }
        
        /////////////////////////////////////////////
        
        public List<OReview> ReviewsU { get; set; }
    }
}
