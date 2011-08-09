﻿using System;

namespace nothinbutdotnetprep.utility.filtering
{
    public class ComparableCriteriaFactory<ItemToMatch, PropertyType> : ICreateSpecifications<ItemToMatch, PropertyType>
        where PropertyType : IComparable<PropertyType>
    {
        Func<ItemToMatch, PropertyType> accessor;

        public ComparableCriteriaFactory(Func<ItemToMatch, PropertyType> accessor,
                                         ICreateSpecifications<ItemToMatch, PropertyType> original)
        {
            this.accessor = accessor;
            this.original = original;
        }

        public IMatchAnItem<ItemToMatch> equal_to(PropertyType value)
        {
            return original.equal_to(value);
        }

        public IMatchAnItem<ItemToMatch> equal_to_any(params PropertyType[] possible_values)
        {
            return original.equal_to_any(possible_values);
        }

        public IMatchAnItem<ItemToMatch> not_equal_to(PropertyType value)
        {
            return original.not_equal_to(value);
        }

        public IMatchAnItem<ItemToMatch> create(Condition<ItemToMatch> condition)
        {
            return original.create(condition);
        }

        ICreateSpecifications<ItemToMatch, PropertyType> original;

        public IMatchAnItem<ItemToMatch> greater_than(PropertyType value)
        {
            return create(item => accessor(item).CompareTo(value) > 0);
        }

        public IMatchAnItem<ItemToMatch> between(PropertyType start, PropertyType end)
        {
            return create(item => accessor(item).CompareTo(start) >= 0)
                .and(create(item => accessor(item).CompareTo(end) <= 0));
        }
    }
}