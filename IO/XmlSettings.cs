using System.Collections.Generic;

namespace Wpf.Frno.SearchAndExtract.IO
{
    public class XmlSettings<TItem>
    {
        public IList<TItem> Settings { get; set; }
    }

    public class XmlStringSettings
    {
        public List<string> Settings { get; set; } 
    }
}
