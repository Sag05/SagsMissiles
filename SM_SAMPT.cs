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
using UnityEngine;


namespace SagsMissiles
{
    public class SM_SAMPT : MissileShortRangeThruster, IMissilePropulsion
    {
        private static ILocFile _locFile;
        public new const enumMissileComponentType ComponentType = (enumMissileComponentType)25567043;
        public override enumMissileComponentCategory Category => enumMissileComponentCategory.Thruster;
        public override string Name => _locFile.Get("Name", "<color=yellow>SAMPT");

        public override string Description =>
            _locFile.Get("Desc", "A BIG engine, slow acceleration but very efficient.");

        protected override string MeshPath => "R_Missiles/missile body";
        protected override bool IsPropeller => false;
        public override float FuelPerThrust => 2f;
        public override float IsIonParameterValue => IsIonParameter?.Value ?? 0f;
        public override float UseFlameParameterValue => 0f;
        public override UIParameter IsIonParameter => base.parameters[2];
        public override UIParameter UseFlameParameter => null;

        static SM_SAMPT()
        {
            _locFile = Loc.GetFile("Turbojet");
        }

        public SM_SAMPT(MissileSize size, UIParameterBag bag) : base(size, bag)
        {
        }


        public void Propel(MissilePropulsion missilePropulsion)
        {
            missilePropulsion.Missile!.Rigidbody.AddForceAtPosition(missilePropulsion.Missile.Forward * 250, missilePropulsion.Position, ForceMode.Acceleration);
        }
    }
}
