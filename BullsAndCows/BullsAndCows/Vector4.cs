using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{

    class Vector4
    {
        public float x, y, z, w;

        public Vector4() { 
        
        }
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static void Test() {
            Vector4 v = new Vector4(0, 1, 2, 3);
            Console.WriteLine("v " + v.ToString());
            Vector4 u = new Vector4(3, 2, 5, 2);
            Console.WriteLine("u " + u);
            Console.WriteLine("u+v " + (u + v).ToString());
            Console.WriteLine("u*v " + (u * v).ToString());
            Console.WriteLine("u-v " + (u - v).ToString());
            Console.WriteLine("v-u " + (v - u).ToString());
            Console.WriteLine("u*4 " + (u * 4).ToString());
            Console.WriteLine("u sqmagnitude " + Vector4.SqMagnitude(u).ToString());
            Console.WriteLine("u magnitude " + Vector4.Magnitude(u).ToString());
            Console.WriteLine("u normal " + Vector4.Normal(u).ToString());
            Console.WriteLine("u normal " + Vector4.Magnitude(Vector4.Normal(u)).ToString());
        }

        public static Vector4 operator + (Vector4 u, Vector4 v) {
            return new Vector4(u.x + v.x, u.y + v.y, u.z + v.z, u.w + v.w);
        }

        public static Vector4 operator -(Vector4 u)
        {
            return new Vector4(-u.x, -u.y, -u.z, -u.w);
        }
        public static Vector4 operator - (Vector4 u, Vector4 v)
        {
            return u + -v;
        }

        public static Vector4 operator * (Vector4 u, float a)
        { 
            return new Vector4(a*u.x, a * u.y, a * u.z, a * u.w);
        }
        public static Vector4 operator *(float a, Vector4 u)
        {
            return u*a;
        }

        public static Vector4 operator * (Vector4 u, Vector4 v)
        {
            return new Vector4(u.x*v.x, u.y*v.y, u.z*v.z, u.w*v.w);
        }

        public static Vector4 operator /(Vector4 u, float a)
        {
            return new Vector4(u.x/a, u.y/a, u.z/a, u.w/a);
        }
        public static Vector4 operator /(float a, Vector4 u)
        {
            return new Vector4(a/u.x, a/u.y, a/u.z, a/u.w);
        }

        public static float Magnitude(Vector4 u) {
            return (float)Math.Sqrt(SqMagnitude(u));
        }

        public static float SqMagnitude(Vector4 u)
        {
            return u.x* u.x + u.y * u.y + u.z * u.z + u.w * u.w;
        }


        public static Vector4 Normal(Vector4 u)
        {
            float magnitude = Magnitude(u);
            if (magnitude == 0)
            {
                return u;
            }
            return u / magnitude;
        }



        public override string ToString()
        {
            return base.ToString() + "("+x+", "+y + ", " +z + ", " +w+")";
        }

       
    }

}
