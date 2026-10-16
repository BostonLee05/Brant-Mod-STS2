using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.Potions;
using System.Collections.Generic;

namespace Brant.Character;

public class BrantPotionPool : PotionPoolModel
{
    public override string EnergyColorName => "brant";

    protected override IEnumerable<PotionModel> GenerateAllPotions()
    {
        return new PotionModel[0];
    }
}
