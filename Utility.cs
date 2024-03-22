namespace Sameple
{
    public static class Utility
    {
        public static void WriteOneByOne(string text)
        {
            foreach(char c in text)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
        }
    }
}
