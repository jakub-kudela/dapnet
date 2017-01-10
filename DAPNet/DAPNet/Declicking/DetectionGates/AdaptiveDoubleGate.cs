using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPNet
{
    /// <summary>
    /// Represents an adaptive deviation gate with double treshold.
    /// </summary>
    public class AdaptiveDoubleGate : DetectionGate
    {
        /// <summary>
        /// Holds the default big detection treshold parameter.
        /// </summary>
        public const double DefaultBigParamter = 3.0;

        /// <summary>
        /// Holds the default little detection treshold parameter.
        /// </summary>
        public const double DefaultLittleParamter = 2.0;

        /// <summary>
        /// Holds the default half-range parameter.
        /// </summary>
        public const int DefautRange = AdaptiveGate.DefautRange;

        /// <summary>
        /// Holds the default decimation parameter.
        /// </summary>
        public const int DefaultDecimation = AdaptiveGate.DefaultDecimation;

        /// <summary>
        /// Holds the default type of deviation calculation.
        /// </summary>
        public const DeviationGate.DeviationType DefaultType = AdaptiveGate.DefaultType;


        /// <summary>
        /// Holds the big detection treshold parameter.
        /// </summary>
        private double bigParameter;

        /// <summary>
        /// Holds the little detection treshold parameter.
        /// </summary>
        private double littleParameter;

        /// <summary>
        /// Adaptive gate for detection.
        /// </summary>
        private AdaptiveGate adaptiveGate;


        /// <summary>
        /// Creates a new adaptive deviation gate with double treshold with default settings.
        /// </summary>
        public AdaptiveDoubleGate() : this(DefaultBigParamter, DefaultLittleParamter, DefautRange, DefaultDecimation, DefaultType)
        { 
        }

        /// <summary>
        /// Creates a new adaptive deviation gate with double treshold.
        /// </summary>
        /// <param name="bigParameter">Big detection treshold parameter of the new adaptive deviation gate.</param>
        /// <param name="littleParameter">Little detection treshold parameter of the new adaptive deviation gate.</param>
        public AdaptiveDoubleGate(double bigParameter, double littleParameter) : this(bigParameter, littleParameter, DefautRange, DefaultDecimation, DefaultType)
        {
        }

        /// <summary>
        /// Creates a new adaptive deviation gate with double treshold.
        /// </summary>
        /// <param name="bigParameter">Big detection treshold parameter of the new adaptive deviation gate.</param>
        /// <param name="littleParameter">Little detection treshold parameter of the new adaptive deviation gate.</param>
        /// <param name="range">Half-range parameter of the new adaptive deviation gate.</param>
        /// <param name="decimation">Decimation parameter of the new adaptive deviation gate.</param>
        /// <param name="type">Type of deviation calculation of the new adaptive deviation gate.</param>
        public AdaptiveDoubleGate(double bigParameter, double littleParameter, int range, int decimation, DeviationGate.DeviationType type)
        {
            this.bigParameter = bigParameter;
            this.littleParameter = littleParameter;
            adaptiveGate = new AdaptiveGate(range, decimation, type);
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
        /// Half-range parameter.
        /// </summary>
        public int Range
        {
            get
            {
                return adaptiveGate.Range;
            }
            set
            {
                adaptiveGate.Range = value;
            }
        }

        /// <summary>
        /// Decimation parameter.
        /// </summary>
        public int Decimation
        {
            get
            {
                return adaptiveGate.Decimation;
            }
            set
            {
                adaptiveGate.Decimation = value;
            }
        }

        /// <summary>
        /// Type of deviation calculation.
        /// </summary>
        public DeviationGate.DeviationType Type
        {
            get
            {
                return adaptiveGate.Type;
            }
            set
            {
                adaptiveGate.Type = value;
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
            bool precedingFlag = DetectionVector.DefaultFlag;
            for (int i = 0; i < samples.Count; i++)
            {
                double parameter = precedingFlag ? LittleParameter : BigParameter;
                double deviation = adaptiveGate.GetLocalDeviation(samples, i);
                double treshold = parameter * deviation;
                detection[i] = precedingFlag = Math.Abs(samples[i]) > treshold;
            }
            return detection;
        }
    }
}
