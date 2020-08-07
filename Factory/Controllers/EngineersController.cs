using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index(string sortOrder, string searchString)
    {
      ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewBag.DateSortParm = sortOrder=="Date" ? "date_desc" : "Date";
      var engineers = from engineer in _db.Engineers
                    select engineer;
      if (!String.IsNullOrEmpty(searchString))
      {
          engineers = engineers.Where(engineer => engineer.EngineerName.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          engineers = engineers.OrderByDescending(engineer => engineer.EngineerName);
          break;
        // case "Date":
        //   engineers = engineers.OrderBy(engineer => engineer.DateofHire);
        //   break;
        // case "date_desc":
        //   engineers = engineers.OrderByDescending(engineer => engineer.DateOfHire);
        //   break;
        default:
          engineers = engineers.OrderBy(engineer => engineer.EngineerName);
          break;
      }
      return View(engineers.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Engineer engineer, int MachineId)
    {
      _db.Engineers.Add(engineer);
      if (MachineId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
      .Include(engineer => engineer.Machines)
      .ThenInclude(join => join.Machine)
      .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }
    public ActionResult Edit(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer, int MachineId)
    {
      if (MachineId !=0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteMachine(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Machine machine, int EngineerId)
    {
      if(EngineerId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}