static class Player
{
    private static float _moveSpeed = 20f;

    public static float MoveSpeed {  get { return _moveSpeed; } }
    public static Inventory Inventory = new();
    public static Journal Journal = new();
}