using Content.Mango.Shared._Slon;
using Robust.Client.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Timing;

// namespace Content.Goida.Client.Useless;
namespace Content.Mango.Client._Slon;

public sealed class WobbleWobbleSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<WobbleWobbleComponent, ComponentHandleState>(OnHandleState);
    }

    private void OnHandleState(
        EntityUid uid,
        WobbleWobbleComponent component,
        ref ComponentHandleState args)
    {
        if (args.Current is not WobbleWobbleComponentState state)
            return;

        component.Intensity = state.Intensity;
        component.CycleTime = state.CycleTime;
        component.BaseRotation = state.BaseRotation;
    }

    public override void FrameUpdate(float frameTime)
    {
        base.FrameUpdate(frameTime);

        var time = _gameTiming.CurTime.TotalSeconds;
        var query = EntityQueryEnumerator<WobbleWobbleComponent, SpriteComponent>();

        while (query.MoveNext(out var uid, out var swaying, out var sprite))
        {
            var phase = (float)(time % swaying.CycleTime / swaying.CycleTime);
            var factor = MathF.Sin(phase * MathF.PI * 2);
            var angleDeviation = swaying.Intensity * factor * MathF.PI / 180f;
            sprite.Rotation = swaying.BaseRotation + angleDeviation;
        }
    }
}
