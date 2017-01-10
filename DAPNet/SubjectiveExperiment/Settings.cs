
namespace SubjectiveExperiment
{
    /// <summary>
    /// Holds application settings.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Holds degraded path string format.
        /// </summary>
        internal const string DegradedPathFormat = "./input/{0}.wav";

        /// <summary>
        /// Holds corrected path string format.
        /// </summary>
        internal const string CorrectedPathFormat = "./output/{0}_{1}.wav";

        /// <summary>
        /// Holds console string format.
        /// </summary>
        internal const string ConsoleFormat = "Wave:{0} Declicker:{1}";
    }
}
