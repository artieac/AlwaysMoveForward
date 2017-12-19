using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    public abstract class MappedListResolver<TSourceContainer, TDestinationContainer, TSourceListItem, TDestinationListItem>
       where TSourceContainer : class
       where TDestinationContainer : class
       where TSourceListItem : class
       where TDestinationListItem : class
    {
        public MappedListResolver(TSourceContainer sourceContainer, TDestinationContainer destinationContainer)
        {
            this.SourceContainer = sourceContainer;
            this.DestinationContainer = destinationContainer;
        }

        protected TSourceContainer SourceContainer { get; private set; }
        protected TDestinationContainer DestinationContainer { get; private set; }

        protected abstract IList<TSourceListItem> GetSourceList();
        protected abstract IList<TDestinationListItem> GetDestinationList();
        protected abstract TDestinationListItem FindItemInList(IList<TDestinationListItem> destinationList, TSourceListItem searchTarget);
        protected abstract TSourceListItem FindItemInList(IList<TSourceListItem> sourceList, TDestinationListItem searchTarget);

        protected abstract void SetDestinationContainer(TDestinationListItem listItem, TDestinationContainer destinationContainer);

        public IList<TDestinationListItem> MapList()
        {
            IList<TDestinationListItem> destinationList = this.GetDestinationList();

            if (destinationList == null)
            {
                destinationList = new List<TDestinationListItem>();
            }

            IList<TSourceListItem> sourceList = this.GetSourceList();

            if (sourceList != null)
            {
                // go through and remove any items that were removed in the domain and need to be removed in the dto
                for (int i = destinationList.Count - 1; i > -1; i--)
                {
                    TSourceListItem destinationListDeleteItem = this.FindItemInList(sourceList, destinationList[i]);

                    if (destinationListDeleteItem == null)
                    {
                        destinationList.RemoveAt(i);
                    }
                }

                // add in all of the new items, or update items already in the list
                for (int i = 0; i < sourceList.Count; i++)
                {
                    TDestinationListItem destinationListAddUpdateItem = this.FindItemInList(destinationList, sourceList[i]);

                    if (destinationListAddUpdateItem == null)
                    {
                        destinationList.Add(Mapper.Map<TSourceListItem, TDestinationListItem>(sourceList[i]));
                    }
                    else
                    {
                        destinationListAddUpdateItem = Mapper.Map<TSourceListItem, TDestinationListItem>(sourceList[i], destinationListAddUpdateItem);
                    }
                }

                foreach (var child in destinationList)
                {
                    this.SetDestinationContainer(child, this.DestinationContainer);
                }
            }

            return destinationList;
        }
    }
}
