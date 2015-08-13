using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Framework
{
    public partial class CumulativeReward : Form
    {
        public CumulativeReward()
        {
            InitializeComponent();
        }

        private void CumulativeReward_Load(object sender, EventArgs e)
        {
            //var experiment = FrameworkFactory.CreateQLearningExperiment();
            var experiment = FrameworkFactory.CreateQLearningAnalysisExperiment();
            //bug means one more than you might think, multiple of 150 + 1
            experiment.RunTrials(2000001);

            //var dictionary = FrameworkFactory.CumulativeRepository.GetData();
            var dictionary = FrameworkFactory.CumulativeEpochRepository.GetData();

            var series = new Series("QLearning", dictionary.Count) {ChartType = SeriesChartType.Line};
            chart1.Series.Clear();
            chart1.Series.Add(series);
            chart1.Series["QLearning"].Points.DataBindXY(dictionary.Keys, dictionary.Values);
            chart1.Series[0].Color = Color.DarkBlue;
            chart1.ChartAreas[0].AxisX.Title = "Total Epochs";
            chart1.ChartAreas[0].AxisY.Title = "Cumulative Reward";
        }
    }
}