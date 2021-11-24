using Prevoid.Model;
using Prevoid.Model.Units;
using Prevoid.View;
using System;

namespace Prevoid.ConsoleController;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "Prevoid";

#pragma warning disable CA1416
        try
        {
            Console.WindowHeight = 31;
        }
        catch (PlatformNotSupportedException) { }
#pragma warning restore CA1416

        RenderHandler rh = new RenderHandler();
        _ = rh.StartRenderingAsync();

        GM.Map.SetUnit(new Tank(GM.Player1), 16, 19);
        GM.Map.SetUnit(new Tank(GM.Player1), 16, 17);
        GM.Map.SetUnit(new Tank(GM.Player2), 12, 13);
        GM.Map.SetUnit(new Tank(GM.Player2), 14, 13);

        GM.Start();

        bool goOn = true;
        while (goOn)
        {
            goOn = GM.HandleInput(Console.ReadKey(true));
        }
    }
}
