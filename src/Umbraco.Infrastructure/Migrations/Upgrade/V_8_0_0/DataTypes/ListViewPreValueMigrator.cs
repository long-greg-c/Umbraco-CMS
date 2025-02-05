﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Umbraco.Extensions;

namespace Umbraco.Cms.Infrastructure.Migrations.Upgrade.V_8_0_0.DataTypes
{
    class ListViewPreValueMigrator : DefaultPreValueMigrator
    {
        public override bool CanMigrate(string editorAlias)
            => editorAlias == "Umbraco.ListView";

        protected override IEnumerable<PreValueDto> GetPreValues(IEnumerable<PreValueDto> preValues)
        {
            return preValues.Where(preValue => preValue.Alias != "displayAtTabNumber");
        }

        protected override object? GetPreValueValue(PreValueDto preValue)
        {
            if (preValue.Alias == "pageSize")
            {
                return int.TryParse(preValue.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i) ? (int?)i : null;
            }

            return preValue.Value?.DetectIsJson() ?? false ? JsonConvert.DeserializeObject(preValue.Value) : preValue.Value;
        }
    }
}
