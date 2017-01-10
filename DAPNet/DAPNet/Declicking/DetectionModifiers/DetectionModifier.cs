
namespace DAPNet
{
    /// <summary>
    /// Represents a detection modifier.
    /// </summary>
    public abstract class DetectionModifier
    {
        /// <summary>
        /// Modifies the detection.
        /// </summary>
        /// <param name="detection">Detection to modify.</param>
        public abstract void Modify(DetectionVector detection);
    }
}
