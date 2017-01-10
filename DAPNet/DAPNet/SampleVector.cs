using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a sample collection.
    /// </summary>
    public class SampleVector
    {
        /// <summary>
        /// Holds the phase inversion coefficient.
        /// </summary>
        private const int InversionCoefficient = -1;

        /// <summary>
        /// Holds the default sample rate.
        /// </summary>
        public const double DefaultSampleRate = 44100;


        /// <summary>
        /// Holds the sample rate.
        /// </summary>
        private double sampleRate;

        /// <summary>
        /// Holds the samples.
        /// </summary>
        private double[] samples;


        /// <summary>
        /// Creates a new sample collection.
        /// </summary>
        /// <param name="count">Number of samples for collection to hold.</param>
        public SampleVector(int count) : this(count, DefaultSampleRate)
        { 
        }

        /// <summary>
        /// Creates a new sample collection.
        /// </summary>
        /// <param name="count">Number of samples for collection to hold.</param>
        /// <param name="sampleRate">Sample rate of the new sample collection.</param>
        public SampleVector(int count, double sampleRate)
        {
            this.sampleRate = sampleRate;
            this.samples = new double[count];
        }

        /// <summary>
        /// Creates a new sample collection with default sample rate.
        /// </summary>
        /// <param name="samples">Samples of the new sample collection.</param>
        public SampleVector(double[] samples) : this(samples, DefaultSampleRate)
        {
        }

        /// <summary>
        /// Creates a new sample collection.
        /// </summary>
        /// <param name="samples">Samples of the new sample collection.</param>
        /// <param name="sampleRate">Sample rate of the new sample collection.</param>
        public SampleVector(double[] samples, double sampleRate)
        {
            this.sampleRate = sampleRate;
            this.samples = samples;
        }

        /// <summary>
        /// Number of samples stored in collection.
        /// </summary>
        public int Count
        {
            get
            {
                return samples.Length;
            }
        }

        /// <summary>
        /// Sample rate of the stored samples.
        /// </summary>
        public double SampleRate
        {
            get
            {
                return sampleRate;
            }
            set
            {
                sampleRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the individual samples.
        /// </summary>
        /// <param name="index">Index of the sample to get or set.</param>
        /// <returns>Sample at given index in case it exists, else a base sample.</returns>
        public double this[int index]
        {
            get
            {
                return (0 <= index && index < Count) ? samples[index] : SampleConverter.Base;
            }
            set
            {
                samples[index] = value;
            }
        }

        /// <summary>
        /// Reverses order of samples in collection.
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(samples);
        }

        /// <summary>
        /// Inverts the phase of sample in collection.
        /// </summary>
        public void Invert()
        {
            for (int i = 0; i < Count; i++)
            {
                samples[i] *= InversionCoefficient;
            }
        }

        /// <summary>
        /// Downsamples samples of collection using sample decimation.
        /// </summary>
        /// <param name="decimation">Dectimation coefficient.</param>
        public void DownSample(int decimation)
        {
            int downCount = Count % decimation == 0 ? (Count / decimation) : (Count / decimation + 1);
            double[] downSamples = new double[downCount];
            for (int i = 0; i < downCount; i++)
            {
                downSamples[i] = this[i * decimation];
            }
            samples = downSamples;
            sampleRate /= decimation;
        }

        /// <summary>
        /// Upsamples samples of collection using sample repetition.
        /// </summary>
        /// <param name="repetition">Repetition coefficient.</param>
        public void UpSample(int repetition)
        {
            int upCount = Count * repetition;
            double[] upSamples = new double[upCount];
            for (int i = 0; i < upCount; i++)
            {
                upSamples[i] = this[i / repetition];
            }
            samples = upSamples;
            sampleRate *= repetition;
        }

        /// <summary>
        /// Admixes other samples to this sample collection.
        /// </summary>
        /// <param name="samples">Samples to admix to this collection.</param>
        public void Admix(SampleVector samples)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] += samples[i];
            }
        }

        /// <summary>
        /// Unmixes other samples from this sample collection.
        /// </summary>
        /// <param name="samples">Samples to unmix from this collection.</param>
        public void Unmix(SampleVector samples)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] -= samples[i];
            }
        }

        /// <summary>
        /// Replaces samples of this sample collection by other samples.
        /// </summary>
        /// <param name="samples">Other samples to replace samples of this sample collection.</param>
        /// <param name="index">Starting index of replacement.</param>
        /// <param name="count">Number of consecutive samples to replace.</param>
        public void Replace(SampleVector samples, int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                this[i] = samples[i - index];
            }
        }
       
        /// <summary>
        /// Gets the calculated mean of absolute values of differences between
        /// corresponding samples of this sample collection and another one.
        /// </summary>
        /// <param name="samples">Other samples for the calculation.</param>
        /// <param name="index">Starting index fot the calculation.</param>
        /// <param name="count">Number of samples for the calculation.</param>
        /// <returns>Calculated mean of absolute values of differences between sample collections.</returns>
        public double GetDiffMean(SampleVector samples, int index, int count)
        {
            double sumDiff = SampleConverter.Base;
            for (int i = index; i < index + count; i++)
            {
                sumDiff += Math.Abs(this[i] - samples[i]);
            }
            return sumDiff / count;
        }

        /// <summary>
        /// Gets the calculated deviation of differences between corresponding 
        /// samples of this sample collection and another one.
        /// </summary>
        /// <param name="samples">Other samples for the calculation.</param>
        /// <param name="index">Starting index fot the calculation.</param>
        /// <param name="count">Number of samples for the calculation.</param>
        /// <returns>Calculated deviation of differences between sample collections.</returns>
        public double GetDiffDeviation(SampleVector samples, int index, int count)
        {
            double sumDiff = SampleConverter.Base;
            double sumSquaredDiff = SampleConverter.Base;
            for (int i = index; i < index + count; i++)
            {
                double absDiff = Math.Abs(this[i] - samples[i]);
                sumDiff += absDiff;
                sumSquaredDiff += absDiff * absDiff;
            }
            double meanSquaredDiff = sumSquaredDiff / count;
            double meanAbsDiff = sumDiff / count;
            return Math.Sqrt(meanSquaredDiff - meanAbsDiff * meanAbsDiff);
        }

        /// <summary>
        /// Gets a sample sub-collection containing samples of this collection.
        /// </summary>
        /// <param name="index">Starting index of samples to be taken.</param>
        /// <param name="count">Number of consecutive samples to be taken.</param>
        /// <returns>Taken sample sub-collection.</returns>
        public SampleVector Take(int index, int count)
        {
            SampleVector taken = new SampleVector(count, sampleRate);
            for (int i = 0; i < count; i++)
            {
                taken[i] = this[index + i];
            }
            return taken;
        }

        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        /// <returns>A new sample collection that is a copy of this instance.</returns>
        public SampleVector Clone()
        {
            double[] samplesClone = (double[])samples.Clone();
            return new SampleVector(samplesClone, sampleRate);
        }

        /// <summary>
        /// Gets an array of copied samples stored in collection.
        /// </summary>
        /// <returns>An array of samples copied from collection.</returns>
        public double[] ToArray()
        {
            return (double[])samples.Clone();
        }
    }
}
