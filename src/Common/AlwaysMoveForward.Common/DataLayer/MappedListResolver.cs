using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace AlwaysMoveForward.Common.DataLayer
{
    /// <summary>
    /// Abstract base class for resolving mapped lists between domain and DTO types.
    /// Updated for AutoMapper 13.x API.
    /// </summary>
    /// <typeparam name="TSource">The source type containing the list</typeparam>
    /// <typeparam name="TDestination">The destination type containing the list</typeparam>
    /// <typeparam name="TDomainListItem">The domain list item type</typeparam>
    /// <typeparam name="TDTOListItem">The DTO list item type</typeparam>
    public abstract class MappedListResolver<TSource, TDestination, TDomainListItem, TDTOListItem>
        : IValueResolver<TSource, TDestination, IList<TDTOListItem>>
        where TDomainListItem : class
        where TDTOListItem : class
    {
        public IList<TDTOListItem> Resolve(TSource source, TDestination destination, IList<TDTOListItem> destMember, ResolutionContext context)
        {
            IList<TDTOListItem> destinationList = destMember ?? new List<TDTOListItem>();
            IList<TDomainListItem> sourceList = this.GetSourceList(source);

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
                        destinationList.Add(context.Mapper.Map<TDomainListItem, TDTOListItem>(sourceList[i]));
                    }
                    else
                    {
                        context.Mapper.Map(sourceList[i], destinationListAddUpdateItem);
                    }
                }
            }

            return destinationList;
        }

        protected abstract IList<TDomainListItem> GetSourceList(TSource source);

        protected abstract TDTOListItem FindItemInList(IList<TDTOListItem> destinationList, TDomainListItem searchTarget);

        protected abstract TDomainListItem FindItemInList(IList<TDomainListItem> sourceList, TDTOListItem searchTarget);
    }
}
