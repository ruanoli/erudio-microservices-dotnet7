using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Models;
using GeekShopping.ProductAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;
        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            //Vai ao banco e busca todos os produtos de forma assincrona.
            List<Product> products = await _context.Products.ToListAsync();
            //Depois pegamos essa lista de produtos e mapeamos ela para uma Lista de VOs
            //e devolve para quem está consumindo o método.
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id) ?? new Product();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            //Convertido para entidade para persistir no banco.
            Product product = _mapper.Map<Product>(vo);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //Retorna como um VO novamente.
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == id) ?? new Product();
                if (product.Id <= 0) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
