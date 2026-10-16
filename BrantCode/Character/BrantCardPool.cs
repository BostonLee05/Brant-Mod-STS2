using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using Godot;
using Brant.Cards;
using Brant.Cards.Basics;

namespace Brant.Character;

public class BrantCardPool : CardPoolModel
{
    public override string Title => "Brant";
    public override string EnergyColorName => "ironclad";
    public override string CardFrameMaterialPath => "card_frame_blue";
    public override Color DeckEntryCardColor => Colors.Blue;
    public override bool IsColorless => false;

    protected override CardModel[] GenerateAllCards()
    {
        return new CardModel[]
        {
            ModelDb.Card<StrikeBrant>(),
            ModelDb.Card<DefendBrant>(),
            ModelDb.Card<ToTheHorizon>(),
            ModelDb.Card<OceanOdyssey>(),
            ModelDb.Card<DragonOfDirge>(),
            ModelDb.Card<Tidebreaking>(),
            ModelDb.Card<SurgingWave>(),
            ModelDb.Card<AnchorsAweigh>(),
            ModelDb.Card<TroupeOfFools>(),
            ModelDb.Card<FinalBow>(),
            ModelDb.Card<PerfectFinale>(),
            ModelDb.Card<BuildingTheCrowd>(),
            ModelDb.Card<AllOut>(),
            ModelDb.Card<TheCourseIsSet>()
        };
    }
}
