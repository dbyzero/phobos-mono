using System;
using Phobos.Engine.Models.Light;
using Phobos.Engine.View.Proxies.Entities;
using Microsoft.Xna.Framework;

namespace Phobos.Tools
{
    static class LightTools
    {
        static public Boolean tileIsOnRay(Vector2 tile, Vector2 lightTarget)
        {
            //false on center
            if (tile == Vector2.Zero) return false;

            //check if we are at oposite part of projection
            if (tile.X * lightTarget.X < 0) return false;
            if (tile.Y * lightTarget.Y < 0) return false;

            //true on row
            if (lightTarget.X == 0 && tile.X == 0 ) return true;
            if (lightTarget.Y == 0 && tile.Y == 0 ) return true;

            //if on row, false to others
            if (lightTarget.X == 0) return false;
            if (lightTarget.Y == 0) return false;

            //don't block itself
            if (tile.X == lightTarget.X &&
                tile.Y == lightTarget.Y) return false;



            float coeff;

            //check if tile is on ray check for Y
            coeff = (((float)lightTarget.Y) / 
                ((float)lightTarget.X));
            if (tile.Y == (int)(coeff * (float)tile.X) ||
                tile.Y == (int)(coeff * (float)tile.X - 1))
            {
                return true;
            }

            //check if tile is on ray check for X
            coeff = (((float)lightTarget.X) /
                ((float)lightTarget.Y));
            if (tile.X == (int)(coeff * ((float)tile.Y) ) ||
                tile.X ==  (int)(coeff * (float)tile.Y) - 1)
            {
                return true;
            }

            return false;
        }
    }
}
