using Common.Utility;

namespace IssuesClient.Converters;

public class InvertBool : BetterConverter<bool, bool>
{

    public override bool Convert(bool value) =>
        !value;

}
