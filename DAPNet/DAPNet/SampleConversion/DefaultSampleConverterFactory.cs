using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a factory for creating 8-bit or 16-bit sample converters.
    /// </summary>
    public class DefaultSampleConverterFactory : SampleConverterFactory
    {
        /// <summary>
        /// Contains a 8-bit depth.
        /// </summary>
        private const int BitDepth8Bit = 8;

        /// <summary>
        /// Contains a 16-bit depth.
        /// </summary>
        private const int BitDepth16Bit = 16;

        /// <summary>
        /// Gets an instance of 8-bit or 16-bit sample converter.
        /// </summary>
        /// <param name="bitDepth">Bit depth of sample converter to get.</param>
        /// <returns>An instance of sample converter.</returns>
        public override SampleConverter GetSampleConverter(int bitDepth)
        {
            switch (bitDepth)
            {
                case BitDepth8Bit:
                    return new SampleConverter8Bit();
                case BitDepth16Bit:
                    return new SampleConverter16Bit();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
