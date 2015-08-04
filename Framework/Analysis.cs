using System;
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
            //var experiment = FrameworkFactory.CreateRandomExperiment();
            //var experiment = FrameworkFactory.CreateRandomExclusionExperiment();
            var experiment = FrameworkFactory.CreateRandomBeliefExperiment();
            //var experiment = FrameworkFactory.CreateRandomBeliefExclusionExperiment();
            //var experiment = FrameworkFactory.CreateMapExperiment();

            experiment.RunTrials(100000);

            var dictionary = FrameworkFactory.Repository.GetData();

            var series = new Series("No. Fixations Across Trials", dictionary.Count) {ChartType = SeriesChartType.Line};
            chart1.Series.Clear();
            chart1.Series.Add(series);
            chart1.Series["No. Fixations Across Trials"].Points.DataBindXY(dictionary.Keys, dictionary.Values);
            chart1.ChartAreas[0].AxisX.Title = "No. Of Fixations";
            chart1.ChartAreas[0].AxisY.Title = "Total Trials";
            //chart1.ChartAreas[0].AxisX.Maximum = 7;
            //chart1.ChartAreas[0].AxisY.Maximum = 350;

            chart1.Titles.Add("Average Fixations : " + FrameworkFactory.Repository.GetAverage());
        }
    }
}