using System;
using System.Collections.Generic;
using System.Linq;
using TechBazaar.Domain.Dto.CartProduct;

namespace TechBazaar.Domain.Dto.Cart
{
    public sealed record CartDto(IEnumerable<CartProductDto> CartProducts, decimal cartTotalPrice);
}