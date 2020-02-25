using BrainstormApp.ComponentModel;
using BrainstormApp.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace BrainstormApp.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly IMemoryCache _memoryCache;

        public AnswersService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<string> GetAnswersByCode(string codeStorage)
        {
            if (_memoryCache.TryGetValue(codeStorage, out IEnumerable<string> answers))
                return answers;
            else
                return Enumerable.Empty<string>();
        }
        public void SetAnswerByCode(string codeStorage, string answer)
        {
           var answers = GetAnswersByCode(codeStorage).ToList();
            answers.Add(answer);

            _memoryCache.Set(codeStorage, answers);
        }


    }
}
