using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public static class PlayerMovement
    {
        public static void ProcessInput(ConsoleKeyInfo key, ref int playerX, ref int playerY, int mapWidth, int mapHeight)
        {
            // 입력에 따른 행동 처리
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MovePlayer(ref playerX, ref playerY, 0, -1, mapWidth, mapHeight);
                    break;
                case ConsoleKey.DownArrow:
                    MovePlayer(ref playerX, ref playerY, 0, 1, mapWidth, mapHeight);
                    break;
                case ConsoleKey.LeftArrow:
                    MovePlayer(ref playerX, ref playerY, -1, 0, mapWidth, mapHeight);
                    break;
                case ConsoleKey.RightArrow:
                    MovePlayer(ref playerX, ref playerY, 1, 0, mapWidth, mapHeight);
                    break;
                case ConsoleKey.D1:
                    Console.SetCursorPosition(mapWidth * 2 + 1, 8);
                    Inventory.OpenInventory(GameManager.currentPlayer.Inventory);

                    break;
                case ConsoleKey.D2:
                    Console.SetCursorPosition(mapWidth * 2 + 1, 8);
                    Shop.OpenShopForBuy(GameManager.shop);
                    break;
                case ConsoleKey.D3:
                    Console.SetCursorPosition(mapWidth * 2 + 1, 8);
                    Inventory.OpenInventoryForSell(GameManager.currentPlayer.Inventory);
                    break;
                case ConsoleKey.D4:
                    Console.SetCursorPosition(mapWidth * 2 + 1, 7);
                    Inventory.EquipWeaponFromInventory(GameManager.currentPlayer.Inventory);
                    break;

                
                    // 다른 키에 대한 처리 추가 가능
            }
        }

        static void MovePlayer(ref int playerX, ref int playerY, int deltaX, int deltaY, int mapWidth, int mapHeight)
        {
            // 플레이어 이동
            int newPlayerX = playerX + deltaX;
            int newPlayerY = playerY + deltaY;
            if (newPlayerX >= 1 && newPlayerX < mapWidth - 1 && newPlayerY >= 1 && newPlayerY < mapHeight - 1)
            {
                playerX = newPlayerX;
                playerY = newPlayerY;
            }
        }
    }
}
