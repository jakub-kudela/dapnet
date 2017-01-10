using System;
using System.IO;

namespace DAPNet
{
    /// <summary>
    /// Represts a sample converter for conversion
    /// of 8-bit samples stored in WAV file.
    /// </summary>
    public class SampleConverter8Bit : SampleConverter
    {
        /// <summary>
        /// Reads an 8-bit sample from the wave reader.
        /// </summary>
        /// <param name="waveReader">The wave reader to read the 8-bit sample from.</param>
        /// <returns>Read 8-bit sample.</returns>
        public override double ReadSample(BinaryReader waveReader)
        {
            Byte value = waveReader.ReadByte();
            return ToSample(value, Byte.MinValue, Byte.MaxValue);
        }

        /// <summary>
        /// Writes an 8-bit sample to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The wave writer to write the 8-bit sample to.</param>
        /// <param name="sample">8-bit sample to write.</param>
        public override void WriteSample(BinaryWriter waveWriter, double sample)
        {
            Byte value = (Byte)ToValue(sample, Byte.MinValue, Byte.MaxValue);
            waveWriter.Write(value);
        }
    }
}
