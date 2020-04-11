using System;

namespace ApiLab.Models
{
    public class LabImage
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ContentType { get; set; }

        public long ContentSize { get; set; }

        public byte[] Content { get; set; }
    }
}
