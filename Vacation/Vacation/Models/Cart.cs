using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation.Models
{
  [Serializable]
  public class Cart
  {
    public Tour Tour{ get; set; }
    public int Quatity{ get; set; }
  }
}