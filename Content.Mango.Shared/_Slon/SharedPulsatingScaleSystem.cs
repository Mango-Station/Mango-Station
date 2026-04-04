using System.Numerics;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

// namespace Content.Goida.Useless;
namespace Content.Mango.Shared._Slon;

public sealed class SharedPulsatingScaleSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<PulsatingScaleComponent, ComponentGetState>(OnGetState);
    }

    private void OnGetState(
        EntityUid uid,
        PulsatingScaleComponent component,
        ref ComponentGetState args)
    {
        args.State = new PulsatingScaleComponentState(
            component.Intensity,
            component.CycleTime,
            component.BaseScale
        );
    }
}

[Serializable, NetSerializable]
public sealed class PulsatingScaleComponentState : ComponentState
{
    public float Intensity;
    public float CycleTime;
    public Vector2 BaseScale;

    public PulsatingScaleComponentState(
        float intensity,
        float cycleTime,
        Vector2 baseScale)
    {
        Intensity = intensity;
        CycleTime = cycleTime;
        BaseScale = baseScale;
    }
}
