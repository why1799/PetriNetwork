using Newtonsoft.Json;
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
        public int Сurvature { get; set; }
        public bool FromPlace { get; }


        public Link(Bridge bridge, Place place)
        {
            Place = place;
            Bridge = bridge;
            FromPlace = false;
            Сurvature = 0;
        }

        public Link(Place place, Bridge bridge)
        {
            Place = place;
            Bridge = bridge;
            FromPlace = true;
            Сurvature = 0;
        }

        [JsonConstructor]
        public Link(Place place, Bridge bridge, int curvature, bool fromPlace)
        {
            Place = place;
            Bridge = bridge;
            FromPlace = fromPlace;
            Сurvature = curvature;
        }

        public static bool operator ==(Link left, Link right)
        {
            return left?.Place == right?.Place && left?.Bridge == right?.Bridge && left?.Сurvature == right?.Сurvature && left?.FromPlace == right?.FromPlace;
        }

        public static bool operator !=(Link left, Link right)
        {
            return left == right;
        }
    }
}
