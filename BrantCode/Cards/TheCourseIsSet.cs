using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brant.Cards;
using Brant.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Brant.Cards;

public sealed class TheCourseIsSet : BrantCard
{
    public TheCourseIsSet() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new BlockVar(8M, ValueProp.Move),
        new CardsVar(1)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            return new IHoverTip[]
            {
                new HoverTip(new LocString("keywords", "BRANT-ER.title"), new LocString("keywords", "BRANT-ER.description"))
            };
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int er = ERSystem.GetER(this.Owner?.PlayerCombatState);
        decimal totalBlock = this.DynamicVars.Block.BaseValue + (decimal)(er / 20);
        await CreatureCmd.GainBlock(this.Owner.Creature, new BlockVar(totalBlock, ValueProp.Move), cardPlay);
    }

    protected override void OnUpgrade() => this.DynamicVars.Block.UpgradeValueBy(3M);
}
