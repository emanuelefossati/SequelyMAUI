using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SequelyMAUI.Interfaces
{
    public interface IErrorProvider
    {
        public Task ProvideError(string message);
    }
}
