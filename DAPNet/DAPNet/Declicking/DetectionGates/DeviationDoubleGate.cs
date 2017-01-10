using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPNet
{
    /// <summary>
    /// Represents a deviation gate with double treshold.
    /// </summary>
    public class DeviationDoubleGate : DetectionGate
    {
        /// <summary>
        /// Holds the default big detection treshold parameter.
        /// </summary>
        public const double DefaultBigParameter = 3.0d;

        /// <summary>
        /// Holds the default little detection treshold parameter.
        /// </summary>
        public const double DefaultLittleParameter = 2.0d;

        /// <summary>
        /// Holds the default type of deviation calculation.
        /// </summary>
        public const DeviationGate.DeviationType DefaultType = DeviationGate.DefaultType;


        /// <summary>
        /// Holds the big detection treshold parameter.
        /// </summary>
        private double bigParameter;

        /// <summary>
        /// Holds the little detection treshold parameter.
        /// </summary>
        private double littleParameter;

        /// <summary>
        /// Deviation gate for detection.
        /// </summary>
        private DeviationGate deviationGate;


        /// <summary>
        /// Creates a new deviation gate with double treshold with default settings.
        /// </summary>
        public DeviationDoubleGate() : this(DefaultBigParameter, DefaultLittleParameter, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new deviation gate with double treshold.
        /// </summary>
        /// <param name="bigParameter">Big detection treshold parameter of the new deviation gate.</param>
        /// <param name="littleParameter">Little detection treshold parameter of the new deviation gate.</param>
        public DeviationDoubleGate(double bigParameter, double littleParameter) : this(bigParameter, littleParameter, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new deviation gate with double treshold.
        /// </summary>
        /// <param name="bigParameter">Big detection treshold parameter of the new deviation gate.</param>
        /// <param name="littleParameter">Little detection treshold parameter of the new deviation gate.</param>
        /// <param name="type">Type of deviation calculation of the new deviation gate.</param>
        public DeviationDoubleGate(double bigParameter, double littleParameter, DeviationGate.DeviationType type)
        {
            this.bigParameter = bigParameter;
            this.littleParameter = littleParameter;
            deviationGate = new DeviationGate(type);
        }

        /// <summary>
        /// Big detection treshold parameter.
        /// </summary>
        public double BigParameter
        {
            get
            {
                return bigParameter;
            }
            set
            {
                bigParameter = value;
            }
        }

        /// <summary>
        /// Little detection treshold parameter.
        /// </summary>
        public double LittleParameter
        {
            get
            {
                return littleParameter;
            }
            set
            {
                littleParameter = value;
            }
        }

        /// <summary>
        /// Type of deviation calculation.
        /// </summary>
        public DeviationGate.DeviationType Type
        {
            get
            {
                return deviationGate.Type;
            }
            set
            {
                deviationGate.Type = value;
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
            double deviation = deviationGate.GetDeviation(samples);
            bool precedingFlag = DetectionVector.DefaultFlag;
            for (int i = 0; i < samples.Count; i++)
            {
                double parameter = precedingFlag ? LittleParameter : BigParameter;
                double treshold = parameter * deviation;
                detection[i] = precedingFlag = Math.Abs(samples[i]) > treshold;
            }
            return detection;
        }
    }
}
