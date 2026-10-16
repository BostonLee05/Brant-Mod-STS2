using BaseLib.Abstracts;
using Brant.Cards;
using Brant.Cards.Basics;
using Brant.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using System.Collections.Generic;
using Godot;

namespace Brant.Character;

public class BrantCharacterModel : CustomCharacterModel
{
    public BrantCharacterModel() : base()
    {
    }

    public override int StartingHp => 70;
    public override int StartingGold => 99;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override Color NameColor => Colors.Blue;
    public override List<string> GetArchitectAttackVfx() => new();
    
    public override CardPoolModel CardPool => ModelDb.CardPool<BrantCardPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<BrantPotionPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<BrantRelicPool>();

    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<StrikeBrant>(), ModelDb.Card<StrikeBrant>(), ModelDb.Card<StrikeBrant>(), ModelDb.Card<StrikeBrant>(), ModelDb.Card<StrikeBrant>(),
        ModelDb.Card<DefendBrant>(), ModelDb.Card<DefendBrant>(), ModelDb.Card<DefendBrant>(), ModelDb.Card<DefendBrant>(),
        ModelDb.Card<ToTheHorizon>()
    };
    public override IReadOnlyList<RelicModel> StartingRelics => new RelicModel[]
    {
        ModelDb.Relic<Bravo>()
    };

    // Placeholder visuals & scenes pointing to Ironclad assets until custom 3D/Spine scenes are created
    public override string? CustomVisualPath => "res://scenes/creature_visuals/ironclad.tscn";
    public override string? CustomCharacterSelectBg => "res://scenes/screens/char_select/char_select_bg_ironclad.tscn";
    public override string? CustomCharacterSelectTransitionPath => "res://materials/transitions/ironclad_transition_mat.tres";
    public override string? CustomCharacterSelectIconPath => "res://images/packed/character_select/char_select_ironclad.png";
    public override string? CustomCharacterSelectLockedIconPath => "res://images/packed/character_select/char_select_ironclad_locked.png";
    public override string? CustomIconTexturePath => "res://images/ui/top_panel/character_icon_ironclad.png";
    public override string? CustomIconOutlineTexturePath => "res://images/ui/top_panel/character_icon_ironclad_outline.png";
    public override string? CustomIconPath => "res://scenes/ui/character_icons/ironclad_icon.tscn";
    public override string? CustomEnergyCounterPath => "res://scenes/combat/energy_counters/ironclad_energy_counter.tscn";
    public override string? CustomRestSiteAnimPath => "res://scenes/rest_site/characters/ironclad_rest_site.tscn";
    public override string? CustomMerchantAnimPath => "res://scenes/merchant/characters/ironclad_merchant.tscn";
    public override string? CustomMapMarkerPath => "res://images/packed/map/icons/map_marker_ironclad.png";
    public override string? CustomTrailPath => "res://scenes/vfx/card_trail_ironclad.tscn";
    public override string? CustomAttackSfx => "event:/sfx/characters/ironclad/ironclad_attack";
    public override string? CustomCastSfx => "event:/sfx/characters/ironclad/ironclad_cast";
    public override string? CustomDeathSfx => "event:/sfx/characters/ironclad/ironclad_die";

    public override string CharacterSelectSfx => "event:/sfx/characters/ironclad/ironclad_select";
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

    public override CreatureAnimator GenerateAnimator(MegaSprite controller)
    {
        return new CreatureAnimator(new AnimState("idle_loop", true), controller);
    }
}
