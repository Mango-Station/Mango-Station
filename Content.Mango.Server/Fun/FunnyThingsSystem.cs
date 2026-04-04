using Content.Mango.Common.CCVar;
using Content.Server.GameTicking;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Random;

namespace Content.Mango.Server.Fun;

public sealed class FunnyThingsSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IRobustRandom _gambling = default!;
    [Dependency] private readonly GameTicker _gameTicker = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<RoundStartAttemptEvent>(OnRoundStartAttempt);
    }

    private void OnRoundStartAttempt(RoundStartAttemptEvent ev)
    {
        var prob = _cfg.GetCVar(MangoCVars.FunProb);
        if (_gambling.Prob(prob / 100f))
            _gameTicker.AddGameRule("CrematoriumFunRule"); // todo i have no fucking idea how to make this system expandable without making its either be 100% hell shift with random shit or 100% nothing
        if (_gambling.Prob(prob / 100f)) // this is what i was talking about
            _gameTicker.AddGameRule("FentbotFunRule");
        if (_gambling.Prob(prob / 100f))
            _gameTicker.AddGameRule("EngiSentryFunRule");
        if (_gambling.Prob(prob / 100f))
            _gameTicker.AddGameRule("FunSkeletonGibRule");
        if (_gambling.Prob(prob / 100f))
            _gameTicker.AddGameRule("HeartRipFunRule");
        if (_gambling.Prob(prob / 100f))
            _gameTicker.AddGameRule("BsoLifelineExplode");
    }

    public bool CheckRule<T>() where T : Component
    {
        var eqe = EntityQueryEnumerator<T>();
        while (eqe.MoveNext(out _, out _))
            return true;
        return false;
    }
}
