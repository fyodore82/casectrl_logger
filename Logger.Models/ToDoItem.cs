namespace Logger.Models
{
    [Serializable]
    public class ToDoItem
    {
        public ToDoItem()
        {
            Title = "";
            AccountId = "";
            CreatedBy = "";
        }
        public ToDoItem(ToDoItem originalToDoItem)
        {
            Id = originalToDoItem.Id;
            Title = originalToDoItem.Title;
            Description = originalToDoItem.Description;
            AccountId = originalToDoItem.AccountId;
            CreatedAt = originalToDoItem.CreatedAt;
            CreatedBy = originalToDoItem.CreatedBy;
            ModifedOn = originalToDoItem.ModifedOn;
            ModifiedBy = originalToDoItem.ModifiedBy;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}