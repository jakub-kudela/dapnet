using System.Collections.Generic;

namespace DAPNet
{
    /// <summary>
    /// Represents a recursive median filter.
    /// </summary>
    public class RecursiveMedianFilter : Effect
    {
        /// <summary>
        /// Holds the default half-range parameter,
        /// </summary>
        public const int DefaultParameter = MedianFilter.DefaultParameter;


        /// <summary>
        /// Holds the half-range parameter.
        /// </summary>
        private MedianFilter medianFilter;


        /// <summary>
        /// Creates a new recursive median filter with default settings.
        /// </summary>
        public RecursiveMedianFilter() : this(DefaultParameter)
        {           
        }

        /// <summary>
        /// Creates a new recursive median filter.
        /// </summary>
        /// <param name="parameter">Half-range parameter of the new recursive median filter.</param>
        public RecursiveMedianFilter(int parameter)
        {
            medianFilter = new MedianFilter(parameter);
        }

        /// <summary>
        /// Half-range parameter.
        /// </summary>
        public int Parameter
        {
            get 
            { 
                return medianFilter.Parameter; 
            }
            set 
            {
                medianFilter.Parameter = value; 
            }
        }

        /// <summary>
        /// Filters samples.
        /// </summary>
        /// <param name="samples">Samples to filter.</param>
        public override void Process(SampleVector samples)
        {
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] = medianFilter.GetLocalMedian(samples, i);
            }
        }
    }
}
