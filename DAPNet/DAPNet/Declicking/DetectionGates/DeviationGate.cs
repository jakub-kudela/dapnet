using System;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;

namespace DAPNet
{
    /// <summary>
    /// Represents a deviation detection gate.
    /// </summary>
    public class DeviationGate : DetectionGate
    {
        /// <summary>
        /// Represents the type of deviation calculation.
        /// </summary>
        public enum DeviationType
        {
            /// <summary>
            /// Calculated deviation.
            /// </summary>
            Calculation,

            /// <summary>
            /// Kleiner-Matrin approximated deviation.
            /// </summary>
            Approximation
        }


        /// <summary>
        /// Holds the default detection treshold parameter.
        /// </summary>
        public const double DefaultParameter = 3.0d;

        /// <summary>
        /// Holds the default type of deviation calculation.
        /// </summary>
        public const DeviationType DefaultType = DeviationType.Approximation;

        /// <summary>
        /// Holds the Kleiner Martin coefficient for robust deviation estimation.
        /// </summary>
        public const double KMCoefficient = 0.6745d;


        /// <summary>
        /// Holds the detection treshold parameter.
        /// </summary>
        private double parameter;

        /// <summary>
        /// Hold the type of deviation calculation.
        /// </summary>
        private DeviationType type;


        /// <summary>
        /// Creates a new deviation gate with default settings.
        /// </summary>
        public DeviationGate() : this(DefaultParameter, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new deviation gate.
        /// </summary>
        /// <param name="parameter">Detection treshold parameter of the new deviation gate.</param>
        public DeviationGate(double parameter) : this(parameter, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new deviation gate.
        /// </summary>
        /// <param name="type">Type of deviation calculation of the new deviation gate.</param>
        public DeviationGate(DeviationType type) : this(DefaultParameter, type)
        { 
        }

        /// <summary>
        /// Creates a new deviation gate.
        /// </summary>
        /// <param name="parameter">Detection treshold parameter of the new deviation gate.</param>
        /// <param name="type">Type of deviation calculation of the new deviation gate.</param>
        public DeviationGate(double parameter, DeviationType type)
        {
            this.parameter = parameter;
            this.type = type;
        }

        /// <summary>
        /// Detection treshold parameter.
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
        /// Type of deviation calculation.
        /// </summary>
        public DeviationType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Detects samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <returns>Detection made above specified samples.</returns>
        public override DetectionVector Detect(SampleVector samples)
        {
            DetectionVector detection = new DetectionVector(samples.Count);
            double treshold = Parameter * GetCalculatedDeviation(samples);
            for (int i = 0; i < samples.Count; i++)
            {
                detection[i] = Math.Abs(samples[i]) > treshold;
            }
            return detection;
        }


        /// <summary>
        /// Gets the deviation of samples.
        /// </summary>
        /// <param name="samples">Samples to get the deviation of.</param>
        /// <returns>Deviation of the samples.</returns>
        internal double GetDeviation(SampleVector samples)
        {
            switch (type)
            {
                case DeviationType.Calculation:
                    return GetCalculatedDeviation(samples);
                case DeviationType.Approximation:
                    return GetApproximatedDeviation(samples);
                default:
                    throw new InvalidOperationException();
            }
        }

        
        /// <summary>
        /// Gets the calculated deviation of samples.
        /// </summary>
        /// <param name="samples">Samples to get the calculated deviation of.</param>
        /// <returns>Calculated deviation of samples.</returns>
        private double GetCalculatedDeviation(SampleVector samples)
        {
            return Statistics.StandardDeviation(samples.ToArray());
        }

        /// <summary>
        /// Gets the approximated deviation of samples.
        /// </summary>
        /// <param name="samples">Samples to get the approximated deviation of.</param>
        /// <returns>Approximated deviation of samples.</returns>
        private double GetApproximatedDeviation(SampleVector samples)
        {
            List<double> absSamples = new List<double>();
            for (int i = 0; i < samples.Count; i++)
            {
                absSamples.Add(Math.Abs(samples[i]));
            }
            return Statistics.Median(absSamples) / KMCoefficient;
        }
    }
}
