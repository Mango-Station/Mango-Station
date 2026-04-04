using System.Numerics;
using Robust.Shared.GameStates;

// namespace Content.Goida.Useless;
namespace Content.Mango.Shared._Slon;

[RegisterComponent, NetworkedComponent]
public sealed partial class PulsatingScaleComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite)] // mf rider
    [DataField]
    public float Intensity { get; set; } = 0.65f;

    [ViewVariables(VVAccess.ReadWrite)]
    [DataField]
    public float CycleTime { get; set; } = 0.3f;

    [ViewVariables(VVAccess.ReadWrite)]
    [DataField]
    public Vector2 BaseScale { get; set; } = Vector2.One;
}
