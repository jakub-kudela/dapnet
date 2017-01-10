using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using System.Diagnostics;

namespace DAPNet
{
    /// <summary>
    /// Represents an autoregressive declicker.
    /// </summary>
    public class ARDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default order.
        /// </summary>
        public const int DefaultOrder = 20;

        /// <summary>
        /// Holds the default block size.
        /// </summary>
        public const int DefaultBlockSize = 1024;
        
        /// <summary>
        /// Holds the autoregression output count.
        /// </summary>
        private const int OutputCount = 1;


        /// <summary>
        /// Holds the order.
        /// </summary>
        private int order;

        /// <summary>
        /// Holds the block size.
        /// </summary>
        private int blockSize;

        /// <summary>
        /// Holds the autoregressive parameters.
        /// </summary>
        private Vector<double> parameters;


        /// <summary>
        /// Creates a new autoregressive declicker with default settings.
        /// </summary>
        public ARDeclicker() : this(DefaultOrder, DefaultBlockSize)
        { 
        }

        /// <summary>
        /// Creates a new autoregressive declicker.
        /// </summary>
        /// <param name="order">Order of the new autoregressive declicker.</param>
        public ARDeclicker(int order) : this(order, DefaultBlockSize)
        { 
        }

        /// <summary>
        /// Creates a new autoregressive declicker.
        /// </summary>
        /// <param name="order">Order of the new autoregressive declicker.</param>
        /// <param name="blockSize">Block size of the new autoregressive declicker.</param>
        public ARDeclicker(int order, int blockSize)
        {
            this.order = order;
            this.blockSize = blockSize;
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
        /// Gets excitations for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get excitations of.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Excitations for the specified block.</returns>
        internal SampleVector GetBlockExcitations(SampleVector samples, DetectionVector detection, int index, int count)
        {
            TryEstimateParameters(samples, detection, index, count);
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
            List<double> blockCorrections = new List<double>();
            TryEstimateParameters(samples, detection, index, count);
            Vector<double> xu = GetXu(samples, detection, index, count);
            int xuIndex = 0;
            for (int i = index; i < index + count; i++)
            {
                if (detection[i])
                {
                    blockCorrections.Add(xu[xuIndex]);
                    xuIndex++;
                }
                else
                {
                    blockCorrections.Add(samples[i]);
                }
            }
            return new SampleVector(blockCorrections.ToArray());
        }


        /// <summary>
        /// Tries to estimate autoregressive parameters for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to perform estimation.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if estimation was succesful, false if not.</returns>
        private bool TryEstimateParameters(SampleVector samples, int index, int count)
        {
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
        /// Tries to estimate autoregressive parameters for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to train.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if estimation was succesful, false if not.</returns>
        private bool TryEstimateParameters(SampleVector samples, DetectionVector detection, int index, int count)
        {
            Matrix<double> gDash = GetGDash(samples, detection, index, count);
            Vector<double> xDash = GetXDash(samples, detection, index, count);

            if (gDash == null || xDash == null)
            {
                return false;
            }

            Matrix<double> gDashT = gDash.Transpose();
            parameters = (gDashT * gDash).Inverse() * gDashT * xDash;
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
            for (int i = index; i < index + count; i++)
            {
                gRows.Add(new DenseVector(samples.Take(i - Order, Order).ToArray()));
            }
            return RowsToMatrix(gRows);
        }

        /// <summary>
        /// Gets the G dahsed matrix for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which G dashed to get.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>G dashed.</returns>
        private Matrix<double> GetGDash(SampleVector samples, DetectionVector detection, int index, int count)
        {
            List<Vector<double>> gDashRows = new List<Vector<double>>();
            for (int i = index; i < index + count; i++)
            {
                if (!detection.ContainsFlag(true, i - Order, Order + OutputCount))
                {
                    gDashRows.Add(new DenseVector(samples.Take(i - Order, Order).ToArray()));
                }
            }
            return RowsToMatrix(gDashRows);
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
                if (!detection.ContainsFlag(true, i - Order, Order + OutputCount))
                {
                    xDashSamples.Add(samples[i]);
                }
            }
            return SamplesToVector(xDashSamples);
        }

        /// <summary>
        /// Gets the xk vector for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which xk to get.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>xk.</returns>
        private Vector<double> GetXk(SampleVector samples, DetectionVector detection, int index, int count)
        {
            List<double> xkSamples = new List<double>();
            for (int i = index - Order; i < index + count; i++)
            {
                if (i < index || !detection[i])
                {
                    xkSamples.Add(samples[i]);
                }
            }
            return SamplesToVector(xkSamples);
        }

        /// <summary>
        /// Gets the xu vector for the specified block.
        /// </summary>
        /// <param name="samples">Samples samples of which xu to get.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>xu.</returns>
        private Vector<double> GetXu(SampleVector samples, DetectionVector detection, int index, int count)
        {
            Matrix<double> a = GetA(count);
            Matrix<double> au = GetAU(a, detection, index);
            Matrix<double> auT = au.Transpose();
            Matrix<double> ak = GetAK(a, detection, index);
            Vector<double> xk = GetXk(samples, detection, index, count);
            return -(auT * au).Inverse() * auT * ak * xk;
        }

        /// <summary>
        /// Gets the A matrix for the specified block.
        /// </summary>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>A.</returns>
        private Matrix<double> GetA(int count)
        {
            LinkedList<double> aRow = new LinkedList<double>();
            foreach (double parameter in parameters)
            {
                aRow.AddLast(-parameter);
            }
            aRow.AddLast(1.0d);
            for (int i = aRow.Count; i < count + Order; i++)
            {
                aRow.AddLast(0.0d);
            }
            List<Vector<double>> aRows = new List<Vector<double>>();
            for (int i = 0; i < count; i++)
            {
                aRows.Add(new DenseVector(aRow.ToArray()));
                aRow.AddFirst(0.0d);
                aRow.RemoveLast();
            }
            return RowsToMatrix(aRows);
        }

        /// <summary>
        /// Gets the Ak matrix for the specified block.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <returns>Ak.</returns>
        private Matrix<double> GetAK(Matrix<double> a, DetectionVector detection, int index)
        {
            List<Vector<double>> akColumns = new List<Vector<double>>();
            for (int i = 0; i < a.ColumnCount; i++)
            {
                int detectionIndex = index - Order + i;
                if (i < Order || !detection[detectionIndex])
                {
                    akColumns.Add(a.Column(i));
                }
            }
            return ColumnsToMatrix(akColumns);
        }

        /// <summary>
        /// Gets the Au matrix for the specified block.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <returns>Au.</returns>
        private Matrix<double> GetAU(Matrix<double> a, DetectionVector detection, int index)
        {
            List<Vector<double>> auColumns = new List<Vector<double>>();
            for (int i = Order; i < a.ColumnCount; i++)
            {
                int detectionIndex = index - Order + i;
                if (detection[detectionIndex])
                {
                    auColumns.Add(a.Column(i));
                }
            }
            return ColumnsToMatrix(auColumns);
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

        /// <summary>
        /// Converts column vectors to matrix.
        /// </summary>
        /// <param name="columns">Column vectors to convert to matrix.</param>
        /// <returns>Matrix out of column vectors.</returns>
        private Matrix<double> ColumnsToMatrix(List<Vector<double>> columns)
        {
            int numColumns = columns.Count;
            if (numColumns == 0)
            {
                return null;
            }
            int numRows = columns[0].Count;
            Matrix<double> matrix = new DenseMatrix(numRows, numColumns);
            for (int i = 0; i < numColumns; i++)
            {
                matrix.SetColumn(i, columns[i]);
            }
            return matrix;
        }
    }
}
