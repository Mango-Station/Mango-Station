using Content.Mango.Server.Fun.Components;
using Content.Mango.Server.Fun.Components.Rules;
using Content.Mango.Shared._Slon;
using Content.Server.Explosion.EntitySystems;
using Content.Server.GameTicking.Rules;
using Robust.Server.Audio;
using Robust.Shared.Audio;
using Robust.Shared.Timing;

namespace Content.Mango.Server.Fun.Systems.Rules;

public sealed class BsoLifelineExplodeFunRuleSystem : GameRuleSystem<BsoLifelineExplodeFunRuleComponent>
{
    [Dependency] private readonly FunnyThingsSystem _fun = default!;
    [Dependency] private readonly AudioSystem _audio = default!;
    [Dependency] private readonly ExplosionSystem _boom = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AntiSkillIssueMeasurementComponent, ComponentInit>(OnInit);
    }

    private void OnInit(EntityUid uid, AntiSkillIssueMeasurementComponent comp, ComponentInit args)
    {
        if (!_fun.CheckRule<BsoLifelineExplodeFunRuleComponent>())
            return;

        _audio.PlayPvs(comp.Sound, uid, new AudioParams().WithVolume(8f));
        EnsureComp<PulsatingScaleComponent>(uid);
        Timer.Spawn(TimeSpan.FromSeconds(comp.Delay), () =>
        {
            if (TerminatingOrDeleted(uid))
                return;

            _boom.QueueExplosion(uid, "Default", 2000f, 4f, 20f, canCreateVacuum: true);
        });
    }
}
