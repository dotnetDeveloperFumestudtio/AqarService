using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfardan.Engine.Entities
{
  [System.ComponentModel.DataAnnotations.Schema.Table ("ImagesTable")]
  public partial class Image
  {
    [Key, System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public Guid stream_id { get; set; }
    public byte[] file_stream { get; set; }
    public string name { get; set; }
    public bool is_directory { get; set; }
  }
}
