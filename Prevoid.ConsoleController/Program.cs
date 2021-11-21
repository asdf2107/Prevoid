using Prevoid.Model;
using Prevoid.Model.Units;
using Prevoid.View;
using System;

namespace Prevoid.ConsoleController
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            RenderHandler rh = new RenderHandler();
            _ = rh.StartRenderingAsync();

            GM.Map.SetUnit(new Tank(GM.Player1), 16, 19);
            GM.Map.SetUnit(new Tank(GM.Player1), 16, 17);
            GM.Map.SetUnit(new Tank(GM.Player2), 12, 13);
            GM.Map.SetUnit(new Tank(GM.Player2), 14, 13);
            GM.NextTurn();
            GM.NextTurn();

            bool goOn = true;
            while (goOn)
            {
                goOn = GM.HandleInput(Console.ReadKey(true));
            }
        }
    }
}
