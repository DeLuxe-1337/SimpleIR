namespace SimpleIR_Code_Builder
{
    internal class Indention
    {
        public static int dent;

        public static void Inc()
        {
            dent += 4;
        }

        public static void Dec()
        {
            dent -= 4;
        }

        public static string ToString()
        {
            return new string(' ', dent);
        }
    }
}