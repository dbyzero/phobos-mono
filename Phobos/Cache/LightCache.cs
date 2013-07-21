using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Phobos.Engine.View.Proxies.Entities;
using Phobos.Tools;

namespace Phobos.Cache
{
    static class LightCache
    {
        /**
         * RaycastingBlockingTiles
         * |_
         * |_
         * |_ For a given key (the light raidus), has a Dictionary of (key:Position_from_center_light,value:SortedList_of_coords)
         *    |_
         *    |_
         *    |_ SortedList of vector 2 : all positions from light center which block the light ray, ordered by distance
         */
        static private Dictionary<int, Dictionary<Vector2, SortedList<float, Vector2>>> RaycastingBlockingTiles =
            new Dictionary<int, Dictionary<Vector2, SortedList<float, Vector2>>>() ;



        static public Dictionary<Vector2, SortedList<float, Vector2>> getCacheForARadius(int radius)
        {
            Dictionary<Vector2, SortedList<float, Vector2>> list;
            RaycastingBlockingTiles.TryGetValue(radius, out list);
            return list;
        }



        static public bool isAlreadyStrored(int radius)
        {
            return RaycastingBlockingTiles.ContainsKey(radius);
        }



        static public void setCache(int radius)
        {
            if (!isAlreadyStrored(radius))
            {
                List<Vector2> listCoordsInRadius = new List<Vector2>();
                for (float i = (0 - radius); i < (0 + radius); i++)
                {
                    for (float j = (0 - radius); j < (0 + radius); j++)
                    {
                        Vector2 vect = new Vector2(i, j);
                        if (radius >= vect.Length())
                        {
                            listCoordsInRadius.Add(vect);
                        }
                    }
                }

                Dictionary<Vector2, SortedList<float, Vector2>> newRadiusCache = new Dictionary<Vector2, SortedList<float, Vector2>>();

                foreach (Vector2 targetTile in listCoordsInRadius)
                {
                    SortedList<float, Vector2> coordsList = new SortedList<float, Vector2>();

                    foreach (Vector2 potentialObstructTile in listCoordsInRadius)
                    {
                        if (LightTools.tileIsOnRay(potentialObstructTile, targetTile))
                        {
                            float key = potentialObstructTile.Length();
                            //do not store same key 2 times
                            while (coordsList.ContainsKey(key))
                            {
                                key += 0.000001f;
                            }
                            coordsList.Add(key, potentialObstructTile);
                        }
                    }
                    newRadiusCache.Add(targetTile, coordsList);
                }
                RaycastingBlockingTiles[radius] = newRadiusCache;
            }
        }
    }
}
