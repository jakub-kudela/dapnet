using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DAPNet
{
    /// <summary>
    /// Represents sinusoidal autoregressive declicker.
    /// </summary>
    public class SARDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default sinusoidal order.
        /// </summary>
        public const int DefaultSinOrder = SDeclicker.DefaultOrder;

        /// <summary>
        /// Holds the default autoregressive order.
        /// </summary>
        public const int DefaultAROrder = ARDeclicker.DefaultOrder;

        /// <summary>
        /// Holds the default block size.
        /// </summary>
        public const int DefaultBlockSize = 1024;


        /// <summary>
        /// Holds the sinusoidal declicker.
        /// </summary>
        private SDeclicker sinDeclicker;

        /// <summary>
        /// Holds the autoregressive declicker.
        /// </summary>
        private ARDeclicker arDeclicker;

        /// <summary>
        /// Holds the block size.
        /// </summary>
        private int blockSize;


        /// <summary>
        /// Creates a new sinusoidal autoregressive declicker with default settings.
        /// </summary>
        public SARDeclicker() : this(DefaultSinOrder, DefaultAROrder, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal autoregressive declicker.
        /// </summary>
        /// <param name="sinHalfOrder">Sinusoidal order of the new sinusoidal autoregressive declicker.</param>
        /// <param name="arOrder">Autoregressive order of the new sinusoidal autoregressive declicker.</param>
        public SARDeclicker(int sinHalfOrder, int arOrder) : this(sinHalfOrder, arOrder, DefaultBlockSize)
        {
        }

        /// <summary>
        /// Creates a new sinusoidal autoregressive declicker.
        /// </summary>
        /// <param name="sinHalfOrder">Sinusoidal order of the new sinusoidal autoregressive declicker.</param>
        /// <param name="arOrder">Autoregressive order of the new sinusoidal autoregressive declicker.</param>
        /// <param name="blockSize">Block size of the new sinusoidal autoregresive declicker.</param>
        public SARDeclicker(int sinHalfOrder, int arOrder, int blockSize)
        {
            sinDeclicker = new SDeclicker(sinHalfOrder); 
            arDeclicker = new ARDeclicker(arOrder);
            this.blockSize = blockSize;
        }

        /// <summary>
        /// Sinusoidal order.
        /// </summary>
        public int SinHalfOrder
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
        /// Autoregressive order.
        /// </summary>
        public int AROrder
        {
            get
            {
                return arDeclicker.Order;
            }
            set
            {
                arDeclicker.Order = value;
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
                    SampleVector blockCorrections = GetCorrections(samples, detection, i, actualBlockSize);
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
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - AROrder, count + AROrder);
            SampleVector arBlockExcitations = arDeclicker.GetBlockExcitations(sinBlockExcitations, AROrder, count);
            return arBlockExcitations;
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
            DetectionVector blockDetection = detection.Take(index - AROrder, count + AROrder);
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - AROrder, count + AROrder);
            SampleVector sinArBlockExcitations = arDeclicker.GetBlockExcitations(sinBlockExcitations, blockDetection, AROrder, count);
            return sinArBlockExcitations;
        }

        /// <summary>
        /// Gets corrections for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get corrections of.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Corrections for the specified block.</returns>
        internal SampleVector GetCorrections(SampleVector samples, DetectionVector detection, int index, int count)
        {
            DetectionVector blockDetection = detection.Take(index - AROrder, count + AROrder);
            SampleVector sinBlockExcitations = sinDeclicker.GetBlockExcitations(samples, index - AROrder, count + AROrder);
            SampleVector arBlockCorrections = arDeclicker.GetBlockCorrections(sinBlockExcitations, blockDetection, AROrder, count);
            SampleVector sinBlockCorrections = sinDeclicker.GetBlockCorrections(samples, detection, index - AROrder, count + AROrder);
            List<double> blockCorrections = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                if (detection[i])
                {
                    int arBlockCorrectionsIndex = i - index;
                    int sinBlockCorrectionsIndex = arBlockCorrectionsIndex + AROrder;
                    blockCorrections.Add(sinBlockCorrections[sinBlockCorrectionsIndex] + arBlockCorrections[arBlockCorrectionsIndex]);
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
