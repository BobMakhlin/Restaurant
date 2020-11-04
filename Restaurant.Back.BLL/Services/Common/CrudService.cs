using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Restaurant.Back.Repository.Common;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Back.BLL.Services.Common
{
    public class CrudService<TDbElement, TDtoElement, TKey>
       : ICrudService<TDtoElement, TKey>
       where TDbElement : class, new()
       where TDtoElement : class, new()

    {
        #region Fields
        ICrudRepository<TDbElement, TKey> m_repository;
        IMapper m_mapper;
        #endregion

        public CrudService(ICrudRepository<TDbElement, TKey> repository)
        {
            m_repository = repository;
            m_mapper = GetMapper();
        }

        #region Methods

        protected virtual IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<TDbElement, TDtoElement>().ReverseMap();
                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }


        public IQueryable<TDtoElement> GetAll()
        {
            var collection = m_repository
                .GetAll()
                .Select(item => m_mapper.Map<TDtoElement>(item));

            return collection;
        }
        public async Task<TDtoElement> GetAsync(TKey key)
        {
            var item = await m_repository.GetAsync(key);
            var target = m_mapper.Map<TDtoElement>(item);
            return target;
        }

        public async Task<TDtoElement> AddAsync(TDtoElement item)
        {
            var target = m_mapper.Map<TDbElement>(item);
            await m_repository.AddAsync(target);
            await m_repository.SaveAsync();
            return m_mapper.Map<TDtoElement>(target);
        }
        public async Task<TDtoElement> UpdateAsync(TDtoElement item)
        {
            var target = m_mapper.Map<TDbElement>(item);
            m_repository.Update(target);
            await m_repository.SaveAsync();
            return m_mapper.Map<TDtoElement>(target);
        }

        public async Task<TDtoElement> DeleteAsync(TDtoElement item)
        {
            var target = m_mapper.Map<TDbElement>(item);
            m_repository.Delete(target);
            await m_repository.SaveAsync();
            return m_mapper.Map<TDtoElement>(target);
        }

        public IQueryable<TDtoElement> Where(Expression<Func<TDtoElement, bool>> predicate)
        {
            var targetPredicate = m_mapper.Map<Expression<Func<TDbElement, bool>>>(predicate);

            var result = m_repository
                .Where(targetPredicate)
                .Select(item => m_mapper.Map<TDtoElement>(item));

            return result;
        }

        #endregion
    }
}
