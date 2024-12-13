namespace Domain.Abstractions {
   public interface IBaseEntity {
      public Guid Id { get; set; }
      public DateTime CreatedOn { get; set; }
      public DateTime ModifiedOn { get; set; }
      public Guid CreatedBy { get; set; }
      public Guid ModifiedBy { get; set; }
   }
}