using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

namespace DAPNet
{
    /// <summary>
    /// Represents an adaptive detection gate.
    /// </summary>
    public class AdaptiveGate : DetectionGate
    {
        /// <summary>
        /// Holds the default detection treshold parameter.
        /// </summary>
        public const double DefaultParameter = 3.0;

        /// <summary>
        /// Holds the default half-range parameter.
        /// </summary>
        public const int DefautRange = 20;

        /// <summary>
        /// Holds the default decimation parameter.
        /// </summary>
        public const int DefaultDecimation = 50;

        /// <summary>
        /// Holds the default type of deviation calculation.
        /// </summary>
        public const DeviationGate.DeviationType DefaultType = DeviationGate.DeviationType.Approximation;


        /// <summary>
        /// Holds the detection treshold parameter.
        /// </summary>
        private double parameter;
        
        /// <summary>
        /// Holds the half-range parameter.
        /// </summary>
        private int range;

        /// <summary>
        /// Holds the decimation parameter.
        /// </summary>
        private int decimation;

        /// <summary>
        /// Hold the type of deviation calculation.
        /// </summary>
        private DeviationGate.DeviationType type;


        /// <summary>
        /// Creates a new adaptive deviation gate with default settings.
        /// </summary>
        public AdaptiveGate() : this(DefaultParameter, DefautRange, DefaultDecimation, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new adaptive deviation gate.
        /// </summary>
        /// <param name="parameter">Detection treshold parameter of the new adaptive deviation gate.</param>
        public AdaptiveGate(double parameter) : this(parameter, DefautRange, DefaultDecimation, DefaultType)
        {
        }

        /// <summary>
        /// Creates a new adaptive deviation gate.
        /// </summary>
        /// <param name="range">Half-range parameter of the new adaptive deviation gate.</param>
        /// <param name="decimation">Decimation parameter of the new adaptive deviation gate.</param>
        /// <param name="type">Type of deviation calculation of the new adaptive deviation gate.</param>
        public AdaptiveGate(int range, int decimation, DeviationGate.DeviationType type) : this(DefaultParameter, range, decimation, type)
        {
        }

        /// <summary>
        /// Creates a new adaptive deviation gate.
        /// </summary>
        /// <param name="parameter">Detection treshold parameter of the new adaptive deviation gate.</param>
        /// <param name="range">Half-range parameter of the new adaptive deviation gate.</param>
        /// <param name="decimation">Decimation parameter of the new adaptive deviation gate.</param>
        /// <param name="type">Type of deviation calculation of the new adaptive deviation gate.</param>
        public AdaptiveGate(double parameter, int range, int decimation, DeviationGate.DeviationType type)
        {
            this.parameter = parameter;
            this.range = range;
            this.decimation = decimation;
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
        /// Half-range parameter.
        /// </summary>
        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

        /// <summary>
        /// Decimation parameter.
        /// </summary>
        public int Decimation
        {
            get
            {
                return decimation;
            }
            set
            {
                decimation = value;
            }
        }

        /// <summary>
        /// Type of deviation calculation.
        /// </summary>
        public DeviationGate.DeviationType Type
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
            for (int i = 0; i < samples.Count; i++)
            {
                double deviation = GetLocalDeviation(samples, i);
                double treshold = Parameter * deviation;
                detection[i] = Math.Abs(samples[i]) > treshold;
            }
            return detection;
        }


        /// <summary>
        /// Gets the local deviation of the sample at the specified index.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get local deviation of.</param>
        /// <param name="index">Index of the sample to get local deviation of.</param>
        /// <returns>Local deviation of the sample at the specified index.</returns>
        internal double GetLocalDeviation(SampleVector samples, int index)
        {
            switch (type)
            {
                case DeviationGate.DeviationType.Calculation:
                    return GetLocalCalculatedDeviation(samples, index);
                case DeviationGate.DeviationType.Approximation:
                    return GetLocalApproximatedDeviation(samples, index);
                default:
                    throw new InvalidOperationException();
            }
        }


        /// <summary>
        /// Gets the local calculated deviation of the sample at the specified index.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get calculated local deviation of.</param>
        /// <param name="index">Index of the sample to get calculated local deviation of.</param>
        /// <returns>Calculated local deviation of the sample at the specified index.</returns>
        private double GetLocalCalculatedDeviation(SampleVector samples, int index)
        {
            int start = index - Range * Decimation;
            int end = index + Range * Decimation;
            List<double> neighbours = new List<double>();
            for (int i = start; i <= end; i += decimation)
            {
                neighbours.Add(samples[i]);
            }
            return Statistics.StandardDeviation(neighbours);
        }

        /// <summary>
        /// Gets the local approximated deviation of the sample at the specified index.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get approximated local deviation of.</param>
        /// <param name="index">Index of the sample to get approximated local deviation of.</param>
        /// <returns>Approximated local deviation of the sample at the specified index.</returns>
        private double GetLocalApproximatedDeviation(SampleVector samples, int index)
        {
            int start = index - Range * Decimation;
            int end = index + Range * Decimation;
            List<double> absNeighbours = new List<double>();
            for (int i = start; i <= end; i += decimation)
            {
                absNeighbours.Add(Math.Abs(samples[i]));
            }
            return Statistics.Median(absNeighbours) / DeviationGate.KMCoefficient;
        }
    }
}
