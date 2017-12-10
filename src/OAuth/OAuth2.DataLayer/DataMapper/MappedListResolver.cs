using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    public abstract class MappedListResolver<TSourceContainer, TDestinationContainer, TDomainListItem, TDTOListItem>
       where TSourceContainer : class
       where TDestinationContainer : class
       where TDomainListItem : class
       where TDTOListItem : class
    {
        public MappedListResolver(TSourceContainer sourceContainer, TDestinationContainer destinationContainer)
        {
            this.SourceContainer = sourceContainer;
            this.DestinationContainer = destinationContainer;
        }

        protected TSourceContainer SourceContainer { get; private set; }
        protected TDestinationContainer DestinationContainer { get; private set; }

        protected abstract IList<TDomainListItem> GetSourceList(TSourceContainer source);
        protected abstract IList<TDTOListItem> GetDestinationList(TDestinationContainer destination);
        protected abstract TDTOListItem FindItemInList(IList<TDTOListItem> destinationList, TDomainListItem searchTarget);
        protected abstract TDomainListItem FindItemInList(IList<TDomainListItem> sourceList, TDTOListItem searchTarget);

        public void MapList()
        {
            IList<TDTOListItem> destinationList = this.GetDestinationList(this.DestinationContainer);

            if (destinationList == null)
            {
                destinationList = new List<TDTOListItem>();
            }

            IList<TDomainListItem> sourceList = this.GetSourceList(this.SourceContainer);

            if (sourceList != null)
            {
                // go through and remove any items that were removed in the domain and need to be removed in the dto
                for (int i = destinationList.Count - 1; i > -1; i--)
                {
                    TDomainListItem destinationListDeleteItem = this.FindItemInList(sourceList, destinationList[i]);

                    if (destinationListDeleteItem == null)
                    {
                        destinationList.RemoveAt(i);
                    }
                }

                // add in all of the new items, or update items already in the list
                for (int i = 0; i < sourceList.Count; i++)
                {
                    TDTOListItem destinationListAddUpdateItem = this.FindItemInList(destinationList, sourceList[i]);

                    if (destinationListAddUpdateItem == null)
                    {
                        destinationList.Add(Mapper.Map<TDomainListItem, TDTOListItem>(sourceList[i]));
                    }
                    else
                    {
                        destinationListAddUpdateItem = Mapper.Map<TDomainListItem, TDTOListItem>(sourceList[i], destinationListAddUpdateItem);
                    }
                }
            }
        }
    }
}
