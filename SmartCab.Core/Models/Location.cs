namespace SmartCab.Core.Models
{
    public class Location
    {
        public int X { get; }
        public int Y { get; }
        public Location(int x, int y) => (X, Y) = (x, y);
        public int GetManhattanDistance(Location other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }
}