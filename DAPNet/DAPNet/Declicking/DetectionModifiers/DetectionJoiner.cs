
namespace DAPNet
{
    /// <summary>
    /// Represents a joining detection modifier.
    /// </summary>
    public class DetectionJoiner : DetectionModifier
    {
        /// <summary>
        /// Holds a default detection flags to join.
        /// </summary>
        public static bool DefaultFlag = true;

        /// <summary>
        /// Holds a default range parameter.
        /// </summary>
        public static int DefaultParameter = 25;


        /// <summary>
        /// Holds a detection flag to join.
        /// </summary>
        private bool flag;

        /// <summary>
        /// Holds a range parameter.
        /// </summary>
        private int parameter;


        /// <summary>
        /// Creates a new joining detection modifier with default settings.
        /// </summary>
        public DetectionJoiner() : this(DefaultFlag, DefaultParameter)
        { 
        }

        /// <summary>
        /// Creates a new joining detection modifier.
        /// </summary>
        /// <param name="parameter">Range parameter of the new joining detection modifier.</param>
        public DetectionJoiner(int parameter) : this(DefaultFlag, parameter)
        { 
        }

        /// <summary>
        /// Creates a new joining detection modifier.
        /// </summary>
        /// <param name="flag">Detection flag of the new joining detection modifier.</param>
        /// <param name="parameter">Range parameter of the new joining detection modifier.</param>
        public DetectionJoiner(bool flag, int parameter)
        {
            this.flag = flag;
            this.parameter = parameter;
        }

        /// <summary>
        /// Detection flag for joining.
        /// </summary>
        public bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        /// <summary>
        /// Range parameter for joining.
        /// </summary>
        public int Parameter
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
        /// Joining detection flags closer than range.
        /// </summary>
        /// <param name="detection">Detection to join.</param>
        public override void Modify(DetectionVector detection)
        {
            int? seqIndex = null;
            int seqCount = 0;
            for (int i = 0; i < detection.Count; i++)
            {
                if (detection[i] != Flag && detection[i - 1] == flag)
                {
                    seqIndex = i;
                    seqCount = 0;
                }
                seqCount++;
                if (detection[i] != Flag && detection[i + 1] == flag && seqIndex != null && seqCount < Parameter)
                {
                    SetSeq(detection, seqIndex.Value, seqCount);
                }
            }
        }


        /// <summary>
        /// Sets the specified interval of detection flags to specified flag.
        /// </summary>
        /// <param name="detection">Detection flag.</param>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Number of consecutive detection flags of the interval.</param>
        private void SetSeq(DetectionVector detection, int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                detection[i] = Flag;
            }
        }
    }
}
