using System;

namespace DAPNet
{
    /// <summary>
    /// Represents a shifting detection modifier.
    /// </summary>
    public class DetectionShifter : DetectionModifier
    {
        /// <summary>
        /// Represents the direction of shifting.
        /// </summary>
        public enum ShiftDirection
        {
            /// <summary>
            /// Backward shifting direction.
            /// </summary>
            Backward,

            /// <summary>
            /// Forward shifting direction.
            /// </summary>
            Forward
        }

        /// <summary>
        /// Holds a default range parameter.
        /// </summary>
        public const int DefaultParameter = 0;

        /// <summary>
        /// Holds a default shifing direction.
        /// </summary>
        public const ShiftDirection DefaultDirection = ShiftDirection.Forward;


        /// <summary>
        /// Holds a range parameter.
        /// </summary>
        private int parameter;

        /// <summary>
        /// Holds a shifting direction.
        /// </summary>
        private ShiftDirection direction;


        /// <summary>
        /// Creates a new shifting detection modifier with default settings.
        /// </summary>
        public DetectionShifter() : this(DefaultParameter, DefaultDirection)
        {
        }

        /// <summary>
        /// Creates a new shifting detection modifier.
        /// </summary>
        /// <param name="parameter">Range parameter of the new shifting detection modifier.</param>
        public DetectionShifter(int parameter) : this(parameter, DefaultDirection)
        { 
        }

        /// <summary>
        /// Creates a new shifting detection modifier.
        /// </summary>
        /// <param name="direction">Shifting direction of the new shifting detection modifier.</param>
        public DetectionShifter(ShiftDirection direction) : this(DefaultParameter, direction)
        { 
        }

        /// <summary>
        /// Creates a new shifting detection modifier.
        /// </summary>
        /// <param name="parameter">Range parameter of the new shifting detection modifier.</param>
        /// <param name="direction">Shifting direction of the new shifting detection modifier.</param>
        public DetectionShifter(int parameter, ShiftDirection direction)
        {
            this.parameter = parameter;
            this.direction = direction;
        }

        /// <summary>
        /// Range parameter for shifting.
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
        /// Direction for shifting.
        /// </summary>
        public ShiftDirection Direction
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
        /// Shifts detection flags in direction and range.
        /// </summary>
        /// <param name="detection">Detection to shift.</param>
        public override void Modify(DetectionVector detection)
        {
            switch (Direction)
            {
                case ShiftDirection.Backward:
                    ShiftBackwards(detection);
                    break;
                case ShiftDirection.Forward:
                    ShiftForwards(detection);
                    break;
            }
        }


        /// <summary>
        /// Shifts detection backwards.
        /// </summary>
        /// <param name="detection">Detection to shift.</param>
        private void ShiftBackwards(DetectionVector detection)
        {
            for (int i = 0; i < detection.Count; i++)
            {
                detection[i] = detection[Parameter + i];
            }
        }

        /// <summary>
        /// Shifts detection forwards.
        /// </summary>
        /// <param name="detection">Detection to shift.</param>
        private void ShiftForwards(DetectionVector detection)
        {
            for (int i = detection.Count -1; i >= 0; i--)
            {
                detection[i] = detection[-Parameter + i];
            }
        }
    }
}
