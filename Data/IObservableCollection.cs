using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public interface IObservableCollection<T> : IProducerConsumerCollection<T>, IEnumerable<T>, ICollection, IEnumerable, INotifyCollectionChanged
    {
    }
}
