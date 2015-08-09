using System.Windows.Forms;

namespace Framework
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Analysis());
            //Application.Fixate(new ActivationAnalysis());
            //Application.Run(new BeliefStateAnalysis());
        }
    }
}