using BrainstormApp.ComponentModel;
using BrainstormApp.Interfaces;

namespace BrainstormApp.Services
{
    public class CodeService : ICodeService
    {
        public bool CheckCode(Code code)
        {
            return code.CodeValue.Equals("123456");
        }
    }
}
