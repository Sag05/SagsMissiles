using BrilliantSkies.Core.Help;
using BrilliantSkies.Core.Logger;
using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using BrilliantSkies.Ftd.Missiles.Components;
using BrilliantSkies.Ftd.Missiles.Ui;
using BrilliantSkies.Localisation;
using BrilliantSkies.Localisation.Runtime.FileManagers.Files;
using System;
using System.Collections.Generic;


namespace SagsMissiles
{
    public class SM_Turbojet : MissileSecondaryPropeller
    {
        private static ILocFile _locFile;
        public new const enumMissileComponentType ComponentType = (enumMissileComponentType)25567042;
        public override enumMissileComponentCategory Category => enumMissileComponentCategory.SecondaryThruster;
        public override string Name => _locFile.Get("Name", "<color=yellow>Turbojet");
        public override string Description => _locFile.Get("Desc", "A jet engine, slow acceleration but very efficient.");
        protected override string MeshPath => "R_Missiles/missile body";
        protected override bool IsPropeller => false;
        public override float FuelPerThrust => 0.5f;
        public override float IsIonParameterValue => IsIonParameter?.Value ?? 0f;
        public override float UseFlameParameterValue => 0f;
        public override UIParameter IsIonParameter => base.parameters[2];
        public override UIParameter UseFlameParameter => null;
        static SM_Turbojet()
        {
            _locFile = Loc.GetFile("Turbojet");
        }

        public SM_Turbojet(MissileSize size, UIParameterBag bag) : base(size, bag)
        {
            base.MaxThrust = Rounding.R0((float)MissileConstants.BaseThrustLimit * base.Size.EfficiencyModifier);
            float defaultValue = 1100f * base.Size.EfficiencyModifier;
            base.MinThrust = base.MaxThrust / 10f;
        }

        protected override bool HandleActiveThrust(float thrust)
        {

            try
            {
                float thrust1 = 500f * base.Size.EfficiencyModifier * (((float) Missile?.Velocity.magnitude  / 100f)+1);
                base.HandleActiveThrust(thrust1);
                Missile.ThrusterWantsGravityDisabled = true;
            }
            catch (Exception e)
            {
                AdvLogger.LogException("Unhandled exception in Turbojet.HandleActiveThrust", e, LogOptions._AlertDevInGame);
            }

            /*
            bool isWet = !IsWet();
            if (isWet)
            {
                Missile.ThrusterWantsGravityDisabled = true;
            }
            else
            {
                base.HandleActiveThrust(thrust);
                Missile.ThrusterWantsGravityDisabled = false;
            }*/

            return true;
        }

        protected override void OnDestroy() { }
    }
}
