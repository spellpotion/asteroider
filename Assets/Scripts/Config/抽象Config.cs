using UnityEngine;

namespace Asteroider
{
    public abstract class 抽象AbilityConfig<T> : 抽象Config where T : 抽象Ability<T> { }
    public abstract class 抽象EquipmentConfig<T> : 抽象Config where T : 抽象Equipment<T> { }
    public abstract class 抽象GameboardObjectConfig<T> : 抽象Config where T : 抽象GameboardObject<T> { }
    public abstract class 抽象LayoutConfig<T> : 抽象Config where T : 抽象Layout<T> { }
    public abstract class 抽象ManagerConfig<T> : 抽象Config where T : 抽象Manager<T> { }
    public abstract class 抽象Config : ScriptableObject { }
}