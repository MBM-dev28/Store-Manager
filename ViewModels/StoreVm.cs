using System.ComponentModel.DataAnnotations;

namespace Store_Manager.ViewModels
{
    public class StoreVm
{
        public required Guid Id { get; set; }

        public required int Number { get; set; }

        public required string Name { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? StoreOwner { get; set; }

        public required DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }


        public string? ChainName { get; set; }
}
}
