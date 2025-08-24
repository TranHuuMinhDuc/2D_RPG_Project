namespace Snorx.Enum
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking,
        KnockedBack,
        Die,
    }
    public enum PlayerState
    {
        Idle,
        Running,
        Attacking,
        Die,
    }
    public enum  ItemTypeEffect
    {
        None,
        MaxHealth,
        CurrentHealth,
        Damage,
        Speed,
    }
    public enum  ItemType
    {
        Meat,
        Fungi,
        Vegetable,
        Fruit,
        Currency,
    }
}

