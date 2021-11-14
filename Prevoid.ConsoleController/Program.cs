using Prevoid.Model;
using Prevoid.Model.Structures;
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

            GM.Map.SetStructure(new Mountain(), 15, 15);
            GM.NextTurn();
            Console.ReadKey();
        }
    }
}
