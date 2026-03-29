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
    public class SM_SuperWings : MissileFins
    {
        public static ILocFile _locFile;

        public new const enumMissileComponentType ComponentType = (enumMissileComponentType)26781;

        public override enumMissileComponentCategory Category => enumMissileComponentCategory.FuelAndControl;

        public override string Name => _locFile.Get("SpecialName", "<color=yellow>Super wings");

        public override string Description => _locFile.Get("SpecialDescription", "Provides manoeuvrability. Fins further away from the center of the missile are more effective.");

        protected override string MeshPath => "R_Missiles/missile fins";

        protected override float DragModifier => 8f;

        static SM_SuperWings()
        {
            _locFile = Loc.GetFile("Missile_Fins");
        }

        public SM_SuperWings(MissileSize size, UIParameterBag bag)
            : base(size, bag)
        {
        }

        protected override void OnDestroy()
        {
        }
    }
}
