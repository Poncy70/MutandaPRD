﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutanda.Extensions
{
    public class ObservableCollectionWithEvents<T> : ObservableCollection<T>
    {
        public ObservableCollectionWithEvents()
        {
            this.CollectionChanged += OnCollectionChanged;
        }

        void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AddOrRemoveListToPropertyChanged(e.NewItems, true);
            AddOrRemoveListToPropertyChanged(e.OldItems, false);
        }

        private void AddOrRemoveListToPropertyChanged(IList list, Boolean add)
        {
            if (list == null) { return; }
            foreach (object item in list)
            {
                INotifyPropertyChanged o = item as INotifyPropertyChanged;
                if (o != null)
                {
                    if (add) { o.PropertyChanged += ListItemPropertyChanged; }
                    if (!add) { o.PropertyChanged -= ListItemPropertyChanged; }
                }
                else
                {
                    throw new Exception("INotifyPropertyChanged is required");
                }
            }
        }

        void ListItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnListItemChanged(this, e);
        }

        public delegate void ListItemChangedEventHandler(object sender, PropertyChangedEventArgs e);

        public event ListItemChangedEventHandler ListItemChanged;

        private void OnListItemChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (ListItemChanged != null) { this.ListItemChanged(this, e); }
        }


    }

    
}
