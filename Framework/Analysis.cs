using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Framework
{
    public partial class Analysis : Form
    {
        public Analysis()
        {
            InitializeComponent();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
            var experiment = FrameworkFactory.CreateRandomExperiment();
            //var experiment = FrameworkFactory.CreateRandomExclusionExperiment();
            //var experiment = FrameworkFactory.CreateRandomBeliefExperiment();
            //var experiment = FrameworkFactory.CreateRandomBeliefExclusionExperiment();
            //var experiment = FrameworkFactory.CreateMapExperiment();
            //var experiment = FrameworkFactory.CreateQLearningExperiment();
            experiment.RunTrials(100000);

            var dictionary = FrameworkFactory.Repository.GetData();
            //var dictionary = memoryDataRepository.GetData();

            var series = new Series("Basic Control", dictionary.Count) {ChartType = SeriesChartType.Line};
            chart1.Series.Clear();
            chart1.Series.Add(series);
            chart1.Series["Basic Control"].Points.DataBindXY(dictionary.Keys, dictionary.Values);
            // Basic = red, Belief = Blue, MAP = Purple, QLearning = Green
            chart1.Series[0].Color = Color.DarkRed;
            chart1.ChartAreas[0].AxisX.Title = "No. Of Fixations";
            chart1.ChartAreas[0].AxisY.Title = "Total Trials";
            chart1.ChartAreas[0].AxisX.Maximum = 60;
            chart1.ChartAreas[0].AxisY.Maximum = 100000;
            //chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.Titles.Add("Average Fixations : " + FrameworkFactory.Repository.GetAverage());
        }
    }
}