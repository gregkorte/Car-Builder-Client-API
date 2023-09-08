namespace CarBuilderAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int WheelId { get; set; }
    public int TechnologyId { get; set; }
    public int PaintColorId { get; set; }
    public int InteriorId { get; set; }
    public Wheels Wheels { get; set; }
    public Technology Technology { get; set; }
    public Interior Interior { get; set; }
    public PaintColor PaintColor { get; set; }
    public bool IsComplete { get; set; }
    public decimal? TotalCost => Wheels?.Price + Technology?.Price + Interior?.Price + PaintColor?.Price;

    // Another way to do it.
    // {
    // get {
    //     return Wheels.Price + Technology.Price + Interior.Price + PaintColor.Price;
    // }
    // }
}