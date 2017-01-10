using System;
using System.Drawing;
using System.Windows.Forms;
using DAPNet;
using ZedGraph;

namespace DeclickerDisplay
{
    /// <summary>
    /// Represents the correction form.
    /// </summary>
    public partial class CorrectionForm : Form
    {
        /// <summary>
        /// Holds the corrected curve.
        /// </summary>
        private LineItem correctedCurve;

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
        /// Holds the reviewed curve.
        /// </summary>
        private LineItem reviewedCurve;

        /// <summary>
        /// Holds the ideal curve.
        /// </summary>
        private LineItem idealCurve;


        /// <summary>
        /// Holds the corrected.
        /// </summary>
        private SampleVector corrected;

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
        /// Holds the reviewed.
        /// </summary>
        private DetectionVector reviewed;

        /// <summary>
        /// Holds the ideal.
        /// </summary>
        private DetectionVector ideal;


        /// <summary>
        /// Creates a correction form.
        /// </summary>
        /// <param name="corrected">Corrected.</param>
        /// <param name="samples">Samples.</param>
        /// <param name="degraded">Degraded.</param>
        /// <param name="degradations">Degradations.</param>
        /// <param name="reviewed">Reviewed.</param>
        /// <param name="ideal">Ideal.</param>
        public CorrectionForm(SampleVector corrected, SampleVector samples, SampleVector degraded, SampleVector degradations, DetectionVector reviewed, DetectionVector ideal)
        {
            InitializeComponent();

            correctionGraph.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler(correctionGraph_ScrollDoneEvent);
            correctionGraph.ZoomEvent += new ZedGraphControl.ZoomEventHandler(correctionGraph_ZoomEvent);

            this.corrected = corrected;
            this.samples = samples;
            this.degraded = degraded;
            this.degradations = degradations;
            this.reviewed = reviewed;
            this.ideal = ideal;
        }

        /// <summary>
        /// Loads the correction form.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void CorrectionForm_Load(object sender, EventArgs e)
        {
            InicializeCorrectionGraph();
            InicializeDisplaySettings();

            ResetViewDisplaySettings(0, samples.Count);
            ResetCurvesDisplaySettings();
            ResetStatistics(0, samples.Count);
            ResetCorrectionGraphCurves();
            ResetCorrectionGraph(0, samples.Count);
        }

        /// <summary>
        /// Inicializes display settings.
        /// </summary>
        private void InicializeDisplaySettings()
        {
            displayCorrectedCheckBox.Checked = true;
            displaySamplesCheckBox.Checked = true;
            displayDegradedCheckBox.Checked = true;
            displayDegradationsCheckBox.Checked = true;
            displayReviewedCheckBox.Checked = true;
            displayIdealCheckBox.Checked = true;
        }

        /// <summary>
        /// Inicializes correction graph.
        /// </summary>
        private void InicializeCorrectionGraph()
        {
            correctionGraph.GraphPane.Title.IsVisible = false;
            correctionGraph.GraphPane.XAxis.Title.Text = Settings.SampleLabel;
            correctionGraph.GraphPane.YAxis.Title.Text = Settings.AmplitudeLabel;

            InicializeCorrectedCurve();
            InicializeSamplesCurve();
            InicializeDegradedCurve();
            InicializeDegradationsCurve();
            InicializeReviewedCurve();
            InicializeIdealCurve();
        }

        /// <summary>
        /// Inicializes corrected curve.
        /// </summary>
        private void InicializeCorrectedCurve()
        {
            PointPairList correctedPoints = new PointPairList();
            for (int i = 0; i < corrected.Count; i++)
            {
                correctedPoints.Add(i, corrected[i]);
            }

            correctedCurve = correctionGraph.GraphPane.AddCurve(Settings.CorrectedLabel, correctedPoints, Settings.DegradedCurveColor);
            correctedCurve.Line.IsSmooth = true;
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

            samplesCurve = correctionGraph.GraphPane.AddCurve(Settings.SamplesLabel, samplesPoints, Settings.SamplesCurveColor);
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

            degradedCurve = correctionGraph.GraphPane.AddCurve(Settings.DegradedLabel, degradedPoints, Settings.DegradedCurveColor);
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

            degradationsCurve = correctionGraph.GraphPane.AddCurve(Settings.DegradationsLabel, degradationsPoints, Settings.DegradationsCurveColor);
            degradationsCurve.Line.IsSmooth = true;
        }

        /// <summary>
        /// Inicializes reviewed curve.
        /// </summary>
        private void InicializeReviewedCurve()
        {
            PointPairList reviewedPoints = new PointPairList();
            for (int i = 0; i < reviewed.Count; i++)
            {
                double value = reviewed[i] ? SampleConverter.UpperBound : 0;
                reviewedPoints.Add(i - Settings.DetectionCurveOffset, value);
            }

            reviewedCurve = correctionGraph.GraphPane.AddCurve(Settings.ReviewedLabel, reviewedPoints, Color.Transparent);
            reviewedCurve.Line.Fill = Settings.ReviewedCurveFill;
            reviewedCurve.Line.StepType = StepType.ForwardStep;
        }

        /// <summary>
        /// Inicializes ideal curve.
        /// </summary>
        private void InicializeIdealCurve()
        {
            PointPairList idealPoints = new PointPairList();
            for (int i = 0; i < ideal.Count; i++)
            {
                double value = ideal[i] ? SampleConverter.LowerBound : 0;
                idealPoints.Add(i - Settings.DetectionCurveOffset, value);
            }

            idealCurve = correctionGraph.GraphPane.AddCurve(Settings.IdealLabel, idealPoints, Color.Transparent);
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
            displayCorrectedSymbolsCheckBox.Enabled = displayCorrectedCheckBox.Checked;
            displayCorrectedSymbolsCheckBox.Refresh();

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
            double i_Number = ideal.GetNumSeqs(true, index, count);
            i_NumberTextBox.Text = i_Number.ToString();
            i_NumberTextBox.Refresh();

            double i_Length = ideal.GetAvgSeqLength(true, index, count);
            i_LengthTextBox.Text = i_Length.ToString(Settings.DoubleFormat);
            i_LengthTextBox.Refresh();

            double c_Effectivity = 1.0d - reviewed.GetEffectivity(ideal, true, index, count);
            c_EffectivityTextBox.Text = c_Effectivity.ToString(Settings.PercentageFormat);
            c_EffectivityTextBox.Refresh();

            double mean = corrected.GetDiffMean(samples, index, count);
            meanTextBox.Text = mean.ToString(Settings.DoubleFormat);
            meanTextBox.Refresh();

            double deviation = corrected.GetDiffDeviation(samples, index, count);
            deviationTextBox.Text = deviation.ToString(Settings.DoubleFormat);
            deviationTextBox.Refresh();
        }

        /// <summary>
        /// Resets correction graph for specified interval.
        /// </summary>
        /// <param name="index">Starting index of the interval.</param>
        /// <param name="count">Length of the interval.</param>
        private void ResetCorrectionGraph(int index, int count)
        {
            correctionGraph.GraphPane.YAxis.Scale.Min = SampleConverter.LowerBound;
            correctionGraph.GraphPane.YAxis.Scale.Max = SampleConverter.UpperBound;

            correctionGraph.GraphPane.XAxis.Scale.Min = index;
            int endIndex = index + count - 1;
            correctionGraph.GraphPane.XAxis.Scale.Max = endIndex;

            correctionGraph.AxisChange();
            correctionGraph.Refresh();
        }

        /// <summary>
        /// Resets correction graph curves.
        /// </summary>
        private void ResetCorrectionGraphCurves()
        {
            correctedCurve.Symbol.Type = displayCorrectedSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            correctedCurve.Color = displayCorrectedCheckBox.Checked ? Settings.CorrectedCurveColor : Color.Transparent;

            samplesCurve.Symbol.Type = displaySamplesSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            samplesCurve.Color = displaySamplesCheckBox.Checked ? Settings.SamplesCurveColor : Color.Transparent;

            degradedCurve.Symbol.Type = displayDegradedSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            degradedCurve.Color = displayDegradedCheckBox.Checked ? Settings.DegradedCurveColor : Color.Transparent;

            degradationsCurve.Symbol.Type = displayDegradationsSymbolsCheckBox.Checked ? Settings.ShownSymbol : Settings.HiddenSymbol;
            degradationsCurve.Color = displayDegradationsCheckBox.Checked ? Settings.DegradationsCurveColor : Color.Transparent;

            reviewedCurve.Line.Fill = displayReviewedCheckBox.Checked ? Settings.ReviewedCurveFill : Settings.TransparentFill;
            idealCurve.Line.Fill = displayIdealCheckBox.Checked ? Settings.IdealCurveFill : Settings.TransparentFill;

            correctionGraph.Refresh();
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
            ResetCorrectionGraph(0, samples.Count);
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
                ResetCorrectionGraph(index, count);
            }
        }

        /// <summary>
        /// Detection graph scroll done event method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="scrollBar">Scroll bar.</param>
        /// <param name="oldState">Old state.</param>
        /// <param name="newState">New state.</param>
        private void correctionGraph_ScrollDoneEvent(ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            correctionGraph_ZoomEvent(sender, oldState, newState);
        }

        /// <summary>
        /// Detection graph scroll zoom event method.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="oldState">Old state.</param>
        /// <param name="newState">New state.</param>
        private void correctionGraph_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            int index = (int)correctionGraph.GraphPane.XAxis.Scale.Min;
            int endIndex = (int)correctionGraph.GraphPane.XAxis.Scale.Max;

            correctionGraph.GraphPane.YAxis.Scale.Min = Math.Max(correctionGraph.GraphPane.YAxis.Scale.Min, SampleConverter.LowerBound);
            correctionGraph.GraphPane.YAxis.Scale.Max = Math.Min(correctionGraph.GraphPane.YAxis.Scale.Max, SampleConverter.UpperBound);
            correctionGraph.AxisChange();
            correctionGraph.Refresh();

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
            ResetCorrectionGraphCurves();
        }
    }
}
