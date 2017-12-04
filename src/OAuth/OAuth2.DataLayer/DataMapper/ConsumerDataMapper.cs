using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.Core.Common.DataLayer;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    /// <summary>
    /// A data mapper going to/from the domain model and the dto
    /// </summary>
    internal class ConsumerDataMapper : DataMapBase<Consumer, Models.Consumers>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static ConsumerDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Consumer Map(Models.Consumers source, Consumer destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.Consumers Map(Consumer source, Models.Consumers destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
