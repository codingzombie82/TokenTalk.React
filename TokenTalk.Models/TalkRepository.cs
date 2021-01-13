using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenTalk.Models
{
    public class TalkRepository : ITalkRepository
    {
        private readonly TalkAppDbContext _context;
        private readonly ILogger _logger;

        public TalkRepository(TalkAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(TalkRepository));
        }


        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        #region 입력
        public async Task<Talk> AddAsync(Talk model)
        {
            try
            {
                _context.Talks.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception e){

                _logger?.LogError($"ERROR({nameof(AddAsync)}) : {e.Message}");
            }

            return model;
        }
        #endregion

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region 삭제하기
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
               var model = await _context.Talks.FindAsync(id);
               _context.Remove(model);
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"ERROR({nameof(DeleteAsync)}) : {e.Message}");
            }

            return false;

        } 
        #endregion

        #region 전체보기
        public async Task<List<Talk>> GetAllAsync()
        {
            return await _context.Talks.OrderByDescending(m => m.Id)
                .ToListAsync();
        }
        #endregion

        #region 상세보기
        public async Task<Talk> GetByIdAsync(int id)
        {
            return await _context.Talks
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        #endregion

        #region 수정하기
        public async Task<bool> UpdateAsync(Talk model)
        {
            try
            {
                _context.Update(model);
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"ERROR({nameof(UpdateAsync)}) : {e.Message}");
            }

            return false;
        }

        //Task<ArticleSet<Talk, int>> ICrudRepositoryBase<Talk, int>.GetArticlesAsync<TParentIdentifier>(int pageIndex, int pageSize, string searchField, string searchQuery, string sortOrder, TParentIdentifier parentIdentifier)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
