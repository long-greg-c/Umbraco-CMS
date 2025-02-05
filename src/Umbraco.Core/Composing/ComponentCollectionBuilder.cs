﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Logging;

namespace Umbraco.Cms.Core.Composing
{
    /// <summary>
    /// Builds a <see cref="ComponentCollection"/>.
    /// </summary>
    public class ComponentCollectionBuilder : OrderedCollectionBuilderBase<ComponentCollectionBuilder, ComponentCollection, IComponent>
    {
        private const int LogThresholdMilliseconds = 100;

        public ComponentCollectionBuilder()
        { }

        protected override ComponentCollectionBuilder This => this;

        protected override IEnumerable<IComponent> CreateItems(IServiceProvider factory)
        {
            var logger = factory.GetRequiredService<IProfilingLogger>();

            using (logger.DebugDuration<ComponentCollectionBuilder>($"Creating components. (log when >{LogThresholdMilliseconds}ms)", "Created."))
            {
                return base.CreateItems(factory);
            }
        }

        protected override IComponent CreateItem(IServiceProvider factory, Type itemType)
        {
            var logger = factory.GetRequiredService<IProfilingLogger>();

            using (logger.DebugDuration<ComponentCollectionBuilder>($"Creating {itemType.FullName}.", $"Created {itemType.FullName}.", thresholdMilliseconds: LogThresholdMilliseconds))
            {
                return base.CreateItem(factory, itemType);
            }
        }
    }
}
