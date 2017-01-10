
namespace DAPNet
{
    /// <summary>
    /// Represents an offline audio effect.
    /// </summary>
    public abstract class Effect
    {
        /// <summary>
        /// Processes samples.
        /// </summary>
        /// <param name="samples">Samples for effect to process.</param>
        public abstract void Process(SampleVector samples);
    }
}
