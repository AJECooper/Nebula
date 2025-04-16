namespace Nebula.NICE.MathOps
{
    public static class VectorOperations
    {
        public static double DotProduct(ReadOnlySpan<double> inputs, ReadOnlySpan<double> weights)
        {
            if (inputs.Length != weights.Length)
            {
                throw new ArgumentException("Inputs and weights must have the same length.");
            }

            double result = 0.0;

            for (int i = 0; i < inputs.Length; i++)
            {
                result += inputs[i] * weights[i];
            }

            return result;
        }
    }
}
