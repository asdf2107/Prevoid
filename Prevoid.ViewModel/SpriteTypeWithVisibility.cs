namespace Prevoid.ViewModel
{
    public struct SpriteTypeWithVisibility
    {
        public SpriteType SpriteType { get; set; }
        public bool IsVisible { get; set; }

        public SpriteTypeWithVisibility(SpriteType spriteType, bool isVisible)
        {
            SpriteType = spriteType;
            IsVisible = isVisible;
        }
    }
}
