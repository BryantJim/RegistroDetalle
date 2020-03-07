using System;
using System.Collections.Generic;
using System.Text;
using RegistroDetalle.Entidades;
using RegistroDetalle.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
  //BLL metodos de la clase
namespace RegistroDetalle.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Personas.Add(persona) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = PersonasBLL.Buscar(persona.PersonaId);
                foreach(var item in Anterior.Telefonos)
                {
                    if (!persona.Telefonos.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }

                foreach(var item in persona.Telefonos)
                {
                    var Estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = Estado;
                }
                db.Entry(persona).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Eliminar = PersonasBLL.Buscar(id);
                db.Entry(Eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar(int id)
        {
            Personas persona = new Personas();
            Contexto db = new Contexto();

            try
            {
                persona = db.Personas.Include(x => x.Telefonos)
                     .Where(x => x.PersonaId == id)
                     .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return persona;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}
