using B2S_REST_API.Domain;

namespace B2S_REST_API.Models
{
    public class TestItemsFilter
    {

        public static List<Brand> FilterBrands(List<Brand> brands)
        {
#if RELEASE
            return brands.Where(b => b.BrdId != 7 && b.BrdId != 1007).ToList();
#elif DEBUG
            return brands;
#endif
        }

        public static Brand? FilterBrands(Brand brand)
        {
#if RELEASE
            return brand.BrdId != 7 && brand.BrdId != 1007 ? brand : null;
#elif DEBUG
            return brand;
#endif
        }

        public static List<BrandAlias> FilterBrandAlias(List<BrandAlias> alias)
        {
#if RELEASE
            return alias.Where(a => a.AliId != 22 && a.AliId != 2023).ToList();
#elif DEBUG
            return alias;
#endif
        }
        public static BrandAlias? FilterBrandAlias(BrandAlias alias)
        {
#if RELEASE
            return alias.AliId != 22 && alias.AliId != 2023 ? alias : null;
#elif DEBUG
            return alias;
#endif
        }

        public static List<ItemGroup> FilterItemGroups(List<ItemGroup> itemGroups)
        {
#if RELEASE
            return itemGroups.Where(ig => ig.GrpId != 4 && ig.GrpId != 1005).ToList();
#elif DEBUG
            return itemGroups;
#endif
        }

        public static ItemGroup? FilterItemGroups(ItemGroup itemGroup)
        {
#if RELEASE
            return itemGroup.GrpId != 4 && itemGroup.GrpId != 1005 ? itemGroup : null;
#elif DEBUG
            return itemGroup;
#endif
        }

        public static List<Product> FilterProducts(List<Product> products)
        {
#if RELEASE
            return products.Where(p => p.PrdId != 6684 && p.PrdId != 7688).ToList();
#elif DEBUG
            return products;
#endif
        }

        public static Product? FilterProducts(Product products)
        {
#if RELEASE
            return products.PrdId != 6684 && products.PrdId != 7688 ? products : null;
#elif DEBUG
            return products;
#endif
        }

    }
}
