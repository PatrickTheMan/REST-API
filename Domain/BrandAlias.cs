using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace B2S_REST_API.Domain;

public partial class BrandAlias
{
    public int AliId { get; set; }

    public string AliAlias { get; set; } = null!;

    public int? BrdId { get; set; }
    [JsonIgnore]
    public virtual Brand? Brd { get; set; }
}
