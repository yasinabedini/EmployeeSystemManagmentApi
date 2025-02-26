﻿using System.Domain.Common.Exceptions;
using System.Domain.Common.Rules;

namespace System.Domain.Common.ValueObjects;
public class BusinessId : BaseValueObject<BusinessId>
{
    public static BusinessId FromString(string value) => new(value);
    public static BusinessId FromGuid(Guid value) => new() { Value = value };

    public BusinessId(string value)
    {
        CheckRule(new TheValueMustNotBeEmpty(value));

        if (Guid.TryParse(value, out Guid tempValue))
        {
            Value = tempValue;
        }
        else
        {
            throw new InvalidValueObjectStateException("ValidationErrorInvalidValue", nameof(BusinessId));
        }
    }
    private BusinessId()
    {

    }

    public Guid Value { get; private set; }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator string(BusinessId title) => title.Value.ToString();
    public static implicit operator BusinessId(string value) => new(value);


    public static explicit operator Guid(BusinessId title) => title.Value;
    public static implicit operator BusinessId(Guid value) => new() { Value = value };

}