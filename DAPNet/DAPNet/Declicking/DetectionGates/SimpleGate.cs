using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a simple detection gate.
    /// </summary>
    public class SimpleGate : DetectionGate
    {
        /// <summary>
        /// Holds a default detection treshold.
        /// </summary>
        public const double DefaultTreshold = 0.5d;


        /// <summary>
        /// Holds a detection treshold.
        /// </summary>
        private double treshold;


        /// <summary>
        /// Creates a new simple gate with default settings.
        /// </summary>
        public SimpleGate() : this(DefaultTreshold)
        {
        }

        /// <summary>
        /// Creates a new simple gate.
        /// </summary>
        /// <param name="treshold">Detection treshold of the new simple gate.</param>
        public SimpleGate(double treshold)
        {
            this.treshold = treshold;
        }

        /// <summary>
        /// Detection treshold.
        /// </summary>
        public double Treshold
        {
            get 
            { 
                return treshold; 
            }
            set 
            { 
                treshold = value; 
            }
        }

        /// <summary>
        /// Detects samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <returns>Detection made above specified samples.</returns>
        public override DetectionVector Detect(SampleVector samples)
        {
            DetectionVector detection = new DetectionVector(samples.Count);
            for (int i = 0; i < samples.Count; i++)
            {
                detection[i] = Math.Abs(samples[i]) > Treshold;
            }
            return detection;
        }
    }
}
