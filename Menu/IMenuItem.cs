using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Menu
{
    public interface IMenuItem
    {
        string Name { get; }

        void OnMenuClick(Func<IMenuOperation, bool> operation, Func<bool> callback);
    }
}
