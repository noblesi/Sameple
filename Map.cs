﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Map
    {
        private const char BorderSymbol = '■';
        private const char EmptySymbol = ' ';

        private char[,] map;
        private int width;
        private int height;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            map = new char[width, height];
        }

        public void Initialize()
        {
            // 맵 초기화
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y] = BorderSymbol; // 테두리
                    }
                    else
                    {
                        map[x, y] = EmptySymbol; // 내부
                    }
                }
            }
        }

        public void Display()
        {
            // 맵 표시
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        public char[,] GetMap()
        {
            return map;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}