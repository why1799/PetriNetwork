using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri_Network
{
    public class Link
    {
        public Place Place { get; }
        public Bridge Bridge { get; }
        public bool FromPlace { get; }


        public Link(Bridge bridge, Place place)
        {
            Place = place;
            Bridge = bridge;
            FromPlace = false;
        }

        public Link(Place place, Bridge bridge)
        {
            Place = place;
            Bridge = bridge;
            FromPlace = true;
        }
    }
}
