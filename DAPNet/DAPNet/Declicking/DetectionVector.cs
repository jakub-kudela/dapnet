using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a sample detection flag collection.
    /// </summary>
    public class DetectionVector
    {
        /// <summary>
        /// Holds the default detection flag.
        /// </summary>
        public const bool DefaultFlag = false;

        /// <summary>
        /// Holds the detection flags.
        /// </summary>
        private bool[] flags;


        /// <summary>
        /// Creates a new sample detection collection.
        /// </summary>
        /// <param name="count">Number of detection flags for collection to hold.</param>
        public DetectionVector(int count)
        {
            flags = new bool[count];
        }

        /// <summary>
        /// Creates a new sample detection collection.
        /// </summary>
        /// <param name="flags">Number of detection flags for collection to hold.</param>
        public DetectionVector(bool[] flags)
        {
            this.flags = flags;
        }

        /// <summary>
        /// Number of detection flags stored in collection.
        /// </summary>
        public int Count
        {
            get
            {
                return flags.Length;
            }
        }

        /// <summary>
        /// Gets or sets the individual detection flag.
        /// </summary>
        /// <param name="index">Index of the detection flag to get or set.</param>
        /// <returns>Detection flag at given index in case it exists, else a default flag.</returns>
        public bool this[int index]
        {
            get
            {
                return (0 <= index && index < Count) ? flags[index] : DefaultFlag;
            }
            set
            {
                flags[index] = value;    
            }
        }

        /// <summary>
        /// Reverses order of detection flags in collection.
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(flags);
        }

        /// <summary>
        /// Iverts the detection flags in collection.
        /// </summary>
        public void Invert()
        {
            for (int i = 0; i < Count; i++)
            {
                flags[i] = !flags[i];
            }
        }

        /// <summary>
        /// Gets a value indicating whether collection contains detection flag within a specified interval.
        /// </summary>
        /// <param name="flag">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>true, if collection contains detection flag within a specified interval, false if not.</returns>
        public bool ContainsFlag(bool flag, int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                if (this[i] == flag)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the number of a specified detection flags within a specified interval.
        /// </summary>
        /// <param name="flag">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>Number of specified detection flags within a specified interval.</returns>
        public int GetNumFlags(bool flag, int index, int count)
        {
            int numFlags = 0;
            for (int i = index; i < index + count; i++)
            {
                if (this[i] == flag)
                {
                    numFlags++;
                }
            }
            return numFlags;
        }

        /// <summary>
        /// Gets the number of sequences of specified detection flag within a specified interval. 
        /// </summary>
        /// <param name="flag">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>Number of sequances of specified detection flags within a specified interval.</returns>
        public int GetNumSeqs(bool flag, int index, int count)
        {
            int numSeqs = 0;
            bool previousFlag = !flag;
            for (int i = index; i < index + count; i++)
            {
                if (this[i] == flag && this[i] != previousFlag)
                {
                    numSeqs++;
                }
                previousFlag = this[i];
            }
            return numSeqs;
        }

        /// <summary>
        /// Gets the average sequence length of specified detection flag within a specified interval.
        /// </summary>
        /// <param name="flag">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>Average sequence length of specified detection flag within a specified interval.</returns>
        public double GetAvgSeqLength(bool flag, int index, int count)
        {
            double numFlags = GetNumFlags(flag, index, count);
            double numSeqs = GetNumSeqs(flag, index, count);
            return numFlags / numSeqs;
        }

        /// <summary>
        /// Gets an effectivity ratio of detection match within specified interval.
        /// </summary>
        /// <param name="idealDetection">An ideal detection to match detection with.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>Effectivity ratio of detection match within specified interval.</returns>
        public double GetEffectivity(DetectionVector idealDetection, int index, int count)
        {
            double numMatchedFlags = 0;
            for (int i = index; i < index + count; i++)
            {
                if (this[i] == idealDetection[i])
                {
                    numMatchedFlags++;
                }
            }
            return numMatchedFlags / count;
        }

        /// <summary>
        /// Gets an effectivity ratio of detection match of specified detection flag within specified interval.
        /// </summary>
        /// <param name="idealDetection">An ideal detectio to match detection with.</param>
        /// <param name="flag">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        /// <returns>Effectivity ratio of detection match of specified detection flag within specified interval.</returns>
        public double GetEffectivity(DetectionVector idealDetection, bool flag, int index, int count)
        {
            double numMatchedFlags = 0;
            double numIdealFlags = 0;
            for (int i = index; i < index + count; i++)
            {
                if (idealDetection[i] == flag)
                {
                    numIdealFlags++;
                    if (this[i] == flag)
                    {
                        numMatchedFlags++;
                    }
                }
            }
            return numMatchedFlags / numIdealFlags;
        }

        /// <summary>
        /// Gets a detection flag sub-collection containing detection flags of this collection.
        /// </summary>
        /// <param name="index">Starting index of detection flag to be taken.</param>
        /// <param name="count">Number of consecutive detection flags to be taken</param>
        /// <returns>Taken detection flag sub-collection.</returns>
        public DetectionVector Take(int index, int count)
        {
            DetectionVector taken = new DetectionVector(count);
            for (int i = 0; i < count; i++)
            {
                taken[i] = this[index + i];
            }
            return taken;
        }
        
        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        /// <returns>A new detection flag collection that is a copy of this instance.</returns>
        public DetectionVector Clone()
        {
            return new DetectionVector((bool[])flags.Clone());
        }
    }
}
