using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Framework
{
    public partial class PercentageCorrectAnalysis : Form
    {
        public PercentageCorrectAnalysis()
        {
            InitializeComponent();
        }

        private void PercentageCorrectAnalysis_Load(object sender, EventArgs e)
        {
            //var experiment = FrameworkFactory.CreatePercentCorrectRandomExperiment();
            //var experiment = FrameworkFactory.CreatePercentCorrectRandomBeliefExperiment();
            //var experiment = FrameworkFactory.CreatePercentCorrectMapExperiment();
            var experiment = FrameworkFactory.CreatePercentCorrectQLearningExperiment();

            //experiment.RunTrials(100000);
            experiment.RunTrials(100000,100000);

            var dictionary = FrameworkFactory.PercentageCorrectRepository.GetData();

            var series = new Series("QLearning", dictionary.Count) { ChartType = SeriesChartType.Line };

            chart1.Series.Clear();
            chart1.Series.Add(series);
            chart1.Series["QLearning"].Points.DataBindXY(dictionary.Keys, dictionary.Values);
            // Basic = red, Belief = Blue, MAP = Purple, QLearning = Green
            chart1.Series[0].Color = Color.DarkGreen;
            chart1.ChartAreas[0].AxisX.Title = "No. Of Fixations";
            chart1.ChartAreas[0].AxisY.Title = "Total Trials";
            chart1.ChartAreas[0].AxisX.Maximum = 12;
            chart1.ChartAreas[0].AxisY.Maximum = 1;
            //chart1.ChartAreas[0].AxisX.Interval = 1;
        }

    }
}
