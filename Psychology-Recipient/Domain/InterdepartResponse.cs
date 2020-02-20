using System;

namespace Psychology_Recipient.Domain
{
    public class InterdepartResponse
    {
        public InterdepartResponse(int id, int statusId)
        {
            Id = id;
            InterdepartStatusId = statusId;
            Request = DateTime.Now;
        }
        public int Id { get; set; }
        public int InterdepartStatusId { get; set; }
        public DateTime Request { get; set; }
    }
}