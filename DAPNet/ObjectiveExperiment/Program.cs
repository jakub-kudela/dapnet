using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DAPNet;

namespace ObjectiveExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Objective experiment settings.
            DetectionGate gate = new AdaptiveDoubleGate(3.0d, 2.0d, 20, 50, DeviationGate.DeviationType.Approximation);
            DetectionModifier modifier = new DetectionJoiner(true, 25);
            SamplesDegrader degrader = new SamplesDegrader(gate, modifier);
            Declicker reviewer = new NDeclicker(5);
            Stopwatch stopWatch = new Stopwatch();

            List<Declicker> declickers = new List<Declicker>();
            declickers.Add(new NDeclicker(5));
            declickers.Add(new KLDeclicker(10, 20, 20));
            declickers.Add(new ARDeclicker(20, 1024));
            declickers.Add(new SARDeclicker(11, 20, 1024));
            declickers.Add(new NNDeclicker(20, 100, 5, 0.1d, 512));

            foreach (Declicker declicker in declickers)
            {
                string outputPath = String.Format(Settings.OutputPathFormat, declicker.GetType().ToString());
                StreamWriter output = new StreamWriter(outputPath);

                for (int samplesIndex = Settings.MinSamplesID; samplesIndex <= Settings.MaxSamplesID; samplesIndex++)
                {
                    output.WriteLine(Settings.SamplesEntryFormat, samplesIndex);

                    for (int ageIndex = Settings.MinAgeID; ageIndex <= Settings.MaxAgeID; ageIndex++)
                    {
                        output.WriteLine(Settings.AgeEntryFormat, ageIndex);
                        Console.WriteLine(Settings.ConsoleFormat, declicker.GetType().ToString(), samplesIndex, ageIndex);

                        string samplesPath = String.Format(Settings.SamplesPathFormat, samplesIndex, ageIndex);
                        Wave samplesWave = new Wave();
                        samplesWave.Read(samplesPath);
                        SampleVector samples = samplesWave.Channels[0];

                        string analogDegradationPath = String.Format(Settings.AnalogDegradationsPathFormat, ageIndex);
                        Wave analogDegradationsWave = new Wave();
                        analogDegradationsWave.Read(analogDegradationPath);
                        SampleVector analogDegradations = analogDegradationsWave.Channels[0];
                        SampleVector analogDegraded = samples.Clone();
                        DetectionVector analogIdeal = degrader.Degrade(analogDegraded, analogDegradations, SamplesDegrader.ClickType.Analog);

                        stopWatch.Restart();
                        SampleVector analogExcitations = declicker.GetExcitations(analogDegraded);
                        double analogExcitationTime = stopWatch.ElapsedMilliseconds;
                        DetectionVector analogAnalyzed = gate.Detect(analogExcitations, modifier);
                        double analogD_Effectivity = analogAnalyzed.GetEffectivity(analogIdeal, 0, analogIdeal.Count);
                        double analogN_Effectivity = analogAnalyzed.GetEffectivity(analogIdeal, false, 0, analogIdeal.Count);
                        double analogP_Effectivity = analogAnalyzed.GetEffectivity(analogIdeal, true, 0, analogIdeal.Count);

                        stopWatch.Restart();
                        declicker.Correct(analogDegraded, analogIdeal);
                        double analogCorrectionTime = stopWatch.ElapsedMilliseconds;
                        double analogMean = analogDegraded.GetDiffMean(samples, 0, samples.Count);
                        double analogDeviation = analogDegraded.GetDiffDeviation(samples, 0, samples.Count);
                        DetectionVector analogReviewed = reviewer.Detect(analogDegraded, gate, modifier);
                        double analogC_Effectivity = 1 - analogReviewed.GetEffectivity(analogIdeal, true, 0, analogIdeal.Count);

                        string digitalDegradationPath = String.Format(Settings.DigitalDegradationsPathFormat, ageIndex);
                        Wave digitalDegradationsWav = new Wave();
                        digitalDegradationsWav.Read(digitalDegradationPath);
                        SampleVector digitalDegradations = digitalDegradationsWav.Channels[0];
                        SampleVector digitalDegraded = samples.Clone();
                        DetectionVector digitalIdeal = degrader.Degrade(digitalDegraded, digitalDegradations, SamplesDegrader.ClickType.Digital);

                        stopWatch.Restart();
                        SampleVector digitalExcitations = declicker.GetExcitations(digitalDegraded);
                        double digitalExcitationTime = stopWatch.ElapsedMilliseconds;
                        DetectionVector digitalAnalyzed = gate.Detect(digitalExcitations, modifier);
                        double digitalD_Effectivity = digitalAnalyzed.GetEffectivity(digitalIdeal, 0, analogIdeal.Count);
                        double digitalN_Effectivity = digitalAnalyzed.GetEffectivity(digitalIdeal, false, 0, analogIdeal.Count);
                        double digitalP_Effectivity = digitalAnalyzed.GetEffectivity(digitalIdeal, true, 0, analogIdeal.Count);

                        stopWatch.Restart();
                        declicker.Correct(digitalDegraded, digitalIdeal);
                        double digitalCorrectionTime = stopWatch.ElapsedMilliseconds;
                        double digitalMean = digitalDegraded.GetDiffMean(samples, 0, samples.Count);
                        double digitalDeviation = digitalDegraded.GetDiffDeviation(samples, 0, samples.Count);
                        DetectionVector digitalReviewed = reviewer.Detect(digitalDegraded, gate, modifier);
                        double digitalC_Effectivity = 1 - digitalReviewed.GetEffectivity(digitalIdeal, true, 0, digitalIdeal.Count);

                        object[] DegradationsEntryObjects = new Object[] { analogExcitationTime, analogD_Effectivity, analogN_Effectivity, analogP_Effectivity, 
                                                                            analogCorrectionTime, analogC_Effectivity, analogMean, analogDeviation, 
                                                                            digitalExcitationTime, digitalD_Effectivity, digitalN_Effectivity, digitalP_Effectivity, 
                                                                            digitalCorrectionTime, digitalC_Effectivity, digitalMean, digitalDeviation };
                        output.WriteLine(String.Format(Settings.deCultureInfo, Settings.DegradationsEntryFormat, DegradationsEntryObjects));
                    }
                    output.WriteLine();
                }
                output.Close();
            }
        }
    }
}
