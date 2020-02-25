using BrainstormApp.ComponentModel;
using System.Collections.Generic;

namespace BrainstormApp.Interfaces
{
    public interface IAnswersService
    {
        IEnumerable<string> GetAnswersByCode(string codeStorage);
        void SetAnswerByCode(string codeStorage, string answer);
    }
}
