using System;
using System.IO;

namespace DAPNet
{
    /// <summary>
    /// Represents a sample converter for conversion of
    /// samples stored in WAV file.
    /// </summary>
    public abstract class SampleConverter
    {
        /// <summary>
        /// Holds the upper bound of sample value.
        /// </summary>
        public const double UpperBound = 1;

        /// <summary>
        /// Holds the base sample value.
        /// </summary>
        public const double Base = 0;

        /// <summary>
        /// Holds the lower bound of sample value.
        /// </summary>
        public const double LowerBound = -1;


        /// <summary>
        /// Reads a sample from the wave reader.
        /// </summary>
        /// <param name="waveReader">The wave reader to read the sample from.</param>
        /// <returns>Read sample.</returns>
        public abstract double ReadSample(BinaryReader waveReader);

        /// <summary>
        /// Writes a sample to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The wave writer to write the sample to.</param>
        /// <param name="sample">Sample to write.</param>
        public abstract void WriteSample(BinaryWriter waveWriter, double sample);


        /// <summary>
        /// Converts the value from an interval to a sample.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="minValue">Minimal interval value.</param>
        /// <param name="maxValue">Maximal interval value.</param>
        /// <returns>Sample converted from value.</returns>
        public static double ToSample(double value, double minValue, double maxValue)
        {
            return GetCorrespondent(value, minValue, maxValue, SampleConverter.LowerBound, SampleConverter.UpperBound);
        }

        /// <summary>
        /// Converts the sample to a value from an interval.
        /// </summary>
        /// <param name="sample">Sample to convert.</param>
        /// <param name="minValue">Minimal interval value.</param>
        /// <param name="maxValue">Maximal interval value.</param>
        /// <returns>Value converted from sample.</returns>
        public static double ToValue(double sample, double minValue, double maxValue)
        {
            // Performs sample truncation according to sample bounds.
            double truncatedSample = GetTruncated(sample);
            return GetCorrespondent(truncatedSample, LowerBound, UpperBound, minValue, maxValue);
        }


        /// <summary>
        /// Truncates the sample according to the sample bounds.
        /// </summary>
        /// <param name="sample">Sample to truncate.</param>
        /// <returns>Truncated sample.</returns>
        private static double GetTruncated(double sample)
        {
            if (sample > UpperBound)
            {
                return UpperBound;
            }
            else if (sample < LowerBound)
            {
                return LowerBound;
            }
            else
            {
                return sample;
            }
        }

        /// <summary>
        /// Converts the value within one interval to a value within another interval.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="sourceMin">Minimal source interval value.</param>
        /// <param name="sourceMax">Maximal source interval value.</param>
        /// <param name="targetMin">Minimal target interval value.</param>
        /// <param name="targetMax">Maximal target interval value.</param>
        /// <returns>A converted value.</returns>
        private static double GetCorrespondent(double value, double sourceMin, double sourceMax, double targetMin, double targetMax)
        {
            double normNum = value - sourceMin;
            double sourceInvervalSize = sourceMax - sourceMin;
            double targetIntervalSize = targetMax - targetMin;
            double conversionRatio = targetIntervalSize / sourceInvervalSize;
            double convertedNormValue = normNum * conversionRatio;
            double correspondingNum = convertedNormValue + targetMin;
            return correspondingNum;
        }
    }
}
