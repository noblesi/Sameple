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

        public static void WriteCenterPosition(string text)
        {
            int screenWidth = Console.WindowWidth;
            int centerX = (screenWidth - text.Length) / 2;

            Console.SetCursorPosition(centerX, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static void ClearConsoleArea(int left, int top, int width, int height)
        {
            Console.SetCursorPosition(left, top);
            for(int y=0; y<height; y++)
            {
                for(int x=0; x<width; x++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
                Console.SetCursorPosition(left,Console.CursorTop);
            }
            Console.SetCursorPosition(left, top);
        }
    }
}
