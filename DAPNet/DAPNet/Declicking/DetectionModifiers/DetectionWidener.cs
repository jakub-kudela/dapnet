
namespace DAPNet
{
    /// <summary>
    /// Represents a widening detection modifier.
    /// </summary>
    public class DetectionWidener : DetectionModifier
    {
        /// <summary>
        /// Represets the direction of widening.
        /// </summary>
        public enum WidenDirection
        {
            /// <summary>
            /// Backward widening direction.
            /// </summary>
            Backward,

            /// <summary>
            /// Forward widening direction.
            /// </summary>
            Forward,

            /// <summary>
            /// Bidirectional widening direction.
            /// </summary>
            Bidirectional
        }


        /// <summary>
        /// Holds a default detection flags to widen.
        /// </summary>
        public const bool DefaultFlag = true;

        /// <summary>
        /// Holds a default range parameter.
        /// </summary>
        public const int DefaultParameter = 20;

        /// <summary>
        /// Holds a default widening direction.
        /// </summary>
        public const WidenDirection DefaultDirection = WidenDirection.Bidirectional;


        /// <summary>
        /// Holds a detection flag to widen.
        /// </summary>
        private bool flag;

        /// <summary>
        /// Holds a range parameter.
        /// </summary>
        private int parameter;

        /// <summary>
        /// Holds a widening direction.
        /// </summary>
        private WidenDirection direction;


        /// <summary>
        /// Creates a new widening detection modifier with default settings.
        /// </summary>
        public DetectionWidener() : this(DefaultParameter)
        {
        }

        /// <summary>
        /// Creates a new widening detection modifier.
        /// </summary>
        /// <param name="parameter">Range parameter of the new widening detection modifier.</param>
        public DetectionWidener(int parameter) : this(parameter, DefaultDirection)
        {
        }

        /// <summary>
        /// Creates a new widening detection modifier.
        /// </summary>
        /// <param name="direction">Widening direction of the new shifting detection modifier.</param>
        public DetectionWidener(WidenDirection direction) : this(DefaultParameter, direction)
        { 
        }

        /// <summary>
        /// Creates a new widening detection modifier.
        /// </summary>
        /// <param name="parameter">Range parameter of the new widening detection modifier.</param>
        /// <param name="direction">Widening direction of the new widening detection modifier.</param>
        public DetectionWidener(int parameter, WidenDirection direction) : this(DefaultFlag, parameter, direction)
        { 
        }

        /// <summary>
        /// Creates a new widening detection modifier.
        /// </summary>
        /// <param name="flag">Detection flag of the new widening detection modifier.</param>
        /// <param name="parameter">Range parameter of the new widening detection modifier.</param>
        /// <param name="direction">Widening direction of the new widening detection modifier.</param>
        public DetectionWidener(bool flag, int parameter, WidenDirection direction)
        {
            this.flag = flag;
            this.parameter = parameter;
            this.direction = direction;
        }

        /// <summary>
        /// Detection flag for widening.
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
        /// Range parameter for widening.
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
        /// Direction for widening.
        /// </summary>
        public WidenDirection Direction
        {
            get 
            { 
                return direction; 
            }
            set
            { 
                direction = value; 
            }
        }

        /// <summary>
        /// Widens detection flags in direction and range.
        /// </summary>
        /// <param name="detection">Detection to widen.</param>
        public override void Modify(DetectionVector detection)
        {
            switch (direction)
            {
                case WidenDirection.Backward:
                    WidenBackwards(detection);
                    break;
                case WidenDirection.Forward:
                    WidenForward(detection);
                    break;
                case WidenDirection.Bidirectional:
                    WidenBackwards(detection);
                    WidenForward(detection);
                    break;
            }
        }


        /// <summary>
        /// Widens detection backwards.
        /// </summary>
        /// <param name="detection">Detection to widen.</param>
        private void WidenBackwards(DetectionVector detection)
        {
            int numWindens = 0;
            for (int i = detection.Count - 1; i >= 0; i--)
            {
                if (detection[i])
                {
                    numWindens = parameter;
                }
                else if (numWindens > 0)
                {
                    detection[i] = true;
                    numWindens--;
                }
            }
        }

        /// <summary>
        /// Widens detection forwards.
        /// </summary>
        /// <param name="detection">Detection to widen.</param>
        private void WidenForward(DetectionVector detection)
        {
            int numWidens = 0;
            for (int i = 0; i < detection.Count; i++)
            {
                if (detection[i])
                {
                    numWidens = parameter;
                }
                else if (numWidens > 0)
                {
                    detection[i] = true;
                    numWidens--;
                }
            }
        }
    }
}
