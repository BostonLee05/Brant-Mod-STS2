using BaseLib.Abstracts;
using BaseLib.Utils;
using Brant.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Brant.Cards;

[Pool(typeof(BrantCardPool))]
public abstract class BrantCard : CustomCardModel
{
    protected BrantCard(int energyCost, CardType type, CardRarity rarity, TargetType targetType)
        : base(energyCost, type, rarity, targetType)
    {
    }
}
