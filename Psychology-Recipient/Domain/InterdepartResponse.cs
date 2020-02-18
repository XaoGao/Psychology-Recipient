namespace Psychology_Recipient.Domain
{
    public class InterdepartResponse
    {
        public InterdepartResponse(int id, int statusId)
        {
            Id = id;
            statusId = InterdepartStatusId;
        }
        public int Id { get; set; }
        public int InterdepartStatusId { get; set; }
    }
}