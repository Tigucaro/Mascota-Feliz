using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioVeterinario : IRepositorioVeterinario
    {
         /// <summary>
        /// Referencia al contexto de Dueno
        /// </summary>
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//


        public RepositorioVeterinario (AppContext appContext)
        {
            _appContext=appContext

        }

        public Veterinario AddVeterinario(Veterinario veterinario)
        {
            var VeterinarioAdicionado=_appContext.Veterinarios.Add(veterinario)
            _appContext.SaveChanges();
            return VeterinarioAdicionado.Entity;
        }
        void Veterinario DeleteVeterinario(int IdVeterinario)
        {   var veterinarioEncontrado=_appContext.Veterinarios.FirstOrDefault(d => d.Id == IdVeterinario);
            if(veterinarioEncontrado == null)
               return;
               _appContext.Veterinarios.Remove(veterinarioEncontrado);
               _appContext.SaveChanges();
        }
        public Veterinario UpdateVeterinario(Veterinario veterinario)
        {
            var veterinarioEncontrado=_appContext.Veterinarios.FirstOrDefault(d => d.Id == veterinario.Id);
            if(veterinarioEncontrado=! null)
            {
                veterinarioEncontrado.Nombres = veterinario.Nombres;
                veterinarioEncontrado.Apellidos = veterinario.Apellidos;
                veterinarioEncontrado.Direccion = veterinario.Direccion;
                veterinarioEncontrado.Telefono = veterinario.Telefono;
                veterinarioEncontrado.Correo = veterinario.Correo;
                _appContext.SaveChanges();

            }
            return veterinarioEncontrado;
        }
        public IEnumerable<Veterinario> GetAllVeterinarios()
        {
            return GetAllVeterinarios_();
        }

        public IEnumerable<Veterinario> GetVeterinariosPorFiltro(string filtro)
        {
            var veterinarios = GetAllVeterinarios(); // Obtiene todos los saludos
            if (veterinarios != null)  //Si se tienen saludos
            {
                if (!String.IsNullOrEmpty(filtro)) // Si el filtro tiene algun valor
                {
                    veterinarios = veterinarios.Where(s => s.Nombres.Contains(filtro));
                }
            }
            return veterinarios;
        }

        public IEnumerable<Veterinario> GetAllVeterinarios_()
        {
            return _appContext.Veterinarios;
        }

        public Veterinario GetVeterinario(int IdVeterinario)
        {
            return _appContext.Veterinarios.FirstOrDefault(d => d.Id == IdVeterinario);
        }


    }
}