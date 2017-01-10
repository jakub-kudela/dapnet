using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DAPNet
{
    /// <summary>
    /// Represents sinusoidal neural network declicker.
    /// </summary>
    public class SNNDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default sinusoidal order.
        /// </summary>
        public const int DefaultSinHalfOrder = SDeclicker.DefaultOrder;

        /// <summary>
        /// Holds the default neural network order.
        /// </summary>
        public const int DefaultNNOrder = NNDeclicker.DefaultOrder;

        /// <summary>
        /// Holds the default block size.
        /// </summary>
        public const int DefaultBlockSize = 1024;


        /// <summary>
        /// Holds the sinusoidal declicker.
        /// </summary>
        private SDeclicker sinDeclicker;

        /// <summary>
        /// Holds the neural networks declicker.
        /// </summary>
        private NNDeclicker nnDeclicker;

        /// <summary>
        /// Holds the block size.
        /// </summary>
        private int blockSize;


        /// <summary>
        /// Creates a new sinusoidal neural network declicker with default settings.
        /// </summary>
        public SNNDeclicker() : this(DefaultSinHalfOrder, DefaultNNOrder, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal neural network declicker.
        /// </summary>
        /// <param name="sinHalfOrder">Sinusoidal order of the new sinusoidal neural network declicker.</param>
        /// <param name="nnOrder">Neural network order of the new sinusoidal neural network declicker.</param>
        public SNNDeclicker(int sinHalfOrder, int nnOrder) : this(sinHalfOrder, nnOrder, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal neural network declicker.
        /// </summary>
        /// <param name="sinHalfOrder">Sinusoidal order of the new sinusoidal neural network declicker.</param>
        /// <param name="nnOrder">Neural network order of the new sinusoidal neural network declicker.</param>
        /// <param name="blockSize">Block size of the new sinusoidal neural network declicker.</param>
        public SNNDeclicker(int sinHalfOrder, int nnOrder, int blockSize)
        {
            sinDeclicker = new SDeclicker(sinHalfOrder);
            nnDeclicker = new NNDeclicker(nnOrder);
            this.blockSize = blockSize;
        }  

        /// <summary>
        /// Sinusoidal order.
        /// </summary>
        public int SinOrder
        {
            get
            {
                return sinDeclicker.Order;
            }
            set
            {
                sinDeclicker.Order = value;
            }
        }

        /// <summary>
        /// Neural network order.
        /// </summary>
        public int NNOrder
        {
            get
            {
                return nnDeclicker.Order;
            }
            set
            {
                nnDeclicker.Order = value;
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
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - NNOrder, count + NNOrder);
            SampleVector sinNnBlockExcitations = nnDeclicker.GetBlockExcitations(sinBlockExcitations, NNOrder, count);
            return sinNnBlockExcitations;
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
            DetectionVector blockDetection = detection.Take(index - NNOrder, count + NNOrder);
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - NNOrder, count + NNOrder);
            SampleVector sinNnBlockExcitations = nnDeclicker.GetBlockExcitations(sinBlockExcitations, blockDetection, NNOrder, count);
            return sinNnBlockExcitations;
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
            DetectionVector blockDetection = detection.Take(index - NNOrder, count + NNOrder);
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - NNOrder, count + NNOrder);
            SampleVector nnBlockCorrections = nnDeclicker.GetBlockCorrections(sinBlockExcitations, blockDetection, NNOrder, count);
            SampleVector sinBlockCorrections = sinDeclicker.GetBlockCorrections(samples, detection, index - NNOrder, count + NNOrder);
            List<double> blockCorrections = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                if (detection[i])
                {
                    int nnBlockCorrectionsIndex = i - index;
                    int sinBlockCorrectionsIndex = nnBlockCorrectionsIndex + NNOrder;
                    blockCorrections.Add(sinBlockCorrections[sinBlockCorrectionsIndex] + nnBlockCorrections[nnBlockCorrectionsIndex]);
                }
                else
                {
                    blockCorrections.Add(samples[i]);
                }
            }
            return new SampleVector(blockCorrections.ToArray());
        }
    }
}
