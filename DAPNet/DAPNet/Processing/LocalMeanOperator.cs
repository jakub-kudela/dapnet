using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

namespace DAPNet
{
    /// <summary>
    /// Represents a local mean operator.
    /// </summary>
    public class LocalMeanOperator : Effect
    {
        /// <summary>
        /// Represents the type of mean calculation.
        /// </summary>
        public enum MeanType
        {
            /// <summary>
            /// Mean of absolute values.
            /// </summary>
            MeanAbs,

            /// <summary>
            /// Root mean square.
            /// </summary>
            RootMeanSquare
        }


        /// <summary>
        /// Holds the default half-range parameter.
        /// </summary>
        public const int DefaultParameter = 10;

        /// <summary>
        /// Holds the default type of mean calculation.
        /// </summary>
        public const MeanType DefaultType = MeanType.RootMeanSquare;


        /// <summary>
        /// Holds the half-range parameter.
        /// </summary>
        private int parameter;

        /// <summary>
        /// Holds the type of mean calculation.
        /// </summary>
        private MeanType type;


        /// <summary>
        /// Creates a new operator with default settings.
        /// </summary>
        public LocalMeanOperator() : this(DefaultParameter, DefaultType)
        {
        }

        /// <summary>
        /// Creates a new local mean operator.
        /// </summary>
        /// <param name="parameter">Half-range parameter of the new operator.</param>
        public LocalMeanOperator(int parameter) : this(parameter, DefaultType)
        {
        }

        /// <summary>
        /// Creates a new local mean operator.
        /// </summary>
        /// <param name="parameter">Half range parameter of the new operator.</param>
        /// <param name="type">Type of a mean of the new operator.</param>
        public LocalMeanOperator(int parameter, MeanType type)
        {
            this.parameter = parameter;
            this.type = type;
        }

        /// <summary>
        /// Half-range parameter.
        /// </summary>
        public int Parameter
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
        /// Processes samples to local means.
        /// </summary>
        /// <param name="samples">Samples to process.</param>
        public override void Process(SampleVector samples)
        {
            SampleVector samplesClone = samples.Clone();
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] = GetLocalMean(samples, i);
            }
        }


        /// <summary>
        /// Gets the local mean of sample.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get the local mean of.</param>
        /// <param name="index">Index of the sample to get the local mean of.</param>
        /// <returns></returns>
        internal double GetLocalMean(SampleVector samples, int index)
        {
            switch (type)
            {
                case MeanType.MeanAbs:
                    return GetLocalMeanAbs(samples, index);
                case MeanType.RootMeanSquare:
                    return GetLocalRootMeanSquare(samples, index);
                default:
                    throw new InvalidOperationException();
            }
        }


        /// <summary>
        /// Gets the local root mean square of sample.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get the local root mean square of.</param>
        /// <param name="index">Index of the sample to get the local root mean square of.</param>
        /// <returns></returns>
        private double GetLocalRootMeanSquare(SampleVector samples, int index)
        {
            int start = index - Parameter;
            int end = index + Parameter;
            List<double> squaredNeighbours = new List<double>();
            for (int i = start; i <= end; i++)
            {
                squaredNeighbours.Add(samples[i] * samples[i]);
            }
            return Math.Sqrt(Statistics.Mean(squaredNeighbours));
        }

        /// <summary>
        /// Gets the local mean of absolute values of sample.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get the local mean of absolute values of.</param>
        /// <param name="index">Index of the sample to get the local mean of absolute values of.</param>
        /// <returns></returns>
        private double GetLocalMeanAbs(SampleVector samples, int index)
        {
            int start = index - Parameter;
            int end = index + Parameter;
            List<double> absNeighbours = new List<double>();
            for (int i = start; i <= end; i++)
            {
                absNeighbours.Add(Math.Abs(samples[i]));
            }
            return Math.Sqrt(Statistics.Mean(absNeighbours));
        }
    }
}
