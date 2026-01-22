namespace rh.BackOffice.Services.AI
{
    public static class VectorUtils
    {
        public static double CosineSimilarity(float[] a, float[] b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException("Les vecteurs ne peuvent pas être null.");

            if (a.Length != b.Length)
                throw new ArgumentException("Les vecteurs doivent avoir la même dimension.");

            double dot = 0.0;
            double normA = 0.0;
            double normB = 0.0;

            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
            }

            if (normA == 0 || normB == 0)
                return 0.0;

            return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }

    }
}
