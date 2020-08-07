using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Engineer
  {
    public Engineer()
    {
      this.Machines = new HashSet<EngineerMachine>();
    }
    public int EngineerId { get; set; }
    public string EngineerName { get; set; }
    public bool IsWorking  { get; set; }
    public DateTime DateofHire { get; set; }
    public ICollection<EngineerMachine> Machines { get; set; }


  }
}