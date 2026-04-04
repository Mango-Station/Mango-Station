using System.Numerics;
using Content.Mango.Shared._Slon;
using Robust.Client.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Timing;

// namespace Content.Goida.Client.Useless;
namespace Content.Mango.Client._Slon;

public sealed class PulsatingScaleSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<PulsatingScaleComponent, ComponentHandleState>(OnHandleState);
    }

    private void OnHandleState(
        EntityUid uid,
        PulsatingScaleComponent component,
        ref ComponentHandleState args)
    {
        if (args.Current is not PulsatingScaleComponentState state)
            return;

        component.Intensity = state.Intensity;
        component.CycleTime = state.CycleTime;
        component.BaseScale = state.BaseScale;
    }

    public override void FrameUpdate(float frameTime)
    {
        base.FrameUpdate(frameTime);

        var time = _gameTiming.CurTime.TotalSeconds;
        var query = EntityQueryEnumerator<PulsatingScaleComponent, SpriteComponent>();

        while (query.MoveNext(out var uid, out var pulsating, out var sprite))
        {
            var phase = (float)(time % pulsating.CycleTime / pulsating.CycleTime);

            var factor = MathF.Sin(phase * MathF.PI * 2);

            var scaleMod = 1f + factor * pulsating.Intensity;

            sprite.Scale = new Vector2(
                pulsating.BaseScale.X / scaleMod,
                pulsating.BaseScale.Y * scaleMod
            );
        }
    }
}
