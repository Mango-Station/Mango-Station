using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

// namespace Content.Goida.Useless;
namespace Content.Mango.Shared._Slon;

public sealed class SharedSwayingSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<WobbleWobbleComponent, ComponentGetState>(OnGetState);
    }

    private void OnGetState(
        EntityUid uid,
        WobbleWobbleComponent component,
        ref ComponentGetState args)
    {
        args.State = new WobbleWobbleComponentState(
            component.Intensity,
            component.CycleTime,
            component.BaseRotation
        );
    }
}


[Serializable, NetSerializable]
public sealed class WobbleWobbleComponentState : ComponentState
{
    public float Intensity;
    public float CycleTime;
    public Angle BaseRotation;

    public WobbleWobbleComponentState(
        float intensity,
        float cycleTime,
        Angle baseRotation)
    {
        Intensity = intensity;
        CycleTime = cycleTime;
        BaseRotation = baseRotation;
    }
}
