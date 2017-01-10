
namespace DAPNet
{
    /// <summary>
    /// Represents an amplifier.
    /// </summary>
    public class Amplifier : Effect
    {
        /// <summary>
        /// Holds the default amplifing parameter.
        /// </summary>
        public const double DefaultParameter = 1.0d;


        /// <summary>
        /// Holds the amplifing parameter.
        /// </summary>
        private double parameter;


        /// <summary>
        /// Creates a new amplifier with default settings.
        /// </summary>
        public Amplifier() : this(DefaultParameter)
        {
        }

        /// <summary>
        /// Creates a new amplifier.
        /// </summary>
        /// <param name="parameter">Amplifing parameter of the new amplifier.</param>
        public Amplifier(double parameter)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// Amplifing parameter.
        /// </summary>
        public double Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }
        
        /// <summary>
        /// Amplifies samples.
        /// </summary>
        /// <param name="samples">Samples to amplify.</param>
        public override void Process(SampleVector samples)
        {
            for (int i = 0; i < samples.Count; i++)
            {
                samples[i] *= Parameter;
            }
        }
    }
}
