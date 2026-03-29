using BrilliantSkies.Ftd.Missiles.Components;

namespace SagsMissiles
{
    public interface IMissilePropulsion
    {
        bool CallOriginalRun => false;
        public void Propel(MissilePropulsion missilePropulsion);
    }
}