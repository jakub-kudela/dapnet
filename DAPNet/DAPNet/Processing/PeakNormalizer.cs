using System;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;

namespace DAPNet
{
    /// <summary>
    /// Represents a peak normalizer.
    /// </summary>
    public class PeakNormalizer : Effect
    {
        /// <summary>
        /// Holds the default normalize-to parameter.
        /// </summary>
        public const double DefaultParameter = SampleConverter.UpperBound;


        /// <summary>
        /// Holds the normalize-to parameter.
        /// </summary>
        private double parameter;


        /// <summary>
        /// Creates a new peak normalizer with default settings.
        /// </summary>
        public PeakNormalizer() : this(DefaultParameter)
        {
        }

        /// <summary>
        /// Creates a new peak normalizer.
        /// </summary>
        /// <param name="parameter">Normalize-to parameter of the new peak normalizer.</param>
        public PeakNormalizer(double parameter)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// Normalize-to parameter.
        /// </summary>
        public double Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }

        /// <summary>
        /// Peak normalizes samples.
        /// </summary>
        /// <param name="samples">Samples to peak normalize.</param>
        public override void Process(SampleVector samples)
        {
            double maxAbsSample = GetMaxAbs(samples);
            double ratio = Parameter / maxAbsSample;
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] *= ratio;
            }
        }


        /// <summary>
        /// Gets the maximal absolute value of sample.
        /// </summary>
        /// <param name="samples">Samples to get the maximal absolute value of.</param>
        /// <returns>Maximal aboslute value of sample.</returns>
        internal double GetMaxAbs(SampleVector samples)
        {
            List<double> absSamples = new List<double>();
            for (int i = 0; i < samples.Count; i++)
            {
                absSamples.Add(Math.Abs(samples[i]));
            }
            return Statistics.Maximum(absSamples);
        }
    }
}
