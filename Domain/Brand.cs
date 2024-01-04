using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace B2S_REST_API.Domain;

public partial class Brand
{
    public int BrdId { get; set; }

    public string? BrdName { get; set; }

    [JsonIgnore]
    public virtual ICollection<BrandAlias> BrandAliases { get; set; } = new List<BrandAlias>();
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
