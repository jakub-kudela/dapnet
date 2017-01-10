using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a Kasparis-Lane delicker.
    /// </summary>
    public class KLDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default derivative filter order.
        /// </summary>
        private const int DefaultDFOrder = 1;

        /// <summary>
        /// Holds the default root mean square operator half-range parameter.
        /// </summary>
        public const int DefaultRmsRange = 10;

        /// <summary>
        /// Holds the default rcursive median filter half-range parameter.
        /// </summary>
        public const int DefaultRmfRange = 20;

        /// <summary>
        /// Holds the default decimation parameter.
        /// </summary>
        public const int DefaultDecimation = 20;


        /// <summary>
        /// Holds the derivative filter.
        /// </summary>
        private DerivativeFilter derivativeFilter;

        /// <summary>
        /// Holds the local mean operator.
        /// </summary>
        private LocalMeanOperator localMeanOperator;

        /// <summary>
        /// Holds the recursive median filter.
        /// </summary>
        private RecursiveMedianFilter recursiveMedianFilter;

        /// <summary>
        /// Holds the decimation parameter.
        /// </summary>
        private int decimation;

        /// <summary>
        /// Holds the median filter.
        /// </summary>
        private MedianFilter medianFilter;


        /// <summary>
        /// Creates a new Kasparis-Lane declicker with default settings.
        /// </summary>
        public KLDeclicker() : this(DefaultDFOrder, DefaultRmsRange, DefaultRmfRange, DefaultDecimation)
        { 
        }

        /// <summary>
        /// Creates a new Kasparis-Lane declicker.
        /// </summary>
        /// <param name="rmsRange">Root mean square half-range parameter of the new Kasparis-Lane declicker.</param>
        /// <param name="rmfRange">Recursive median filter half-range parameter of the new Kasparis-Lane declicker.</param>
        /// <param name="decimation">Decimation parameter of the new Kasparis-Lane declicker.</param>
        public KLDeclicker(int rmsRange, int rmfRange, int decimation) : this(DefaultDFOrder, rmsRange, rmfRange, decimation)
        { 
        }

        /// <summary>
        /// Creates a new Kasparis-Lane declicker.
        /// </summary>
        /// <param name="dfOrder">Derivative filter order of the new Kasparis-Lane declicker.</param>
        /// <param name="rmsRange">Root mean square half-range parameter of the new Kasparis-Lane declicker.</param>
        /// <param name="rmfRange">Recursive median filter half-range parameter of the new Kasparis-Lane declicker.</param>
        /// <param name="decimation">Decimation parameter of the new Kasparis-Lane declicker.</param>
        public KLDeclicker(int dfOrder, int rmsRange, int rmfRange, int decimation)
        {
            derivativeFilter = new DerivativeFilter(dfOrder);
            localMeanOperator = new LocalMeanOperator(rmsRange);
            recursiveMedianFilter = new RecursiveMedianFilter(rmfRange);
            this.decimation = decimation;
            medianFilter = new MedianFilter();
        }

        /// <summary>
        /// Derivative filter order.
        /// </summary>
        public int DFOrder
        {
            get
            {
                return derivativeFilter.Order;
            }
            set
            {
                derivativeFilter.Order = value;
            }
        }

        /// <summary>
        /// Root mean square half-range parameter.
        /// </summary>
        public int RmsRange
        {
            get
            {
                return localMeanOperator.Parameter;
            }
            set
            {
                localMeanOperator.Parameter = value;
            }
        }

        /// <summary>
        /// Recursive median filter half-range parameter.
        /// </summary>
        public int RmfRange
        {
            get
            {
                return recursiveMedianFilter.Parameter;
            }
            set
            {
                recursiveMedianFilter.Parameter = value;
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
        /// Gets excitations out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get excitations of.</param>
        /// <returns>Excitations of specified samples.</returns>
        public override SampleVector GetExcitations(SampleVector samples)
        {
            SampleVector w = samples.Clone();
            derivativeFilter.Process(w);
            localMeanOperator.Process(w);
            SampleVector b = w.Clone();
            b.DownSample(decimation);
            recursiveMedianFilter.Process(b);
            b.UpSample(decimation);

            SampleVector d = new SampleVector(w.Count);
            for (int i = 0; i < d.Count; i++)
            {
                d[i] = Math.Abs(w[i] - b[i]) / b[i];
            }
            return d;
        }

        /// <summary>
        /// Corrects specified degraded samples.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples to correct.</param>
        public override void Correct(SampleVector samples, DetectionVector detection)
        {
            SampleVector samplesClone = samples.Clone();
            int[] g = GetG(detection);
            for (int i = 0; i < samples.Count; i++)
            {
                medianFilter.Parameter = g[i];
                samples[i] = g[i] > 0 ? medianFilter.GetLocalMedian(samplesClone, i) : samplesClone[i];
            }
        }
    }
}
