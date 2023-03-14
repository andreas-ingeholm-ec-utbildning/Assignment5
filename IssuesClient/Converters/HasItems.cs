using System.Collections;
using Common.Utility;

namespace IssuesClient.Converters;

public class HasItems : BetterConverter<IList, bool>
{

    public override bool Convert(IList? value) =>
        value?.Count > 0;

}