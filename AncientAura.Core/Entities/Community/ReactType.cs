using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReactType
    {
        Love,
        Like,
        Sad,
        Angery,
        Haha
    }
}
