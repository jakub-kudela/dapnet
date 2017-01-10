using System;
using System.Windows.Forms;
using DAPNet;

namespace DeclickerDisplay
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Settings for declicker inspector.
            DetectionGate gate = new AdaptiveDoubleGate();
            DetectionModifier modifier = new DetectionJoiner();
            SamplesDegrader degrader = new SamplesDegrader(gate, modifier);
            NDeclicker reviewer = new NDeclicker();            
            PeakNormalizer normalizer = new PeakNormalizer();


            if (args.Length != Settings.ArgsCount)
            {
                throw new ArgumentException("ERROR: Arguments!");
            }

            Wave samplesWave = new Wave();
            Wave degradationsWave = new Wave();
            try
            {
                samplesWave.Read(args[Settings.SamplesArgIndex]);
                degradationsWave.Read(args[Settings.DegradationsArgIndex]);
            }
            catch
            {
                throw new ArgumentException("ERROR: File!");
            }
            SampleVector samples = samplesWave.Channels[0];
            SampleVector degradations = degradationsWave.Channels[0];

            DetectionVector ideal = null;
            SampleVector degraded = samples.Clone();
            switch (args[Settings.ModelArgIndex])
            {
                case Settings.AnalogLabel:
                    {
                        ideal = degrader.Degrade(degraded, degradations, SamplesDegrader.ClickType.Analog);
                        break;
                    }
                case Settings.DigitalLabel:
                    {
                        ideal = degrader.Degrade(degraded, degradations, SamplesDegrader.ClickType.Digital);
                        break;
                    }
                default:
                    throw new ArgumentException("ERROR: Action!");
            }

            Declicker declicker = null;            
            switch (args[Settings.DeclickerArgIndex])
            {
                case Settings.NDeclickerLabel:
                    declicker = new NDeclicker();
                    break;
                case Settings.KLDeclickerLabel:
                    declicker = new KLDeclicker();
                    break;
                case Settings.ARDeclickerLabel:
                    declicker = new ARDeclicker();
                    break;
                case Settings.NNDeclickerLabel:
                    declicker = new NNDeclicker();
                    break;
                case Settings.SDeclickerLabel:
                    declicker = new SDeclicker();
                    break;
                case Settings.SARDeclickerLabel:
                    declicker = new SARDeclicker();
                    break;
                case Settings.SNNDeclickerLabel:
                    declicker = new SNNDeclicker();
                    break;
                default:
                    throw new ArgumentException("ERROR: Declicker!");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            switch (args[Settings.ActionArgIndex])
            {
                case Settings.DetectionLabel:
                    SampleVector excitations = declicker.GetExcitations(degraded);
                    normalizer.Process(excitations);
                    DetectionVector analyzed = gate.Detect(excitations, modifier);
                    Application.Run(new DetectionForm(excitations, samples, degraded, degradations, analyzed, ideal));
                    break;
                case Settings.CorrectionLabel:
                    SampleVector corrected = degraded.Clone();
                    declicker.Correct(corrected, ideal);
                    DetectionVector reviewed = reviewer.Detect(corrected, gate, modifier);
                    Application.Run(new CorrectionForm(corrected, samples, degraded, degradations, reviewed, ideal));
                    break;
                default:
                    throw new ArgumentException("ERROR: Action!");
            }
        }
    }
}
