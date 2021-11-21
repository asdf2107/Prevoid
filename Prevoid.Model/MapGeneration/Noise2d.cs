using System;
using System.Linq;
using System.Numerics;

namespace Prevoid.Model.MapGeneration
{
    public static class Noise2d
    {
        private static Random _Random = new Random();
        private static int[] _Permutation;

        private static Vector2[] _Gradients;

        static Noise2d()
        {
            CalculatePermutation(out _Permutation);
            CalculateGradients(out _Gradients);
        }

        private static void CalculatePermutation(out int[] p)
        {
            p = Enumerable.Range(0, 256).ToArray();

            for (var i = 0; i < p.Length; i++)
            {
                var source = _Random.Next(p.Length);

                var t = p[i];
                p[i] = p[source];
                p[source] = t;
            }
        }

        public static void Reseed()
        {
            CalculatePermutation(out _Permutation);
        }

        public static void Reseed(int seed)
        {
            _Random = new Random(seed);
            Reseed();
        }

        private static void CalculateGradients(out Vector2[] grad)
        {
            grad = new Vector2[256];

            for (var i = 0; i < grad.Length; i++)
            {
                Vector2 gradient;

                do
                {
                    gradient = new Vector2((float)(_Random.NextDouble() * 2 - 1), (float)(_Random.NextDouble() * 2 - 1));
                }
                while (gradient.LengthSquared() >= 1);

                gradient = Vector2.Normalize(gradient);

                grad[i] = gradient;
            }

        }

        private static float Drop(float t)
        {
            t = Math.Abs(t);
            return 1f - t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static float Q(float u, float v)
        {
            return Drop(u) * Drop(v);
        }

        public static float Noise(float x, float y)
        {
            var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));

            var total = 0f;

            var corners = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

            foreach (var n in corners)
            {
                var ij = cell + n;
                var uv = new Vector2(x - ij.X, y - ij.Y);

                var index = _Permutation[(int)ij.X % _Permutation.Length];
                index = _Permutation[(index + (int)ij.Y) % _Permutation.Length];

                var grad = _Gradients[index % _Gradients.Length];

                total += Q(uv.X, uv.Y) * Vector2.Dot(grad, uv);
            }

            return Math.Max(Math.Min(total, 1f), -1f);
        }
    }
}
