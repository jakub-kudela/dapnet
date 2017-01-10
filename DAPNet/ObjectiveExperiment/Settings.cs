using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ObjectiveExperiment
{
    /// <summary>
    /// Holds application settings.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Holds a DE culture info.
        /// </summary>
        internal static readonly CultureInfo deCultureInfo = CultureInfo.GetCultureInfo("de-DE");


        /// <summary>
        /// Samples path string format.
        /// </summary>
        internal const string SamplesPathFormat = "./input/S_{0:00}_{1:00}.wav";
        
        /// <summary>
        /// Analog degradations path string format.
        /// </summary>
        internal const string AnalogDegradationsPathFormat = "./input/A_{0:00}.wav";
        
        /// <summary>
        /// Digital degradations path string format.
        /// </summary>
        internal const string DigitalDegradationsPathFormat = "./input/D_{0:00}.wav";

        /// <summary>
        /// Output path string format.
        /// </summary>
        internal const string OutputPathFormat = "./output/{0}.txt";

        
        /// <summary>
        /// Samples entry string format.
        /// </summary>
        internal const string SamplesEntryFormat = "S_{0:00}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
        
        /// <summary>
        /// Age entry string format.
        /// </summary>
        internal const string AgeEntryFormat = "A_{0:00}\t\t\t\t\t\t\t\tD_{0:00}\t\t\t\t\t\t\t";
        
        /// <summary>
        /// Degradation entry string format.
        /// </summary>
        internal const string DegradationsEntryFormat = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}";

        /// <summary>
        /// Holds console string format.
        /// </summary>
        internal const string ConsoleFormat = "{0}\tS_{1:00}\tA_{2:00}\tD_{2:00}";


        /// <summary>
        /// Holds minimal samples identification.
        /// </summary>
        internal const int MinSamplesID = 1;
        
        /// <summary>
        /// Holds maximal samples identification.
        /// </summary>
        internal const int MaxSamplesID = 12;
        
        /// <summary>
        /// Holds minimal age identification.
        /// </summary>
        internal const int MinAgeID = 1;
        
        /// <summary>
        /// Holds maximal age indetification.
        /// </summary>
        internal const int MaxAgeID = 6;
    }
}
