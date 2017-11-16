using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.Core.Common.DataLayer;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    internal class ProtectedResourceDataMapper : DataMapBase<ProtectedResource, DTO.ProtectedResource>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static ProtectedResourceDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override ProtectedResource Map(DTO.ProtectedResource source, ProtectedResource destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.ProtectedResource Map(ProtectedResource source, DTO.ProtectedResource destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
