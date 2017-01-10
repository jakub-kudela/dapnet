using System;
using System.Drawing;
using System.Windows.Forms;
using DAPNet;
using ZedGraph;

namespace DeclickerDisplay
{
    /// <summary>
    /// Represents the detection form.
    /// </summary>
    public partial class DetectionForm : Form
    {
        /// <summary>
        /// Holds the excitation curve.
        /// </summary>
        private LineItem excitationsCurve;

        /// <summary>
        /// Holds the samples curve.
        /// </summary>
        private LineItem samplesCurve;

        /// <summary>
        /// Holds the degraded curve.
        /// </summary>
        private LineItem degradedCurve;

        /// <summary>
        /// Holds the degradations curve.
        /// </summary>
        private LineItem degradationsCurve;

        /// <summary>
        /// Holds the analyzed curve.
        /// </summary>
        private LineItem analyzedCurve;

        /// <summary>
        /// Holds the ideal curve.
        /// </summary>
        private LineItem idealCurve;


        /// <summary>
        /// Holds the excitations.
        /// </summary>
        private SampleVector excitations;

        /// <summary>
        /// Holds the samples.
        /// </summary>
        private SampleVector samples;

        /// <summary>
        /// Holds the degraded.
        /// </summary>
        private SampleVector degraded;

        /// <summary>
        /// Holds the degradations.
        /// </summary>
        private SampleVector degradations;

        /// <summary>
        /// Holds the analyzed.
        /// </summary>
        private DetectionVector analyzed;

        /// <summary>
        /// Holds the ideal.
        /// </summary>
        private DetectionVector ideal;


        /// <summary>
        /// Creates a detection form.
        /// </summary>
        /// <param name="excitations">Excitations.</param>
        /// <param name="samples">Samples.</param>
        /// <param name="degraded">Degraded.</param>
        /// <param name="degradations">Degradations.</param>
        /// <param name="analyzed">Analyzed.</param>
        /// <param name="ideal">Ideal.</param>
        public DetectionForm(SampleVector excitations, SampleVector samples, SampleVector degraded, SampleVector degradations, DetectionVector analyzed, DetectionVector ideal)
        {
            InitializeComponent();

            detectionGraph.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler(detectionGraph_ScrollDoneEvent);
            detectionGraph.ZoomEvent += new ZedGraphControl.ZoomEventHandler(detectionGraph_ZoomEvent);

            this.excitations = excitations;
            this.samples = samples;
            this.degradations = degradations;
            this.degraded = degraded;
            this.analyzed = analyzed;
            this.ideal = ideal;
        }

        /// <summary>
        /// Loads the detection form.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void DetectionForm_Load(object sender, EventArgs e)
        {
            InicializeDetectionGraph();
            InicializeDisplaySettings();

            ResetViewDisplaySettings(0, samples.Count);
            ResetCurvesDisplaySettings();
            ResetStatistics(0, samples.Count);
            ResetDetectionGraphCurves();
            ResetDetectionGraph(0, samples.Count);
        }

        /// <summary>
        /// Inicializes display settings.
        /// </summary>
        private void InicializeDisplaySettings()
        {
            displayExcitationsCheckBox.Checked = true;
            displaySamplesCheckBox.Checked = true;
            displayDegradedCheckBox.Checked = true;
            displayDegradationsCheckBox.Checked = true;
            displayAnalyzedCheckBox.Checked = true;
            displayIdealCheckBox.Checked = true;
        }

        /// <summary>
        /// Inicializes detection graph.
        /// </summary>
        private void InicializeDetectionGraph()
        {
            detectionGraph.GraphPane.Title.IsVisible = false;
            detectionGraph.GraphPane.XAxis.Title.Text = Settings.SampleLabel;
            detectionGraph.GraphPane.YAxis.Title.Text = Settings.AmplitudeLabel;

            InicializeExcitationsCurve();
            InicializeSamplesCurve();
            InicializeDegradedCurve();
            InicializeDegradationsCurve();
            InicializeAnalyzedCurve();
            InicializeIdealCurve();
        }

        /// <summary>
        /// Inicializes excitations curve.
        /// </summary>
        private void InicializeExcitationsCurve()
        {
            PointPairList excitationsPoints = new PointPairList();
            for (int i = 0; i < samples.Count; i++)
            {
                excitationsPoints.Add(i, excitations[i]);
            }

            excitationsCurve = detectionGraph.GraphPane.AddCurve(Settings.ExcitationsLabel, excitationsPoints, Settings.ExcitationsCurveColor);
            excitationsCurve.Line.IsSmooth = true;
        }

        /// <summary>
        /// Inicializes samples curve.
        /// </summary>
        private void InicializeSamplesCurve()
        {
            PointPairList samplesPoints = new PointPairList();
            for (int i = 0; i < samples.Count; i++)
            {
                samplesPoints.Add(i, samples[i]);
            }

            samplesCurve = detectionGraph.GraphPane.AddCurve(Settings.SamplesLabel, samplesPoints, Settings.SamplesCurveColor);
            samplesCurve.Line.IsSmooth = true;
        }

        /// <summary>
        /// Inicializes degraded curve.
        /// </summary>
        private void InicializeDegradedCurve()
        {
            PointPairList degradedPoints = new PointPairList();
            for (int i = 0; i < degraded.Count; i++)
            {
                degradedPoints.Add(i, degraded[i]);
            }

            degradedCurve = detectionGraph.GraphPane.AddCurve(Settings.DegradedLabel, degradedPoints, Settings.DegradedCurveColor);
            degradedCurve.Line.IsSmooth = true;
        }

        /// <summary>
        /// Inicializes degradations curve.
        /// </summary>
        private void InicializeDegradationsCurve()
        {
            PointPairList degradationsPoints = new PointPairList();
            for (int i = 0; i < degradations.Count; i++)
            {
                degradationsPoints.Add(i, degradations[i]);
            }

            degradationsCurve = detectionGraph.GraphPane.AddCurve(Settings.DegradationsLabel, degradationsPoints, Settings.DegradationsCurveColor);
            degradationsCurve.Line.IsSmooth = true;
        }

        /// <summary>
        /// Inicializes analyzed curve.
        /// </summary>
        private void InicializeAnalyzedCurve()
        {
            PointPairList analyzedDetectionPoints = new PointPairList();
            for (int i = 0; i < analyzed.Count; i++)
            {
                double value = analyzed[i] ? SampleConverter.UpperBound : 0;
                analyzedDetectionPoints.Add(i - Settings.DetectionCurveOffset, value);
            }

            analyzedCurve = detectionGraph.GraphPane.AddCurve(Settings.AnalyzedLabel, analyzedDetectionPoints, Color.Transparent);
            analyzedCurve.Line.Fill = Settings.AnalyzedCurveFill;
            analyzedCurve.Line.StepType = StepType.ForwardStep;
        }

        /// <summary>
        /// Inicializes ideal curve.
        /// </summary>
        private void InicializeIdealCurve()
        {
            PointPairList idealDetectionPoints = new PointPairList();
            for (int i = 0; i < ideal.Count; i++)
            {
                double value = ideal[i] ? SampleConverter.LowerBound : 0;
                idealDetectionPoints.Add(i - Settings.DetectionCurveOffset, value);
            }

            idealCurve = detectionGraph.GraphPane.AddCurve(Settings.IdealLabel, idealDetectionPoints, Color.Transparent);
            idealCurve.Line.Fill = Settings.IdealCurveFill;
            idealCurve.Line.StepType = StepType.ForwardStep;
        }

        /// <summary>
        /// Resets view display settings for specified interval.
        /// </summary>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Length of the interval.</param>
        private void ResetViewDisplaySettings(int index, int count)
        {
            indexTextBox.Text = index.ToString();
            indexTextBox.Refresh();

            countTextBox.Text = count.ToString();
            countTextBox.Refresh();
        }

        /// <summary>
        /// Resets curves display settings.
        /// </summary>
        private void ResetCurvesDisplaySettings()
        {
            displayExcitationsSymbolsCheckBox.Enabled = displayExcitationsCheckBox.Checked;
            displayExcitationsSymbolsCheckBox.Refresh();

            displaySamplesSymbolsCheckBox.Enabled = displaySamplesCheckBox.Checked;
            displaySamplesSymbolsCheckBox.Refresh();

            displayDegradedSymbolsCheckBox.Enabled = displayDegradedCheckBox.Checked;
            displayDegradedSymbolsCheckBox.Refresh();

            displayDegradationsSymbolsCheckBox.Enabled = displayDegradationsCheckBox.Checked;
            displayDegradationsSymbolsCheckBox.Refresh();
        }

        /// <summary>
        /// Resets statistics for specified internal.
        /// </summary>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Length of the interval.</param>
        private void ResetStatistics(int index, int count)
        {
            double a_Number = analyzed.GetNumSeqs(true, index, count);
            a_NumberTextBox.Text = a_Number.ToString();
            a_NumberTextBox.Refresh();

            double a_Length = analyzed.GetAvgSeqLength(true, index, count);
            a_LengthTextBox.Text = a_Length.ToString(Settings.DoubleFormat);
            a_LengthTextBox.Refresh();

            double i_Number = ideal.GetNumSeqs(true, index, count);
            i_NumberTextBox.Text = i_Number.ToString();
            i_NumberTextBox.Refresh();

            double i_Length = ideal.GetAvgSeqLength(true, index, count);
            i_LengthTextBox.Text = i_Length.ToString(Settings.DoubleFormat); 
            i_LengthTextBox.Refresh();

            double effectivity = analyzed.GetEffectivity(ideal, index, count);
            d_EffectivityTextBox.Text = effectivity.ToString(Settings.PercentageFormat);
            d_EffectivityTextBox.Refresh();

            double n_Effectivity = analyzed.GetEffectivity(ideal, false, index, count);
            n_EffectivityTextBox.Text = n_Effectivity.ToString(Settings.PercentageFormat);
            n_EffectivityTextBox.Refresh();

            double p_Effectivity = analyzed.GetEffectivity(ideal, true, index, count);
            p_EffectivityTextBox.Text = p_Effectivity.ToString(Settings.PercentageFormat);
            p_EffectivityTextBox.Refresh();
        }

        /// <summary>
        /// Resets detection graph for specified interval.
        /// </summary>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Length of the interval.</param>
        private void ResetDetectionGraph(int index, int count)
        {
            detectionGraph.GraphPane.YAxis.Scale.Min = SampleConverter.LowerBound;
            detectionGraph.GraphPane.YAxis.Scale.Max = SampleConverter.UpperBound;

            detectionGraph.GraphPane.XAxis.Scale.Min = index;
            int endIndex = index + count - 1;
            detectionGraph.GraphPane.XAxis.Scale.Max = endIndex;

            detectionGraph.AxisChange();
            detectionGraph.Refresh();
        }

        /// <summary>
        /// Resets detection graph curves.
        /// </summary>
        private void ResetDetectionGraphCurves()
        {
            excitationsCurve.Symbol.Type = displayExcitationsSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            excitationsCurve.Color = displayExcitationsCheckBox.Checked ? Settings.ExcitationsCurveColor : Color.Transparent;

            samplesCurve.Symbol.Type = displaySamplesSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            samplesCurve.Color = displaySamplesCheckBox.Checked ? Settings.SamplesCurveColor : Color.Transparent;

            degradedCurve.Symbol.Type = displayDegradedSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            degradedCurve.Color = displayDegradedCheckBox.Checked ? Settings.DegradedCurveColor : Color.Transparent;

            degradationsCurve.Symbol.Type = displayDegradationsSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            degradationsCurve.Color = displayDegradationsCheckBox.Checked ? Settings.DegradationsCurveColor : Color.Transparent;

            analyzedCurve.Line.Fill = displayAnalyzedCheckBox.Checked ? Settings.AnalyzedCurveFill : Settings.TransparentFill;
            idealCurve.Line.Fill = displayIdealCheckBox.Checked ? Settings.IdealCurveFill : Settings.TransparentFill;

            detectionGraph.Refresh();
        }

        /// <summary>
        /// Display all button click method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void displayAllButton_Click(object sender, EventArgs e)
        {
            ResetViewDisplaySettings(0, samples.Count);
            ResetStatistics(0, samples.Count);
            ResetDetectionGraph(0, samples.Count);
        }

        /// <summary>
        /// Display button click method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void displayButton_Click(object sender, EventArgs e)
        {
            int index, count;
            bool isIndexValid = Int32.TryParse(indexTextBox.Text, out index);                       
            bool isCountValid = Int32.TryParse(countTextBox.Text, out count);

            if (isIndexValid && isCountValid)
            {
                ResetViewDisplaySettings(index, count);
                ResetStatistics(index, count);
                ResetDetectionGraph(index, count);
            }
        }

        /// <summary>
        /// Detection graph scroll done event method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="scrollBar">Scroll bar.</param>
        /// <param name="oldState">Old state.</param>
        /// <param name="newState">New state.</param>
        private void detectionGraph_ScrollDoneEvent(ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            detectionGraph_ZoomEvent(sender, oldState, newState);
        }

        /// <summary>
        /// Detection graph scroll zoom event method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="oldState">Old state.</param>
        /// <param name="newState">New state.</param>
        private void detectionGraph_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            int index = (int)detectionGraph.GraphPane.XAxis.Scale.Min;
            int endIndex = (int)detectionGraph.GraphPane.XAxis.Scale.Max;

            detectionGraph.GraphPane.YAxis.Scale.Min = Math.Max(detectionGraph.GraphPane.YAxis.Scale.Min, SampleConverter.LowerBound);
            detectionGraph.GraphPane.YAxis.Scale.Max = Math.Min(detectionGraph.GraphPane.YAxis.Scale.Max, SampleConverter.UpperBound);
            detectionGraph.AxisChange();
            detectionGraph.Refresh();

            int count = endIndex - index + 1;
            ResetViewDisplaySettings(index, count);
            ResetStatistics(index, count);
        }

        /// <summary>
        /// Display check box check changed event method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void displayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ResetCurvesDisplaySettings();
            ResetDetectionGraphCurves();
        }  
    }
}
