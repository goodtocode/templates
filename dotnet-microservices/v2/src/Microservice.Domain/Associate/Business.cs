using GoodToCode.Shared.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Microservice.Domain
{
    public class Business : DomainEntity<IBusiness>, IBusiness
    {
        public override string PartitionKey { get; set; } = "Default";
        public override Guid RowKey { get { return BusinessKey; } set { BusinessKey = value; } }
        public Guid BusinessKey { get; set; }
        public string BusinessName { get; set; }
        public string TaxNumber { get; set; }
    }
}
