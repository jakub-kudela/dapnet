using System;
using System.IO;

namespace DAPNet
{
    /// <summary>
    /// Represts a sample converter for conversion
    /// of 16-bit samples stored in WAV file.
    /// </summary>
    public class SampleConverter16Bit : SampleConverter
    {
        /// <summary>
        /// Reads an 16-bit sample from the wave reader.
        /// </summary>
        /// <param name="waveReader">The wave reader to read the 16-bit sample from.</param>
        /// <returns>Read 16-bit sample.</returns>
        public override double ReadSample(BinaryReader waveReader)
        {
            Int16 value = waveReader.ReadInt16();
            return ToSample(value, Int16.MinValue, Int16.MaxValue);
        }

        /// <summary>
        /// Writes an 16-bit sample to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The wave writer to write the 16-bit sample to.</param>
        /// <param name="sample">16-bit sample to write.</param>
        public override void WriteSample(BinaryWriter waveWriter, double sample)
        {
            Int16 value = (Int16)ToValue(sample, Int16.MinValue, Int16.MaxValue);
            waveWriter.Write(value);
        }
    }
}
