using Phobos.Engine.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos.Engine.Models.World.Generators {
    class BasicGenerator {

        public int Zoom;
        public int MaxHeight;
        public int Octave;
        public double Pitch;

        public BasicGenerator( int zoom = 200, int maxHeight = 100, int octave = 7, double pitch = 0.9f ) {
            Zoom = zoom;
            MaxHeight = maxHeight;
            Octave = octave;
            Pitch = pitch;
        }

        public float perlinNoiseHeight( float x, float y ) {
            double getnoise = 0;
            double maxAmplitude = 0;
            for( int a = 0 ; a < Octave - 1 ; a++ )//This loops trough the octaves.
            {
                //This uses our perlin noise functions. It calculates all our zoom and frequency and amplitude
                double frequency = Math.Pow( 2, a );//This increases the frequency with every loop of the octave.
                double amplitude = Math.Pow( Pitch, a );//This decreases the amplitude with every loop of the octave.
                maxAmplitude += amplitude;
                getnoise += noise( x * frequency / Zoom, y / Zoom * frequency ) * amplitude;
            }

            return (float) ( MaxHeight * ( getnoise + maxAmplitude ) / ( maxAmplitude * 2 ) );//Convert to 0-maxHeight values.
        }

        public double noise( double x, double y ) {
            double floorx = (double) ( (int) x );//This is kinda a cheap way to floor a double integer.
            double floory = (double) ( (int) y );
            double s, t, u, v;//Integer declaration
            s = findnoise2( floorx, floory );
            t = findnoise2( floorx + 1, floory );
            u = findnoise2( floorx, floory + 1 );//Get the surrounding pixels to calculate the transition.
            v = findnoise2( floorx + 1, floory + 1 );
            double int1 = interpolate( s, t, x - floorx );//Interpolate between the values.
            double int2 = interpolate( u, v, x - floorx );//Here we use x-floorx, to get 1st dimension. Don't mind the x-floorx thingie, it's part of the cosine formula.
            return interpolate( int1, int2, y - floory );//Here we use y-floory, to get the 2nd dimension.
        }

        public double findnoise2( double x, double y ) {
            int n = (int) x + (int) y * 57;
            n = ( n << 13 ) ^ n;
            int nn = ( n * ( n * n * 60493 + 19990303 ) + 1376312589 ) & 0x7fffffff;
            return 1.0 - ( (double) nn / 1073741824.0 );
        }

        static double interpolate( double a, double b, double x ) {
            double ft=x * 3.1415927;
            double f=( 1.0 - Math.Cos( ft ) ) * 0.5;
            return a * ( 1.0 - f ) + b * f;

        }

        public Map BuildSimpleMap() {
            Map m = Map.BuildFlatMap();
            Chunk current_chunk;
            Core current_core;
            for( int i = 0 ; i < Map.MAP_SIZE ; i++ ) {
                for( int j = 0 ; j < Map.MAP_SIZE ; j++ ) {
                    current_chunk = m[ i, j ];
                    for( int k = 0 ; k < Chunk.CHUNKS_SIZE ; k++ ) {
                        for( int l = 0 ; l < Chunk.CHUNKS_SIZE ; l++ ) {
                            current_core = current_chunk[ k, l ];
                            current_core.layerList.Clear();
                            current_core.layerList.Add( this.perlinNoiseHeight( ( i * Chunk.CHUNKS_SIZE + k ) , ( j * Chunk.CHUNKS_SIZE + l )  ), Layer.DIRT );
                        }
                    }
                }
            }

            return m;
        }
    }
}
