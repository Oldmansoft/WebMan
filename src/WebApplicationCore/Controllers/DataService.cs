using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore
{
    public class DataService
    {
        public static readonly DataService Instance = new DataService();

        private readonly List<Data> Source = new List<Data>();

        public Data Get(Guid id)
        {
            return Source.FirstOrDefault(o => o.Id == id);
        }

        public void Add(Data data)
        {
            data.Id = Guid.NewGuid();
            Source.Add(data);
        }

        public List<Data> List()
        {
            return Source;
        }
    }

    public class Data
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DataFile Avatar { get; set; }
    }

    public class DataFile
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Type { get; set; }

        public static DataFile Create(Microsoft.AspNetCore.Http.IFormFile file)
        {
            var result = new DataFile
            {
                Name = file.FileName,
                Type = file.ContentType
            };

            using (var stream = file.OpenReadStream())
            {
                var reader = new System.IO.BinaryReader(stream);
                reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                result.Content = reader.ReadBytes((int)reader.BaseStream.Length);
            }
            return result;
        }
    }
}
