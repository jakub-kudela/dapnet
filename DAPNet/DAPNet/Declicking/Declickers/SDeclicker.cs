using System;
using System.Collections.Generic;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms.Algorithms;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.IntegralTransforms;
using System.Diagnostics;

namespace DAPNet
{
    /// <summary>
    /// Represents a sinusoidal declicker.
    /// </summary>
    public class SDeclicker : Declicker
    {
        /// <summary>
        /// Represents a fft result bin.
        /// </summary>
        private struct Bin : IComparable<Bin>
        {
            /// <summary>
            /// Holds index.
            /// </summary>
            private int index;

            /// <summary>
            /// Holds magnitude.
            /// </summary>
            private double magnitude;


            /// <summary>
            /// Creates a fft result bin.
            /// </summary>
            /// <param name="index">Index of the new fft result bin.</param>
            /// <param name="magnitude">Magnitude of the new fft result bin.</param>
            public Bin(int index, double magnitude)
            {
                this.index = index;
                this.magnitude = magnitude;
            }

            /// <summary>
            /// Index.
            /// </summary>
            public int Index
            {
                get
                {
                    return index;
                }
            }

            /// <summary>
            /// Magnitude
            /// </summary>
            public double Magnitude
            {
                get
                {
                    return magnitude;
                }
            }

            /// <summary>
            /// Compares bin to another one.
            /// </summary>
            /// <param name="other">Other bin to compare this to.</param>
            /// <returns>Less than zero if magnitude of this bin is smaller than othres, zero if equal, greater than zero if it is bigger.</returns>
            public int CompareTo(Bin other)
            {
                return magnitude.CompareTo(other.magnitude);
            }
        }

        /// <summary>
        /// Holds the default order.
        /// </summary>
        public const int DefaultOrder = 5;

        /// <summary>
        /// Holds the default block size.
        /// </summary>
        public const int DefaultBlockSize = 1024;


        /// <summary>
        /// Holds the order.
        /// </summary>
        private int order;

        /// <summary>
        /// Holds the block size.
        /// </summary>
        private int blockSize;

        /// <summary>
        /// Holds the discrete fourier transform.
        /// </summary>
        private DiscreteFourierTransform dicreteFourierTransform;

        /// <summary>
        /// Holds the bin chart.
        /// </summary>
        private List<Bin> binChart;

        /// <summary>
        /// Holds the sinusoidal parameters.
        /// </summary>
        private Vector<double> parameters;


        /// <summary>
        /// Creates a new sinusoidal declicker with default settings.
        /// </summary>
        public SDeclicker() : this(DefaultOrder, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal declicker.
        /// </summary>
        /// <param name="order">Order of the new sinusoidal declicker.</param>
        public SDeclicker(int order) : this(order, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal declicker.
        /// </summary>
        /// <param name="order">Order of the new sinusoidal declicker.</param>
        /// <param name="blockSize">Block size of the new sinusoidal declicker.</param>
        public SDeclicker(int order, int blockSize)
        {
            this.order = order;
            this.blockSize = blockSize;
            dicreteFourierTransform = new DiscreteFourierTransform();
            binChart = new List<Bin>();
        }

        /// <summary>
        /// Order.
        /// </summary>
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        /// <summary>
        /// Processing block size.
        /// </summary>
        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize = value;
            }
        }

        /// <summary>
        /// Gets excitations out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get excitations of.</param>
        /// <returns>Excitations of specified samples.</returns>
        public override SampleVector GetExcitations(SampleVector samples)
        {
            SampleVector excitations = new SampleVector(samples.Count);
            for (int i = 0; i < samples.Count; i += BlockSize)
            {
                int actualBlockSize = Math.Min(BlockSize, samples.Count - i);
                SampleVector blockExcitations = GetBlockExcitations(samples, i, actualBlockSize);
                excitations.Replace(blockExcitations, i, actualBlockSize);
            }
            return excitations;
        }

        /// <summary>
        /// Corrects specified degraded samples.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples to correct.</param>
        public override void Correct(SampleVector samples, DetectionVector detection)
        {
            for (int i = 0; i < samples.Count; i += BlockSize)
            {
                int actualBlockSize = Math.Min(BlockSize, samples.Count - i);
                if (detection.ContainsFlag(true, i, actualBlockSize))
                {
                    SampleVector blockCorrections = GetBlockCorrections(samples, detection, i, actualBlockSize);
                    samples.Replace(blockCorrections, i, actualBlockSize);
                }
            }
        }

        /// <summary>
        /// Gets excitations for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get excitations of.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Excitations for the specified block.</returns>
        internal SampleVector GetBlockExcitations(SampleVector samples, int index, int count)
        {
            TryEstimateParameters(samples, index, count);
            Matrix<double> g = GetG(samples, index, count);
            Vector<double> x = GetX(samples, index, count);
            Vector<double> blockExcitations = x - g * parameters;
            return new SampleVector(blockExcitations.ToArray());
        }

        /// <summary>
        /// Gets corrections for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get corrections of.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Corrections for the specified block.</returns>
        internal SampleVector GetBlockCorrections(SampleVector samples, DetectionVector detection, int index, int count)
        {
            TryEstimateParameters(samples, index, count);
            Matrix<double> g = GetG(samples, index, count);
            Vector<double> blockCorrections = g * parameters;
            return new SampleVector(blockCorrections.ToArray());
        }


        /// <summary>
        /// Tries to estimate sinusoidal parameters for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to perform estimation.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if estimation was succesful, false if not.</returns>
        private bool TryEstimateParameters(SampleVector samples, int index, int count)
        {
            TryEstimateBinChart(samples, index, count);
            Matrix<double> g = GetG(samples, index, count);
            Vector<double> x = GetX(samples, index, count);

            if (g == null || x == null)
            {
                return false;
            }

            Matrix<double> gT = g.Transpose();
            parameters = (gT * g).Inverse() * gT * x;
            return true;
        }

        /// <summary>
        /// Tries to estimate fft results bin chart for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to perform estimation.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if estimation was succesful, false if not.</returns>
        private bool TryEstimateBinChart(SampleVector samples, int index, int count)
        {
            // Excluding the 0-th DC bin.
            int binChartCount = count / 2 - 1;
            if (binChartCount < Order)
            {
                return false;
            }

            Complex[] fftData = new Complex[count];
            for (int i = 0; i < fftData.Length; i++)
            {
                double sample = samples[index + i];
                fftData[i] = new Complex(sample, 0);
            }
            dicreteFourierTransform.BluesteinForward(fftData, FourierOptions.Default);

            binChart.Clear();
            // Excluding the 0-th DC bin.
            for (int i = 1; i < binChartCount + 1; i++)
            {
                binChart.Add(new Bin(i, fftData[i].Magnitude));
            }
            binChart.Sort();
            binChart.Reverse();
            return true;
        }


        /// <summary>
        /// Gets the G matrix for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which G to get.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>G.</returns>
        private Matrix<double> GetG(SampleVector samples, int index, int count)
        {
            List<Vector<double>> gRows = new List<Vector<double>>();
            for (int i = 0; i < count; i++)
            {
                gRows.Add(GetGRow(i, count));
            }
            return RowsToMatrix(gRows);
        }

        /// <summary>
        /// Gets the row of a G matrix.
        /// </summary>
        /// <param name="index">Number of G matrix row to get.</param>
        /// <param name="blockSize">Block size upon which is G matrix made.</param>
        /// <returns></returns>
        private Vector<double> GetGRow(int index, int blockSize)
        {
            List<double> gRowSamples = new List<double>();
            // Including the DC zeroth bin.
            gRowSamples.Add(1.0d);
            for (int i = 0; i < Order; i++)
            {
                int binIndex = binChart[i].Index;
                double freq = binIndex * 2.0d * Math.PI / blockSize;
                gRowSamples.Add(Math.Sin(index * freq));
                gRowSamples.Add(Math.Cos(index * freq));
            }
            return new DenseVector(gRowSamples.ToArray());
        }

        /// <summary>
        /// Gets the x vector for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which x to get.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>x.</returns>
        private Vector<double> GetX(SampleVector samples, int index, int count)
        {
            return new DenseVector(samples.Take(index, count).ToArray());
        }

        /// <summary>
        /// Gets the x dahsed for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which x dashed to get.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>x dashed.</returns>
        private Vector<double> GetXDash(SampleVector samples, DetectionVector detection, int index, int count)
        {
            List<double> xDashSamples = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                if (!detection[i])
                {
                    xDashSamples.Add(samples[i]);
                }
            }
            return SamplesToVector(xDashSamples);
        }     


        /// <summary>
        /// Converts samples to vector.
        /// </summary>
        /// <param name="samples">Samples to convert to vector.</param>
        /// <returns>Vector out of samples.</returns>
        private Vector<double> SamplesToVector(List<double> samples)
        {
            if (samples.Count == 0)
            {
                return null;
            }

            return new DenseVector(samples.ToArray());
        }

        /// <summary>
        /// Converts row vetors to matrix.
        /// </summary>
        /// <param name="rows">Row vectors to convert to matrix.</param>
        /// <returns>Matrix out of row vectors.</returns>
        private Matrix<double> RowsToMatrix(List<Vector<double>> rows)
        {
            int numRows = rows.Count;
            if (numRows == 0)
            {
                return null;
            }
            int numColumns = rows[0].Count;
            Matrix<double> matrix = new DenseMatrix(numRows, numColumns);
            for (int i = 0; i < numRows; i++)
            {
                matrix.SetRow(i, rows[i]);
            }
            return matrix;
        }
    }
}
