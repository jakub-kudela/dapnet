
namespace DAPNet
{
    /// <summary>
    /// Represents a high pass filter using discrete approximation
    /// of second derivative of a digital signal.
    /// </summary>
    public class DerivativeFilter : Effect
    {
        /// <summary>
        /// Holds the default derivative order.
        /// </summary>
        public const int DefaultOrder = 1;


        /// <summary>
        /// Holds the derivative order.
        /// </summary>
        private int order;


        /// <summary>
        /// Creates a new filter with default settings.
        /// </summary>
        public DerivativeFilter() : this(DefaultOrder)
        {
        }

        /// <summary>
        /// Creates a new derivative filter.
        /// </summary>
        /// <param name="order">Derivative order of the new filter.</param>
        public DerivativeFilter(int order)
        {
            this.order = order;
        }

        /// <summary>
        /// Derivative order.
        /// </summary>
        public int Order
        {
            get 
            { 
                return order; 
            }
            set 
            { 
                order = value; 
            }
        }

        /// <summary>
        /// Filters samples.
        /// </summary>
        /// <param name="samples">Samples to filter.</param>
        public override void Process(SampleVector samples)
        {
            for (int i = 0; i < Order; i++)
            {
                Derivate(samples);
            }
        }


        /// <summary>
        /// Process samples to the second derivative.
        /// </summary>
        /// <param name="samples">Samples to process to second derivative.</param>
        private void Derivate(SampleVector samples)
        {
            double preceding = SampleConverter.Base;
            for (int i = 0; i < samples.Count; i++)
            {
                double current = samples[i];
                double succeeding = samples[i + 1];
                double derivative = GetDerivative(preceding, current, succeeding);
                samples[i] = derivative;
                preceding = current;
            }
        }

        /// <summary>
        /// Gets the second derivative of sample.
        /// </summary>
        /// <param name="preceding">Preceding sample.</param>
        /// <param name="current">Current sample.</param>
        /// <param name="succeeding">Succeeding sample.</param>
        /// <returns>Second derivative of sample.</returns>
        private double GetDerivative(double preceding, double current, double succeeding)
        {
            return  preceding - (2 * current) + succeeding;
        }
    }
}
