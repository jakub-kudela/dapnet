using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a naive declicker.
    /// </summary>
    public class NDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default order.
        /// </summary>
        public const int DefaultOrder = 5;

        /// <summary>
        /// Holds the derivative filter.
        /// </summary>
        private DerivativeFilter filter;


        /// <summary>
        /// Creates a new naive declicker with default settings.
        /// </summary>
        public NDeclicker() : this(DefaultOrder)
        { 
        }

        /// <summary>
        /// Creates a new naive declicker.
        /// </summary>
        /// <param name="order">Order of the new naive declicker.</param>
        public NDeclicker(int order)
        {
            filter = new DerivativeFilter(order);
        }


        /// <summary>
        /// Gets excitations out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get excitations of.</param>
        /// <returns>Excitations of specified samples.</returns>
        public override SampleVector GetExcitations(SampleVector samples)
        {
            SampleVector f = samples.Clone();
            filter.Process(f);
            return f;
        }

        /// <summary>
        /// Corrects specified degraded samples.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples to correct.</param>
        public override void Correct(SampleVector samples, DetectionVector detection)
        {
            int[] g = GetG(detection);
            int[] o = GetO(detection);

            for (int i = 0; i < samples.Count; i++)
            {
                if (detection[i])
                {
                    double p_i = samples[i - o[i]];
                    double s_i = samples[i + g[i] - o[i] + 1];
                    double v_i = (double)o[i] / (g[i] + 1);
                    samples[i] = (1 - v_i) * p_i + v_i * s_i;
                }
            }
        }
    }
}
