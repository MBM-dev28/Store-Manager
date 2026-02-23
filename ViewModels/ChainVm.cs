using System.ComponentModel.DataAnnotations;

namespace Store_Manager.ViewModels
{
    public class ChainVm
{
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }


        public int StoreCount { get; set; }
    }
}
