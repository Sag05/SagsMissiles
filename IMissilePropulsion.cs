using BrilliantSkies.Ftd.Missiles.Components;

namespace SagsMissiles
{
    public interface IMissilePropulsion
    {
        public void Propel(MissilePropulsion missilePropulsion);

        bool CallOriginalRun => false;
    }
}