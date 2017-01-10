using DAPNet;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates wave representation for degraded wave.
            Wave wave = new Wave();
            // Loads wave degraded wave.
            wave.Read("degraded.wav");

            // Creates an autoregressive declicker with the given order 
            // of its AR model and the block size for computation.
            ARDeclicker arDeclicker = new ARDeclicker(20, 256);
            // Creates a simple deviation gate with the given parameter for detection 
            // of degraded samples that exceed a value of the parameter multiplied by 
            // a deviation of a given excitation signal.
            DeviationGate deviationSimpleGate = new DeviationGate(6.0);
            // Creates a detection modifier for widening of sequences of detected 
            // degraded samples by the given parameters of flag, distance and direction.
            DetectionWidener detectionWidener = new DetectionWidener(true, 10, DetectionWidener.WidenDirection.Bidirectional);
            // Corrects every channel of degraded wav.
            foreach (SampleVector degradedChannel in wave.Channels)
            {
                // Performs detection of degraded samples of actual channel with 
                // prepared declicker, gate and modifier.
                DetectionVector detection = arDeclicker.Detect(degradedChannel, deviationSimpleGate, detectionWidener);
                // Performs correction of channel with obtained detection.
                arDeclicker.Correct(degradedChannel, detection);
            }

            // Creates a peak normalizing effect with the given parameter
            // for peak normalization of signals to the given parameter.
            PeakNormalizer peakNormalizer = new PeakNormalizer(SampleConverter.UpperBound);
            // Peak normalizes every channel of corrected wav.
            foreach (SampleVector correctedChannel in wave.Channels)
            {
                // Peak normalizes actual channel.
                peakNormalizer.Process(correctedChannel);
            }

            // Saves wave with corrected and normalized channels.
            wave.Write("corrected.wav");
        }
    }
}
