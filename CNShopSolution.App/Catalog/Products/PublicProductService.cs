﻿using CNShopSolution.App.Catalog.Products.DTOS;
using CNShopSolution.App.Catalog.Products.DTOS.Mangager;
using CNShopSolution.App.Catalog.Products.DTOS.Public;
using CNShopSolution.App.Dtos;
using CNShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CNShopSolution.App.Catalog.Products
{
    public class PublicProductService : IPuclicProductService
    {
        private readonly CNShopDbContext _context;
        public PublicProductService(CNShopDbContext context)
        {
            _context = context;
        }

        public  async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPaingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                     
                        select new { p, pt, pic };
  

            if (request.Category_Id.HasValue && request.Category_Id.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.Category_Id);
            }
            //paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.Id,
                    Name = x.pt.Name,
                    CreateDate = x.p.CreateDate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount

                }).ToListAsync();

            var pageresult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageresult;
        }
    }
}