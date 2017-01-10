using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a simple gate with double treshold.
    /// </summary>
    public class SimpleDoubleGate : DetectionGate
    {
        /// <summary>
        /// Holds a default big detection treshold.
        /// </summary>
        public const double DefaultBigTreshold = 0.5d;

        /// <summary>
        /// Holds a default little detection treshold.
        /// </summary>
        public const double DefaultLittleTreshold = 0.2d;


        /// <summary>
        /// Holds a big detection treshold.
        /// </summary>
        private double bigTreshold;

        /// <summary>
        /// Holds a little detection treshold.
        /// </summary>
        private double littleTreshold;


        /// <summary>
        /// Creates a new simple gate with double treshold with default settings.
        /// </summary>
        public SimpleDoubleGate() : this(DefaultBigTreshold, DefaultLittleTreshold)
        {
        }

        /// <summary>
        /// Creates a new simple gate with double treshold.
        /// </summary>
        /// <param name="bigTreshold">Big detection treshold of the new simple gate.</param>
        /// <param name="littleTreshold">Little detection treshold of the new simple gate.</param>
        public SimpleDoubleGate(double bigTreshold, double littleTreshold)
        {
            this.bigTreshold = bigTreshold;
            this.littleTreshold = littleTreshold;
        }

        /// <summary>
        /// Big detection treshold.
        /// </summary>
        public double BigTreshold
        {
            get
            {
                return bigTreshold;
            }
            set
            {
                bigTreshold = value;
            }
        }

        /// <summary>
        /// Little detection treshold.
        /// </summary>
        public double LittleTreshold
        {
            get
            {
                return littleTreshold;
            }
            set
            {
                littleTreshold = value;
            }
        }

        /// <summary>
        /// Detecs samples.
        /// </summary>
        /// <param name="samples">Samples to get detection of.</param>
        /// <returns>Detection made above specified samples.</returns>
        public override DetectionVector Detect(SampleVector samples)
        {
            DetectionVector detection = new DetectionVector(samples.Count);
            bool precedingFlag = DetectionVector.DefaultFlag;
            for (int i = 0; i < samples.Count; i++)
            {
                double treshold = precedingFlag ? LittleTreshold : BigTreshold;
                detection[i] = precedingFlag = Math.Abs(samples[i]) > treshold;
            }
            return detection;
        }
    }
}
