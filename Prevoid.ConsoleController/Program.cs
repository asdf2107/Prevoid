using Prevoid.Model;
using Prevoid.Model.Units;
using Prevoid.Network;
using Prevoid.View;
using System;
using System.Globalization;
using Constants = Prevoid.Model.Constants;

namespace Prevoid.ConsoleController;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "Prevoid";
        
        CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

#pragma warning disable CA1416
        try
        {
            Console.WindowHeight = 31;
        }
        catch (PlatformNotSupportedException) { }
#pragma warning restore CA1416

        NetworkManager.StartOnline();
        //GM.CommandHandler.HandleCommand(new Model.Commands.GenMapCommand(GM.Random.Next()));

        _ = new RenderHandler().StartRenderingAsync();

        GM.Map.SetUnit(new Base(GM.Player1), Constants.MapWidth - 2, Constants.MapHeight - 2);
        GM.Map.SetUnit(new Base(GM.Player2), 1, 1);

        GM.Map.SetUnit(new ScoutCar(GM.Player1), Constants.MapWidth - 6, Constants.MapHeight - 2);
        GM.Map.SetUnit(new Tank(GM.Player1), Constants.MapWidth - 11, Constants.MapHeight - 2);
        GM.Map.SetUnit(new Tank(GM.Player1), Constants.MapWidth - 16, Constants.MapHeight - 2);
        GM.Map.SetUnit(new Tank(GM.Player1), Constants.MapWidth - 21, Constants.MapHeight - 2);
        GM.Map.SetUnit(new ScoutCar(GM.Player1), Constants.MapWidth - 26, Constants.MapHeight - 2);
        GM.Map.SetUnit(new ScoutCar(GM.Player2), 5, 1);
        GM.Map.SetUnit(new Tank(GM.Player2), 10, 1);
        GM.Map.SetUnit(new Tank(GM.Player2), 15, 1);
        GM.Map.SetUnit(new Tank(GM.Player2), 20, 1);
        GM.Map.SetUnit(new ScoutCar(GM.Player2), 25, 1);

        GM.Start();

        bool continueGameLoop = true;
        while (continueGameLoop)
        {
            continueGameLoop = GM.HandleInput(Console.ReadKey(true).Key);
        }
    }
}
