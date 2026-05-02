using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;

namespace Content.Inky.Server.Fun.Components.Rules;

[RegisterComponent]
public sealed partial class RoundstartGameruleChooserRuleComponent : Component
{
    [DataField]
    public List<string> Rules = new();

    [DataField]
    public bool FunOnly;
}
