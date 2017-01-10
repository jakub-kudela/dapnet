using System.Drawing;
using ZedGraph;


namespace DeclickerDisplay
{
    /// <summary>
    /// Holds application settings.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Holds declicker argument index.
        /// </summary>
        internal const int DeclickerArgIndex = 0;

        /// <summary>
        /// Holds action argument index.
        /// </summary>
        internal const int ActionArgIndex = 1;

        /// <summary>
        /// Holds samples argument index.
        /// </summary>
        internal const int SamplesArgIndex = 2;

        /// <summary>
        /// Holds degradations argument index.
        /// </summary>
        internal const int DegradationsArgIndex = 3;

        /// <summary>
        /// Holds degradation model argument idnex.
        /// </summary>
        internal const int ModelArgIndex = 4;
        
        /// <summary>
        /// Holds required number of aruments.
        /// </summary>
        internal const int ArgsCount = 5;


        /// <summary>
        /// Holds naive declicker label.
        /// </summary>
        internal const string NDeclickerLabel = "n";

        /// <summary>
        /// Holds Kasparis-Lane declicker label.
        /// </summary>
        internal const string KLDeclickerLabel = "kl";

        /// <summary>
        /// Holds autoregressive declicker label.
        /// </summary>
        internal const string ARDeclickerLabel = "ar";

        /// <summary>
        /// Holds neural netowork declicker label.
        /// </summary>
        internal const string NNDeclickerLabel = "nn";

        /// <summary>
        /// Holds sinusoidal declicker lablel.
        /// </summary>
        internal const string SDeclickerLabel = "s";

        /// <summary>
        /// Holds sinusoidal autoregressive declicker label.
        /// </summary>
        internal const string SARDeclickerLabel = "sar";

        /// <summary>
        /// Holds sinusoidal neural network declicker label.
        /// </summary>
        internal const string SNNDeclickerLabel = "snn";


        /// <summary>
        /// Holds detection action label.
        /// </summary>
        internal const string DetectionLabel = "d";

        /// <summary>
        /// Holds correction action label.
        /// </summary>
        internal const string CorrectionLabel = "c";


        /// <summary>
        /// Holds analog degradation model label.
        /// </summary>
        internal const string AnalogLabel = "a";
        
        /// <summary>
        /// Holds digital degradation model label.
        /// </summary>
        internal const string DigitalLabel = "d";


        /// <summary>
        /// Holds detection curve offset.
        /// </summary>
        internal const double DetectionCurveOffset = 0.5;


        /// <summary>
        /// Holds dark red color.
        /// </summary>
        internal static readonly Color DarkRed = Color.FromArgb(180, 0, 0);

        /// <summary>
        /// Holds light red color.
        /// </summary>
        internal static readonly Color LightRed = Color.FromArgb(255, 153, 153);

        /// <summary>
        /// Holds dark blue color.
        /// </summary>
        internal static readonly Color DarkBlue = Color.FromArgb(0, 0, 180);

        /// <summary>
        /// Holds light blue color.
        /// </summary>
        internal static readonly Color LightBlue = Color.FromArgb(153, 204, 255);

        /// <summary>
        /// Holds dark green color.
        /// </summary>
        internal static readonly Color DarkGreen = Color.FromArgb(0, 180, 0);

        /// <summary>
        /// Holds dark grey color.
        /// </summary>
        internal static readonly Color DarkGrey = Color.FromArgb(163, 163, 163);

        /// <summary>
        /// Holds transparent color.
        /// </summary>
        internal static readonly Color Transparet = Color.Transparent;


        /// <summary>
        /// Holds excitations curve color.
        /// </summary>
        internal static readonly Color ExcitationsCurveColor = DarkGrey;

        /// <summary>
        /// Holds corrected curve color.
        /// </summary>
        internal static readonly Color CorrectedCurveColor = DarkGrey;

        /// <summary>
        /// Holds samples curve color.
        /// </summary>
        internal static readonly Color SamplesCurveColor = DarkBlue;

        /// <summary>
        /// Holds degraded curve color.
        /// </summary>
        internal static readonly Color DegradedCurveColor = DarkGreen;

        /// <summary>
        /// Holds degradations curve color.
        /// </summary>
        internal static readonly Color DegradationsCurveColor = DarkRed;

        /// <summary>
        /// Holds analyzed curve fill color.
        /// </summary>
        internal static readonly Fill AnalyzedCurveFill = new Fill(LightRed);

        /// <summary>
        /// Holds reviewed curve fill color.
        /// </summary>
        internal static readonly Fill ReviewedCurveFill = new Fill(LightRed);

        /// <summary>
        /// Holds ideal curve fill color.
        /// </summary>
        internal static readonly Fill IdealCurveFill = new Fill(LightBlue);

        /// <summary>
        /// Holds transparent curve fill color.
        /// </summary>
        internal static readonly Fill TransparentFill = new Fill(Transparet);


        /// <summary>
        /// Holds sample label.
        /// </summary>
        internal const string SampleLabel = "Sample";

        /// <summary>
        /// Holds amplitude label.
        /// </summary>
        internal const string AmplitudeLabel = "Amplitude";

        /// <summary>
        /// Holds excitations label.
        /// </summary>
        internal const string ExcitationsLabel = "Excitations";

        /// <summary>
        /// Holds corrected label.
        /// </summary>
        internal const string CorrectedLabel = "Corrected";

        /// <summary>
        /// Holds samples label.
        /// </summary>
        internal const string SamplesLabel = "Samples";

        /// <summary>
        /// Holds degradations label.
        /// </summary>
        internal const string DegradationsLabel = "Degradations";

        /// <summary>
        /// Holds degraded label.
        /// </summary>
        internal const string DegradedLabel = "Degraded";

        /// <summary>
        /// Holds analyzed label.
        /// </summary>
        internal const string AnalyzedLabel = "Analyzed";

        /// <summary>
        /// Holds reviewed label.
        /// </summary>
        internal const string ReviewedLabel = "Reviewed";

        /// <summary>
        /// Holds ideal label.
        /// </summary>
        internal const string IdealLabel = "Ideal";


        /// <summary>
        /// Holds percentage string format.
        /// </summary>
        internal const string PercentageFormat = "0.00000%";

        /// <summary>
        /// Holds double string format.
        /// </summary>
        internal const string DoubleFormat = "0.000000";


        /// <summary>
        /// Holds shown symbol.
        /// </summary>
        internal const SymbolType ShownSymbol = SymbolType.VDash;

        /// <summary>
        /// Holds hidden symbol.
        /// </summary>
        internal const SymbolType HiddenSymbol = SymbolType.None;
    }
}
