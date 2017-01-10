using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace DAPNet
{
    /// <summary>
    /// Represents a median filter.
    /// </summary>
    public class MedianFilter : Effect
    {
        /// <summary>
        /// Holds the default half-range parameter.
        /// </summary>
        public const int DefaultParameter = 10;


        /// <summary>
        /// Holds the half-range parameter.
        /// </summary>
        private int parameter;


        /// <summary>
        /// Creates a new median filter with default settings.
        /// </summary>
        public MedianFilter() : this(DefaultParameter)
        {           
        }

        /// <summary>
        /// Creates a new median filter.
        /// </summary>
        /// <param name="parameter">Half-range parameter of the new median filter.</param>
        public MedianFilter(int parameter)
        {
            this.parameter = parameter;
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
        /// Filters samples.
        /// </summary>
        /// <param name="samples">Samples to filter.</param>
        public override void Process(SampleVector samples)
        {
            SampleVector samplesClone = samples.Clone();
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] = GetLocalMedian(samplesClone, i);
            }
        }
        

        /// <summary>
        /// Gets the local median of sample.
        /// </summary>
        /// <param name="samples">Samples containing the sample to get the local median of.</param>
        /// <param name="index">Index of the sample to get the local median of.</param>
        /// <returns></returns>
        internal double GetLocalMedian(SampleVector samples, int index)
        {
            int start = index - Parameter;
            int end = index + Parameter;
            List<double> neighbours = new List<double>();
            for (int i = start; i <= end; i++)
            {
                neighbours.Add(samples[i]);
            }
            return Statistics.Median(neighbours);
        }
    }
}
