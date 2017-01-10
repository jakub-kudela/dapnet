
namespace DAPNet
{
    /// <summary>
    /// Represents a factory for creating sample converters.
    /// </summary>
    public abstract class SampleConverterFactory
    {
        /// <summary>
        /// Gets an instance of sample converter.
        /// </summary>
        /// <param name="bitDepth">Bit depth of sample converter.</param>
        /// <returns>An instance of sample converter.</returns>
        public abstract SampleConverter GetSampleConverter(int bitDepth);
    }
}
