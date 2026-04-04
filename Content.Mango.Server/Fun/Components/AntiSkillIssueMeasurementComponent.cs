using Robust.Shared.Audio;

namespace Content.Mango.Server.Fun.Components;

/// <summary>
/// Jarvis, explode.
/// </summary>
[RegisterComponent]
public sealed partial class AntiSkillIssueMeasurementComponent : Component
{
    /// <summary>
    /// Delay after which jarvis will explode the entity
    /// </summary>
    [ViewVariables]
    public float Delay = 1.4f;

    [DataField]
    public SoundSpecifier Sound = new SoundPathSpecifier("/Audio/_Mango/Fun/skibidibopmmdada.ogg");
}
