using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Domain.Common.ValueObjects;

namespace System.Domain.Common.ValueObjects.Conversion;

public class DescriptionConversion : ValueConverter<Description, string>
{
    public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
    {

    }
}
