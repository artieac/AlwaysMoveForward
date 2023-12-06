using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace AlwaysMoveForward.Common.DataLayer
{
    /*
    public abstract class MappedListResolver<
        TDomainListItem, 
        TDTOListItem> 
        : ITypeConverter
        where TDomainListItem : class
        where TDTOListItem : class
    {
        public TDTOListItem Convert(TDomainListItem source, TDTOListItem destination, ResolutionContext context)
        {
            IList<TDTOListItem> destinationList = this.GetDestinationList(context);
            
            if (destinationList == null)
            {
                destinationList = new List<TDTOListItem>();
            }

            IList<TDomainListItem> sourceList = this.GetSourceList(context);

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

            return source.New(destinationList, typeof(IList<TDTOListItem>));
        }

        protected abstract IList<TDTOListItem> GetDestinationList(ResolutionContext contex);

        protected abstract IList<TDomainListItem> GetSourceList(ResolutionContext contex);

        protected abstract TDTOListItem FindItemInList(IList<TDTOListItem> destinationList, TDomainListItem searchTarget);

        protected abstract TDomainListItem FindItemInList(IList<TDomainListItem> sourceList, TDTOListItem searchTarget);
    }
    */
}
