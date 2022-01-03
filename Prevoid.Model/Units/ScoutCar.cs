namespace Prevoid.Model.Units
{
    public class ScoutCar : Unit
    {
        public ScoutCar(Player player, string nickname = null)
            : base(player, string.IsNullOrEmpty(nickname) ? "SCOUT CAR" : $"SCOUT CAR {nickname}",
                4, 2, ViewModel.SpriteType.ScoutCar, 5, null) { }
    }
}
