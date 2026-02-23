namespace Store_Manager.Data.Entities
{
    public class Chain
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }


        //nav property
        public ICollection<Store> Stores { get; set; } = [];
    }
}
