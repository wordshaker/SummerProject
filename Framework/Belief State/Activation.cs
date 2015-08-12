using Accord.Statistics.Distributions.Univariate;
using Framework.Observation;

namespace Framework.Belief_State
{
    public class Activation : IActivation
    {
        private readonly NormalDistribution _standardDistribution;
        private double[] _observationValues;

        public Activation(NormalDistribution standardDistribution)
        {
            _standardDistribution = standardDistribution;
        }

        public double[] GenerateActivation(int fixation, int[] visualArray)
        {
            var activationValues = new double[visualArray.Length];
            _observationValues = GenerateObservations(fixation, visualArray);

            for (var i = 0; i < visualArray.Length; i++)
            {
                //discriminability is explained in Butko and Movellan (2008) and is determined using FPOC and eceentricity from fixation.
                var discriminability =
                    new ObservationGenerationModel(_standardDistribution, fixation, i)
                        .GenerateDiscriminabilityValue();
                activationValues[i] = _observationValues[i] - discriminability/2;
            }

            return activationValues;
        }

        private double[] GenerateObservations(int fixation, int[] visualArray)
        {
            _observationValues = new double[visualArray.Length];
            for (var location = 0; location < visualArray.Length; location++)
            {
                if (visualArray[location] == 0)
                {
                    _observationValues[location] = _standardDistribution.Generate();
                }
                else
                {
                    //discriminability is explained in Butko and Movellan (2008) and is determined using FPOC and eceentricity from fixation.
                    var discriminability =
                        new ObservationGenerationModel(_standardDistribution, fixation, location)
                            .GenerateDiscriminabilityValue();
                    _observationValues[location] = new NormalDistribution(discriminability, 1.0).Generate();
                }
            }
            return _observationValues;
        }
    }
}