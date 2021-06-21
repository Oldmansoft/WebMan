using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Models
{
    public class TableListModel
    {
        public Guid Id { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "头像")]
        public IFormFile Avatar { get; set; }
    }

    public class TableEditModel
    {
        public Guid Id { get; set; }

        [Display(Name = "名称")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "图像")]
        [Oldmansoft.Html.WebMan.Annotations.FileRequired]
        public IFormFile File { get; set; }
    }

    public class UserFile
    {
        public string FileName { get; set; }

        public long Length { get; set; }

        public string Extension { get; set; }

        public string FileType { get; set; }

        private readonly static string[] Filters = { ".jpg", ".png", ".bmp" };

        public bool IsValid => !string.IsNullOrEmpty(this.Extension) && Filters.Contains(this.Extension);

        private IFormFile file;

        public IFormFile File
        {
            get { return file; }
            set
            {
                if (value != null)
                {
                    file = value;
                    FileType = file.ContentType;
                    Length = file.Length;
                    Extension = file.FileName[file.FileName.LastIndexOf('.')..];
                    if (string.IsNullOrEmpty(FileName)) FileName = FileName;
                }
            }
        }

        public async Task<string> SaveAs(string destinationDir = null)
        {
            if (this.file == null) throw new ArgumentNullException("没有需要保存的文件");
            if (destinationDir != null) Directory.CreateDirectory(destinationDir);
            var newName = DateTime.Now.Ticks;
            var newFile = Path.Combine(destinationDir ?? "", $"{newName}{this.Extension}");
            using (FileStream fs = new FileStream(newFile, FileMode.CreateNew))
            {
                await this.file.CopyToAsync(fs);
                fs.Flush();
            }
            return newFile;
        }
    }
}
