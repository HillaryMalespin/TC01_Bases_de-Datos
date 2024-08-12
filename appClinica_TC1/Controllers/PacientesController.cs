using Microsoft.AspNetCore.Mvc;
using appClinica_TC1.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace appClinica_TC1.Controllers
{
    public class PacientesController : Controller
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "pacientes.txt");

        public IActionResult Index()
        {
            var pacientes = GetPacientes();
            return View(pacientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            var pacientes = GetPacientes();
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            SavePacientes(pacientes);
            return RedirectToAction("Index");
        }

        private List<Paciente> GetPacientes()
        {
            var pacientes = new List<Paciente>();

            if (System.IO.File.Exists(_filePath))
            {
                var lines = System.IO.File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var fields = line.Split(',');
                    if (fields.Length == 5) // Ensure correct number of fields
                    {
                        pacientes.Add(new Paciente
                        {
                            Id = int.Parse(fields[0]),
                            Cedula = fields[1],
                            Nombre = fields[2],
                            FechaNacimiento = DateTime.Parse(fields[3]),
                            Edad = int.Parse(fields[4]),
                            Direccion = fields[5]
                        });
                    }
                }
            }
            return pacientes;
        }

        private void SavePacientes(List<Paciente> pacientes)
        {
            var lines = pacientes.Select(p => $"{p.Id},{p.Cedula},{p.Nombre},{p.FechaNacimiento.ToShortDateString()},{p.Edad},{p.Direccion}");
            System.IO.File.WriteAllLines(_filePath, lines);
        }
    }
}
