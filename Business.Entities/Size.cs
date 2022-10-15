namespace Business.Entities
{
    public class Size
    {
        public Size(int widthPx, int heightPx)
        {
            WidthPx = widthPx;
            HeightPx = heightPx;
        }

        public int WidthPx { get; }
        public int HeightPx { get; }
    }
}
