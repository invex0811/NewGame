static class Player
{
    private static float _moveSpeed = 40f;

    public static float MoveSpeed {  get { return _moveSpeed; } }
    public static Inventory Inventory = new();
    public static Journal Journal = new();
}