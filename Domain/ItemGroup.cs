using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace B2S_REST_API.Domain;

public partial class ItemGroup
{
    public int GrpId { get; set; }

    public string? GrpName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
