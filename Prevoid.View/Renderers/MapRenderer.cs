using Prevoid.Model;
using Prevoid.Model.EventArgs;
using Prevoid.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View.Renderers
{
    public class MapRenderer : Renderer
    {
        public Map Map { get; private set; }

        public MapRenderer(ScreenDrawer screenDrawer, Map map) : base(screenDrawer)
        {
            Map = map;
            RecacheAndRedrawFieldOfView();
        }

        public void RenderMap()
        {
            var locatedSymbols = new List<LocatedSymbol>();

            for (int j = 0; j < Map.Height; j++)
            {
                var lineSymbols = new List<LocatedSymbol>();

                for (int i = 0; i < Map.Width; i++)
                {
                    lineSymbols.Add(GetLocatedSymbolAt(i, j));
                }

                locatedSymbols.AddRange(lineSymbols.MergeConsecutive());
            }

            _Drawer.Draw(locatedSymbols);
        }

        public void RenderField(int x, int y)
        {
            _Drawer.Draw(GetLocatedSymbolAt(x, y));
        }

        public void RenderFields(IEnumerable<(int, int)> coords)
        {
            foreach (var coord in coords)
            {
                RenderField(coord.Item1, coord.Item2);
            }
        }

        public void RenderOverlay(Overlay overlay)
        {
            List<LocatedSymbol> symbols = new List<LocatedSymbol>();

            foreach (var coord in overlay.GetFields())
            {
                symbols.Add(GetLocatedSymbolAt(coord.Item1, coord.Item2, GetOverlayColor(overlay.Type)));
            }

            _Drawer.Draw(symbols);
        }

        public void HideOverlay(Overlay overlay)
        {
            RenderFields(overlay.GetFields());
        }

        public void RecacheAndRedrawFieldOfView()
        {
            RenderFields(GM.RecacheFieldOfView());
        }

        private static ConsoleColor GetOverlayColor(OverlayType overlayType)
        {
            return overlayType switch
            {
                OverlayType.Select => Constants.SelectOverlayColor,
                OverlayType.Forbidden => Constants.ForbiddenOverlayColor,
                OverlayType.Move => Constants.MoveOverlayColor,
                OverlayType.Attack => Constants.AttackOverlayColor,
                _ => throw new NotImplementedException(),
            };
        }

        private LocatedSymbol GetLocatedSymbolAt(int x, int y, ConsoleColor? overlayColor = null)
        {
            return new LocatedSymbol
            {
                ScreenX = x * 2,
                ScreenY = y,
                Symbol = GetSymbolAt(x, y, overlayColor),
            };
        }

        private Symbol GetSymbolAt(int x, int y, ConsoleColor? overlayColor)
        {
            var (spriteTypeWithVisibility, harmable) = GetSpriteTypeWithVisibilityAndIHarmableAt(x, y);
            var symbol = GetSymbolFromSpriteTypeWithVisibility(spriteTypeWithVisibility, harmable);

            if (Map.Selection.Item1 == x && Map.Selection.Item2 == y && !GM.HasTurnEnded)
            {
                return symbol.GetTranslucent(Constants.SelectionColor);
            }
            if (overlayColor is not null)
            {
                return symbol.GetTranslucent(overlayColor.Value);
            }

            return symbol;
        }

        private (SpriteTypeWithVisibility, IHarmable) GetSpriteTypeWithVisibilityAndIHarmableAt(int x, int y)
        {
            bool visible = GM.CanCurrentPlayerSee(x, y);

            if (Map.Fields[x, y] != null && visible)
            {
                return (new SpriteTypeWithVisibility(Map.Fields[x, y].SpriteType, visible), Map.Fields[x, y]);
            }
            else if (Map.Structures[x, y] != null && visible)
            {
                return (new SpriteTypeWithVisibility(Map.Structures[x, y].SpriteType, visible), Map.Structures[x, y] as IHarmable);
            }
            else
            {
                return (new SpriteTypeWithVisibility(GetSpriteTypeFromTerrain(x, y), visible), null);
            }
        }

        private SpriteType GetSpriteTypeFromTerrain(int x, int y)
        {
            return Map.TerrainTypes[x, y] switch
            {
                TerrainType.Flat => SpriteType.Empty,
                TerrainType.SparceForest => SpriteType.SparceForest,
                TerrainType.DeepForest => SpriteType.DeepForest,
                TerrainType.Mountain => SpriteType.Mountain,
                TerrainType.Water => SpriteType.Water,
                _ => throw new NotImplementedException(),
            };
        }

        private Symbol GetSymbolFromSpriteTypeWithVisibility(SpriteTypeWithVisibility spriteTypeWithVisibility, IHarmable harmable)
        {
            var result = spriteTypeWithVisibility.SpriteType switch
            {
                SpriteType.Empty => new Symbol
                {
                    BackColor = Constants.TerrainColor,
                    Text = "  ",
                },
                SpriteType.FogOfWar => new Symbol
                {
                    BackColor = Constants.FogOfWarColor,
                    Text = "  ",
                },
                SpriteType.Mountain => new Symbol
                {
                    ForeColor = Constants.MountainColor,
                    BackColor = Constants.TerrainColor,
                    Text = @"/\",
                },
                SpriteType.Water => new Symbol
                {
                    ForeColor = Constants.WavesColor,
                    BackColor = Constants.WaterColor,
                    Text = "~ ",
                },
                SpriteType.SparceForest => new Symbol
                {
                    ForeColor = Constants.ForestColor,
                    BackColor = Constants.TerrainColor,
                    Text = "▲ ",
                },
                SpriteType.DeepForest => new Symbol
                {
                    ForeColor = Constants.ForestColor,
                    BackColor = Constants.TerrainColor,
                    Text = "▲▲",
                },
                SpriteType.Tank => new Symbol
                {
                    ForeColor = harmable.Player.Color,
                    BackColor = Constants.TerrainColor,
                    Text = "T" + harmable.HpChar,
                },
                SpriteType.Base => new Symbol
                {
                    ForeColor = harmable.Player.Color,
                    BackColor = Constants.TerrainColor,
                    Text = "B" + harmable.HpChar,
                },
                SpriteType.ScoutCar => new Symbol
                {
                    ForeColor = harmable.Player.Color,
                    BackColor = Constants.TerrainColor,
                    Text = "S" + harmable.HpChar,
                },
                _ => throw new NotImplementedException(),
            };

            if (spriteTypeWithVisibility.IsVisible)
            {
                return result;
            }
            else
            {
                return new Symbol
                {
                    ForeColor = Constants.FogOfWarForeColor,
                    BackColor = Constants.FogOfWarColor,
                    Text = result.Text,
                };
            }
        }
    }
}
