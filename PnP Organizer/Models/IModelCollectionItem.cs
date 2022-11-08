using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PnP_Organizer.Models
{
    public interface IModelCollectionItem
    {
        public ObservableCollection<IModelCollectionItem> ParentCollection { get; }
    }
}
