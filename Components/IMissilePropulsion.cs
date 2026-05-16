using BrilliantSkies.Ftd.Missiles.Components;

namespace SagsMissiles
{
    public interface IMissilePropulsion
    {
        bool CallOriginalRun => false;

        public virtual void Propel(MissilePropulsion missilePropulsion)
        {
        }
    }
}