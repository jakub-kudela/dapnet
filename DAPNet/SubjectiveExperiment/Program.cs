using System;
using DAPNet;
using System.IO;
using System.Collections.Generic;

namespace SubjectiveExperiment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Subjective experiment settings.
            List<string> degradeds = new List<string>();
            degradeds.Add("mussorgsky");
            degradeds.Add("strauss");
            degradeds.Add("vega");

            List<Declicker> declickers = new List<Declicker>();
            List<DetectionGate> gates = new List<DetectionGate>();
            List<DetectionModifier> modifiers = new List<DetectionModifier>();

            // Naive declicker with its appropriate gate and modifier.
            declickers.Add(new NDeclicker(5));
            gates.Add(new AdaptiveDoubleGate(3.5d, 2.0d));
            modifiers.Add(new DetectionWidener(5, DetectionWidener.WidenDirection.Forward));

            // Kasparis-Lane declicker with its appropriate gate and modifier.
            declickers.Add(new KLDeclicker(1, 5, 8, 8));
            gates.Add(new AdaptiveDoubleGate(3.5d, 2.0d));
            modifiers.Add(new DetectionWidener(1, DetectionWidener.WidenDirection.Bidirectional));

            // Autoregressive declicker with its appropriate gate and modifier.
            declickers.Add(new ARDeclicker(30, 1024));
            gates.Add(new AdaptiveDoubleGate(3.5d, 2.0d));
            modifiers.Add(new DetectionWidener(5, DetectionWidener.WidenDirection.Forward));

            // Sinusoidal autoregressive declicker with its appropriate gate and modifier.
            declickers.Add(new SARDeclicker(5, 20, 512));
            gates.Add(new AdaptiveDoubleGate(3.0d, 2.0d));
            modifiers.Add(new DetectionWidener(5, DetectionWidener.WidenDirection.Forward));

            // Neural network declicker with its appropriate gate and modifier.
            declickers.Add(new NNDeclicker(10, 1000, 5, 0.1, 1024));
            gates.Add(new AdaptiveDoubleGate(3.0d, 2.0d));
            modifiers.Add(new DetectionWidener(0, DetectionWidener.WidenDirection.Bidirectional));


            for (int declickerIndex = 0; declickerIndex < 5; declickerIndex++)
            {
                for (int degradedIndex = 0; degradedIndex < 3; degradedIndex++)
                {
                    Console.WriteLine(Settings.ConsoleFormat, degradedIndex, declickerIndex);

                    Wave degradedWave = new Wave();
                    degradedWave.Read(String.Format(Settings.DegradedPathFormat, degradeds[degradedIndex]));
                    SampleVector degraded = degradedWave.Channels[0];

                    Declicker declicker = declickers[declickerIndex];
                    DetectionGate gate = gates[declickerIndex];
                    DetectionModifier modifier = modifiers[declickerIndex];

                    DetectionVector detection = declicker.Detect(degraded, gate, modifier);
                    declicker.Correct(degraded, detection);

                    degradedWave.Write(String.Format(Settings.CorrectedPathFormat, degradeds[degradedIndex], declickers[declickerIndex].GetType().ToString()));
                }
            }
        }
    }
}
