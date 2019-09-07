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

        public override bool Equals(object obj)
        {
            return this == obj as Link;
        }
        public override int GetHashCode()
        {
            return Place.GetHashCode() ^ Bridge.GetHashCode();
        }

        public static bool operator ==(Link left, Link right)
        {
            if (Equals(left, null) == true && Equals(right, null) == true)
            {
                return true;
            }
            if (Equals(left, null) == true && Equals(right, null) == false ||
                Equals(left, null) == false && Equals(right, null) == true)
                return false;
            return left.Place == right.Place
                && left.Bridge == right.Bridge
                && left.Сurvature == right.Сurvature
                && left.FromPlace == right.FromPlace;
        }
        public static bool operator !=(Link left, Link right)
        {
            return !(left == right);
        }
    }
}
