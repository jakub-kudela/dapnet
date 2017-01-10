using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a samples degrader.
    /// </summary>
    public class SamplesDegrader
    {
        /// <summary>
        /// Represents the type of degradations model.
        /// </summary>
        public enum ClickType
        {
            /// <summary>
            /// Analog additive degradation model.
            /// </summary>
            Analog,

            /// <summary>
            /// Digital replacement degradation model.
            /// </summary>
            Digital
        }


        /// <summary>
        /// Detection gate used in degraded samples detection.
        /// </summary>
        DetectionGate gate;

        /// <summary>
        /// Detection modifier used in degraded samples detection.
        /// </summary>
        DetectionModifier modifier;


        /// <summary>
        /// Creates a new sample click degrader.
        /// </summary>
        /// <param name="gate">Detection gate to be used in degraded samples detection.</param>
        /// <param name="modifier">Detection modifier to be used in degraded samples detection.</param>
        public SamplesDegrader(DetectionGate gate, DetectionModifier modifier)
        {
            this.gate = gate;
            this.modifier = modifier;
        }

        /// <summary>
        /// Detection gate used in degraded samples detection.
        /// </summary>
        public DetectionGate Gate
        {
            get
            {
                return gate;
            }
            set
            {
                gate = value;
            }
        }

        /// <summary>
        /// Detection modifier used in degraded samples detection.
        /// </summary>
        public DetectionModifier Modifier
        {
            get
            {
                return modifier;
            }
            set
            {
                modifier = value;
            }
        }

        /// <summary>
        /// Degrades samples with specified degradations.
        /// </summary>
        /// <param name="samples">Samples to degrade.</param>
        /// <param name="degradations">Degradations to degrade samples with.</param>
        /// <param name="type">Type of degradation model.</param>
        /// <returns>Detection of degradations applied to samples.</returns>
        public DetectionVector Degrade(SampleVector samples, SampleVector degradations, ClickType type)
        {
            DetectionVector detection = gate.Detect(degradations, modifier);
            SampleVector gatedDegradations = degradations.Clone();
            gate.Process(gatedDegradations, detection);

            switch (type)
            {
                case ClickType.Analog:
                    break;
                case ClickType.Digital:
                    detection.Invert();
                    gate.Process(samples, detection);
                    detection.Invert();
                    break;
                default:
                    throw new ArgumentException();
            }

            samples.Admix(gatedDegradations);
            return detection;
        }
    }
}
