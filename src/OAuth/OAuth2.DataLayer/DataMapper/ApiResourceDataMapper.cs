using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    internal class ApiResourceDataMapper : DataMapBase<ApiResources, Models.ApiResources>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static ApiResourceDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override ApiResources Map(Models.ApiResources source, ApiResources destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.ApiResources Map(ApiResources source, Models.ApiResources destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
