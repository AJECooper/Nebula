namespace Nebula.NN.Activations
{
    public class HeavisideStep
    {
        public HeavisideStep()
        {
        }

        public int Activate(double sumOfOperations)
        {
            return sumOfOperations >= 0 ? 1 : 0;
        }
    }
}
