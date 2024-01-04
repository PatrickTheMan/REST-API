using B2S_REST_API.Domain;
using System.Text.RegularExpressions;

namespace Algorithms
{
    internal partial class ProductFinder
    {
        private ProductRequest _requestBase = new() { Amount = 10 };
        private List<BrandAlias> _brandAlias = new();

        [GeneratedRegex("/[0-9]/")] private static partial Regex NumbersRegex();

        public ProductFinder(List<BrandAlias> brandAlias)
        {
            _brandAlias = brandAlias;
        }

        /// <returns> All <see cref="ProductRequest"/> candidates in a <see cref="List{T}"/> </returns>
        public List<ProductRequest> GetProductRequests(object input)
        {

            List<string> term;

            term = input switch
            {
                string s => s.Split(' ').ToList(),
                List<string> l => l,
                _ => throw new ArgumentException($"Invalid input. Must be string or List<string>. Received: {input.GetType()}")
            };

            List<string> keyCandidates = new();
            bool hasBrand = false;

            foreach (string s in term)
            {
				if (IsBrand(s))
                {
                    _requestBase.Brand = s;
                    hasBrand = true;
                    continue;
                } // UPC - TODO
                if (IsEAN(s))
                {
                    _requestBase.EAN = s;
					keyCandidates.Add(string.Empty);
					continue;
                }
                keyCandidates.Add(s);
            }

            List<ProductRequest> result = new();

            foreach (string k in keyCandidates)
            {
                _requestBase.ProductNumber = k;
                _requestBase.TypeNumber = k;
                result.Add(new ProductRequest(_requestBase));
            }

            return result;
        }

        private bool IsBrand(string term)
        {

            foreach (BrandAlias alias in _brandAlias)
            {
                if (alias.AliAlias.ToLower() == term.ToLower())
                    return true;
            }
            return false;
        }

        private bool IsEAN(string term)
        {
            if (term.Length == 8 || term.Length == 13)
            {
                if (NumbersRegex().IsMatch(term))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
