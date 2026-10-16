using System;
using System.Collections.Generic;
using System.Linq;
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

public sealed class BuildingTheCrowd : BrantCard
{
    public BuildingTheCrowd() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new CardsVar(2)
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
        int enemyCount = this.CombatState != null ? this.CombatState.Enemies.Count(e => e.IsAlive) : 1;
        int erGain = 15 + (10 * enemyCount);
        ERSystem.AddER(this.Owner?.PlayerCombatState, erGain);
        await CardPileCmd.Draw(choiceContext, 1, this.Owner);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}
