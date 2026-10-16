using Brant.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System.Collections.Generic;

namespace Brant.Character;

public class BrantRelicPool : RelicPoolModel
{
    public override string EnergyColorName => "brant";

    protected override IEnumerable<RelicModel> GenerateAllRelics()
    {
        return new RelicModel[]
        {
            ModelDb.Relic<Bravo>()
        };
    }
}
