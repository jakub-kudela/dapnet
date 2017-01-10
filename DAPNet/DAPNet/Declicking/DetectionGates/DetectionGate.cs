
using System.Collections.Generic;
namespace DAPNet
{
    /// <summary>
    /// Represents a detection gate.
    /// </summary>
    public abstract class DetectionGate : Effect
    {
        /// <summary>
        /// Gates the samples.
        /// </summary>
        /// <param name="samples">Samples to gate.</param>
        public override void Process(SampleVector samples)
        {
            DetectionVector detection = Detect(samples);
            Process(samples, detection);
        }

        /// <summary>
        /// Gates the samples with specified detection.
        /// </summary>
        /// <param name="samples">Samples to gate.</param>
        /// <param name="detection">Detection according which to gate.</param>
        public void Process(SampleVector samples, DetectionVector detection)
        {
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] = detection[i] ? samples[i] : SampleConverter.Base;
            }
        }


        /// <summary>
        /// Detects samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <returns>Detection made above specified samples.</returns>
        public abstract DetectionVector Detect(SampleVector samples);

        /// <summary>
        /// Detects samples using modifier.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <param name="modifier">Modifier to modify made detection with.</param>
        /// <returns>Detection made above specified samples modified by specified modifier.</returns>
        public DetectionVector Detect(SampleVector samples, DetectionModifier modifier)
        {
            DetectionVector detection = Detect(samples);
            modifier.Modify(detection);
            return detection;
        }

        /// <summary>
        /// Detects samples using modifiers.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <param name="modifiers">Modifiers to modify made detection with.</param>
        /// <returns>Detection made above specified samples modified by specified modifiers.</returns>
        public DetectionVector Detect(SampleVector samples, List<DetectionModifier> modifiers)
        {
            DetectionVector detection = Detect(samples);
            foreach (DetectionModifier modifier in modifiers)
	        {
                modifier.Modify(detection);
	        }
            return detection;
        }
    }
}
