using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Components;
using BrilliantSkies.Ftd.Missiles.Ui;
using BrilliantSkies.Localisation;
using BrilliantSkies.Localisation.Runtime.FileManagers.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagsMissiles
{
    internal class SM_Wings : MissileFins
    {
        public static ILocFile _locFile;

        public new const enumMissileComponentType ComponentType = (enumMissileComponentType)26780;

        public override enumMissileComponentCategory Category => enumMissileComponentCategory.FuelAndControl;

        public override string Name => _locFile.Get("SpecialName", "<color=yellow>Wings");

        public override string Description => _locFile.Get("SpecialDescription", "Provides manoeuvrability. Fins further away from the center of the missile are more effective.");

        protected override string MeshPath => "R_Missiles/missile fins";

        protected override float DragModifier => 8f;

        static SM_Wings()
        {
            _locFile = Loc.GetFile("Missile_Fins");
        }

        public SM_Wings(MissileSize size, UIParameterBag bag)
            : base(size, bag)
        {
        }

        protected override void OnDestroy()
        {
        }
    }
}
