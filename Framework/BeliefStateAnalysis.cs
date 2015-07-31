using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Framework
{
    public partial class BeliefStateAnalysis : Form
    {
        public BeliefStateAnalysis()
        {
            InitializeComponent();
        }

        private void BubbleChartAnalysis_Load(object sender, EventArgs e)
        {
            var experiment = FrameworkFactory.CreateRandomBeliefBeliefStateAnalysisExperiment();
            experiment.RunTrials(1);

            var dictionary = FrameworkFactory.ActivationRepository.GetData();
            var series = new Series("Belief States");
            chart1.Series.Clear();
            series.ChartType = SeriesChartType.Bubble;
            series.MarkerStyle = MarkerStyle.Circle;
            chart1.Series.Add(series);

            foreach (var kvp in dictionary)
            {
                series.Points.AddXY(0, kvp.Key, kvp.Value[0]);
                series.Points.AddXY(1, kvp.Key, kvp.Value[1]);
                series.Points.AddXY(2, kvp.Key, kvp.Value[2]);
                series.Points.AddXY(3, kvp.Key, kvp.Value[3]);
                series.Points.AddXY(4, kvp.Key, kvp.Value[4]);
                series.Points.AddXY(5, kvp.Key, kvp.Value[5]);
                series.Points.AddXY(6, kvp.Key, kvp.Value[6]);
            }

            foreach (var point in series.Points.Where(p => p.YValues[1] < 0.9))
            {
                point.Color = Color.Crimson;
            }

            chart1.ChartAreas[0].AxisY.Title = "Fixation";
            chart1.ChartAreas[0].AxisX.Title = "Location in Visual Array";
            chart1.ChartAreas[0].AxisY2.Title = "Belief";

            chart1.Titles.Add("Belief State for each location in the visual array across trials.");
        }
    }
}