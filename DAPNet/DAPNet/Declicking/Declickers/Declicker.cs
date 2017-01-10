using System.Collections.Generic;

namespace DAPNet
{
    /// <summary>
    /// Represents a declicking algorithm.
    /// </summary>
    public abstract class Declicker
    {
        /// <summary>
        /// Gets excitations out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get excitations of.</param>
        /// <returns>Excitations of specified samples.</returns>
        public abstract SampleVector GetExcitations(SampleVector samples);

        /// <summary>
        /// Detects degraded samples out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of degraded samples from.</param>
        /// <param name="gate">Gate for detection.</param>
        /// <returns>Detection of degraded samples out of specified samples.</returns>
        public DetectionVector Detect(SampleVector samples, DetectionGate gate)
        {
            SampleVector excitations = GetExcitations(samples);
            DetectionVector detection = gate.Detect(excitations);
            return detection;
        }

        /// <summary>
        /// Detects degraded samples out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of degraded samples from.</param>
        /// <param name="gate">Gate for detection.</param>
        /// <param name="modifier">Modifier for detection.</param>
        /// <returns>Detection of degraded samples out of specified samples.</returns>
        public DetectionVector Detect(SampleVector samples, DetectionGate gate, DetectionModifier modifier)
        {
            DetectionVector detection = Detect(samples, gate);
            modifier.Modify(detection);
            return detection;
        }

        /// <summary>
        /// Detects degraded samples out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of degraded samples from.</param>
        /// <param name="gate">Gate for detection.</param>
        /// <param name="modifiers">Modifiers for detection.</param>
        /// <returns>Detection of degraded samples out of specified samples.</returns>
        public DetectionVector Detect(SampleVector samples, DetectionGate gate, List<DetectionModifier> modifiers)
        {
            DetectionVector detection = Detect(samples, gate);
            foreach (DetectionModifier modifier in modifiers)
            {
                modifier.Modify(detection);
            }
            return detection;
        }

        /// <summary>
        /// Corrects specified degraded samples.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples to correct.</param>
        public abstract void Correct(SampleVector samples, DetectionVector detection);


        /// <summary>
        /// Fadingly merges two sample collections according to specified detection.
        /// </summary>
        /// <param name="samples">Fist samples to merge.</param>
        /// <param name="rSamples">Second samples to merge.</param>
        /// <param name="detection">Detection according which to merge.</param>
        /// <returns>Merged sample collection.</returns>
        protected SampleVector Merge(SampleVector samples, SampleVector rSamples, DetectionVector detection)
        {
            SampleVector merged = new SampleVector(samples.Count);
            int[] g = GetG(detection);
            int[] o = GetO(detection);

            for (int i = 0; i < merged.Count; i++)
            {
                double v_i = (double)o[i] / (g[i] + 1);
                merged[i] = (1 - v_i) * samples[i] + v_i * rSamples[i];
            }
            return merged;
        }

        /// <summary>
        /// Get special integer gating signal of specified detection.
        /// </summary>
        /// <param name="detection">Detection to get the gating signal of.</param>
        /// <returns>Special integer gating signal.</returns>
        protected int[] GetG(DetectionVector detection)
        {
            int[] g = new int[detection.Count];
            int seqLength = 0;
            for (int i = 0; i < detection.Count; i++)
            {
                g[i] += seqLength = detection[i] ? ++seqLength : 0;
            }
            seqLength = 0;
            for (int i = detection.Count - 1; i >= 0; i--)
            {
                g[i] += seqLength = detection[i] ? ++seqLength : 0;
                g[i] = detection[i] ? --g[i] : g[i];
            }
            return g;
        }

        /// <summary>
        /// Gets special integer sequential of specified detection.
        /// </summary>
        /// <param name="detection">Detection to get the seqential signal of.</param>
        /// <returns>Special integer sequential signal.</returns>
        protected int[] GetO(DetectionVector detection)
        {
            int[] o = new int[detection.Count];
            int seqLength = 0;
            for (int i = 0; i < detection.Count; i++)
            {
                o[i] = seqLength = detection[i] ? ++seqLength : 0;
            }
            return o;
        }
    }
}
