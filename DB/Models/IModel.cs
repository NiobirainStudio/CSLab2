using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    // THE FIRST VARIABLE MUST ALWAYS BE THE KEY!
    public interface IModel
    {
        public string GetVisible();
    }
}
