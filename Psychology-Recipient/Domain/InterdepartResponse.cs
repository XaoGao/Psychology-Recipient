using System;

namespace Psychology_Recipient.Domain
{
    public class InterdepartResponse
    {
        public InterdepartResponse(int id, int statusId)
        {
            Id = id;
            InterdepartStatusId = statusId;

            DateTime theDate = DateTime.Now;
            Request = theDate.ToString("yyyy-MM-dd H:mm:ss");
        }
        public int Id { get; set; }
        public int InterdepartStatusId { get; set; }
        public string Request { get; set; }
    }
}