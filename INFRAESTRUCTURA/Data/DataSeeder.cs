using DOMINIO.Entidades;
using DOMINIO.Enumeradores;

namespace Infraestructura.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(VoluntariadoDbContext contexto)
        {
            if (contexto.Organizaciones.Any())
                return;

            var fechaBase = DateTime.UtcNow;

            // Organizaciones
            var org1 = new Organizacion
            {
                Nombre = "Fundaci\u00f3n Esperanza",
                Descripcion = "ONG dedicada a la educaci\u00f3n infantil en comunidades vulnerables",
                Contacto = "Mar\u00eda L\u00f3pez",
                Email = "contacto@fundacionesperanza.org",
                Telefono = "+591 72210001",
                Direccion = "Av. Siempre Viva 123, La Paz",
                FechaRegistro = fechaBase.AddMonths(-6)
            };

            var org2 = new Organizacion
            {
                Nombre = "EcoVida Bolivia",
                Descripcion = "Proyectos de reforestaci\u00f3n y conservaci\u00f3n ambiental",
                Contacto = "Carlos Mendoza",
                Email = "info@ecovida.bo",
                Telefono = "+591 72210002",
                Direccion = "Calle Naturaleza 456, Cochabamba",
                FechaRegistro = fechaBase.AddMonths(-4)
            };

            var org3 = new Organizacion
            {
                Nombre = "Manos Solidarias",
                Descripcion = "Apoyo a adultos mayores y centros de acogida",
                Contacto = "Ana R\u00edos",
                Email = "contacto@manossolidarias.org",
                Telefono = "+591 72210003",
                Direccion = "Av. Solidaridad 789, Santa Cruz",
                FechaRegistro = fechaBase.AddMonths(-2)
            };

            contexto.Organizaciones.AddRange(org1, org2, org3);
            await contexto.SaveChangesAsync();

            // Voluntarios
            var vol1 = new Voluntario
            {
                Nombre = "Pedro",
                Apellido = "Garc\u00eda",
                Email = "pedro.garcia@email.com",
                Telefono = "+591 71110001",
                Direccion = "Av. Bol\u00edvar 100",
                Cedula = "1234567LP",
                FechaNacimiento = new DateTime(1995, 5, 15),
                FechaRegistro = fechaBase.AddMonths(-5),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 40,
                OrganizacionId = org1.Id
            };

            var vol2 = new Voluntario
            {
                Nombre = "Luc\u00eda",
                Apellido = "Mamani",
                Email = "lucia.mamani@email.com",
                Telefono = "+591 71110002",
                Direccion = "Calle 10 Nro 200",
                Cedula = "7654321LP",
                FechaNacimiento = new DateTime(1998, 8, 22),
                FechaRegistro = fechaBase.AddMonths(-4),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 25,
                OrganizacionId = org1.Id
            };

            var vol3 = new Voluntario
            {
                Nombre = "Jorge",
                Apellido = "Quispe",
                Email = "jorge.quispe@email.com",
                Telefono = "+591 71110003",
                Direccion = "Av. Ecol\u00f3gica 500",
                Cedula = "9876543CB",
                FechaNacimiento = new DateTime(2000, 1, 10),
                FechaRegistro = fechaBase.AddMonths(-3),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 15,
                OrganizacionId = org2.Id
            };

            var vol4 = new Voluntario
            {
                Nombre = "Sof\u00eda",
                Apellido = "Paredes",
                Email = "sofia.paredes@email.com",
                Telefono = "+591 71110004",
                Direccion = "Calle Amistad 300",
                Cedula = "4567890SC",
                FechaNacimiento = new DateTime(2002, 11, 3),
                FechaRegistro = fechaBase.AddMonths(-2),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 10,
                OrganizacionId = org3.Id
            };

            var vol5 = new Voluntario
            {
                Nombre = "Diego",
                Apellido = "Morales",
                Email = "diego.morales@email.com",
                Telefono = "+591 71110005",
                Direccion = "Av. Principal 777",
                Cedula = "3216547LP",
                FechaNacimiento = new DateTime(1993, 7, 19),
                FechaRegistro = fechaBase.AddMonths(-1),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 0,
                OrganizacionId = null
            };

            contexto.Voluntarios.AddRange(vol1, vol2, vol3, vol4, vol5);
            await contexto.SaveChangesAsync();

            // Actividades
            var act1 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Clases de Apoyo Escolar",
                Descripcion = "Refuerzo escolar en matem\u00e1ticas y lenguaje para ni\u00f1os de primaria",
                FechaInicio = fechaBase.AddDays(10),
                FechaFin = fechaBase.AddDays(40),
                Ubicacion = "Centro Comunitario Villa Esperanza",
                VoluntariosRequeridos = 5,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = fechaBase.AddDays(-15)
            };

            var act2 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Campa\u00f1a de \u00fatiles Escolares",
                Descripcion = "Recolecci\u00f3n y distribuci\u00f3n de \u00fatiles para ni\u00f1os de escasos recursos",
                FechaInicio = fechaBase.AddDays(5),
                FechaFin = fechaBase.AddDays(20),
                Ubicacion = "Oficinas Centrales Fundaci\u00f3n Esperanza",
                VoluntariosRequeridos = 8,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = fechaBase.AddDays(-20)
            };

            var act3 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Jornada de Reforestaci\u00f3n",
                Descripcion = "Plantaci\u00f3n de \u00e1rboles nativos en el Parque Nacional",
                FechaInicio = fechaBase.AddDays(15),
                FechaFin = fechaBase.AddDays(16),
                Ubicacion = "Parque Nacional Tunari",
                VoluntariosRequeridos = 20,
                Estado = EstadoActividad.Planificada,
                FechaRegistro = fechaBase.AddDays(-10)
            };

            var act4 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Limpieza de R\u00edos",
                Descripcion = "Jornada de limpieza y concientizaci\u00f3n sobre el cuidado del r\u00edo",
                FechaInicio = fechaBase.AddDays(-5),
                FechaFin = fechaBase.AddDays(-4),
                Ubicacion = "R\u00edo Rocha",
                VoluntariosRequeridos = 15,
                Estado = EstadoActividad.Completada,
                FechaRegistro = fechaBase.AddDays(-30)
            };

            var act5 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Taller de Manualidades para Adultos Mayores",
                Descripcion = "Taller recreativo para adultos mayores en el hogar de acogida",
                FechaInicio = fechaBase.AddDays(7),
                FechaFin = fechaBase.AddDays(28),
                Ubicacion = "Hogar San Jos\u00e9",
                VoluntariosRequeridos = 4,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = fechaBase.AddDays(-8)
            };

            contexto.Actividades.AddRange(act1, act2, act3, act4, act5);
            await contexto.SaveChangesAsync();

            // Asignar voluntarios a actividades (M:N)
            act1.VoluntariosAsignados.Add(vol1);
            act1.VoluntariosAsignados.Add(vol2);
            act2.VoluntariosAsignados.Add(vol1);
            act3.VoluntariosAsignados.Add(vol3);
            act4.VoluntariosAsignados.Add(vol3);
            act5.VoluntariosAsignados.Add(vol4);
            act5.VoluntariosAsignados.Add(vol5);
            await contexto.SaveChangesAsync();

            // Horas registradas
            var horas1 = new HorasVoluntariado
            {
                VoluntarioId = vol1.Id,
                ActividadId = act4.Id,
                FechaInicio = act4.FechaInicio,
                FechaFin = act4.FechaFin,
                Horas = 8,
                FechaActividad = act4.FechaInicio,
                FechaRegistro = act4.FechaRegistro,
                Descripcion = "Participaci\u00f3n en jornada de limpieza"
            };

            var horas2 = new HorasVoluntariado
            {
                VoluntarioId = vol2.Id,
                ActividadId = act4.Id,
                FechaInicio = act4.FechaInicio,
                FechaFin = act4.FechaFin,
                Horas = 6,
                FechaActividad = act4.FechaInicio,
                FechaRegistro = act4.FechaRegistro,
                Descripcion = "Apoyo en clasificaci\u00f3n de residuos"
            };

            var horas3 = new HorasVoluntariado
            {
                VoluntarioId = vol3.Id,
                ActividadId = act4.Id,
                FechaInicio = act4.FechaInicio,
                FechaFin = act4.FechaFin,
                Horas = 8,
                FechaActividad = act4.FechaInicio,
                FechaRegistro = act4.FechaRegistro,
                Descripcion = "Coordinaci\u00f3n de grupos de limpieza"
            };

            contexto.HorasVoluntariado.AddRange(horas1, horas2, horas3);
            await contexto.SaveChangesAsync();

            // Certificados
            var cert1 = new Certificado
            {
                VoluntarioId = vol1.Id,
                ActividadId = act4.Id,
                OrganizacionId = org2.Id,
                NumeroSerie = $"CERT-{fechaBase:yyyyMMdd}-A1B2C3D4",
                FechaEmision = fechaBase.AddDays(-1),
                FechaVencimiento = fechaBase.AddYears(2).AddDays(-1),
                HorasTotales = 8,
                TemaEspecifico = "Limpieza y conservaci\u00f3n ambiental",
                FirmanteNombre = "Carlos Mendoza",
                FirmanteCargo = "Director Ejecutivo - EcoVida Bolivia",
                Estado = EstadoCertificado.Generado
            };

            var cert2 = new Certificado
            {
                VoluntarioId = vol3.Id,
                ActividadId = act4.Id,
                OrganizacionId = org2.Id,
                NumeroSerie = $"CERT-{fechaBase:yyyyMMdd}-E5F6G7H8",
                FechaEmision = fechaBase.AddDays(-1),
                FechaVencimiento = fechaBase.AddYears(2).AddDays(-1),
                HorasTotales = 8,
                TemaEspecifico = "Coordinaci\u00f3n de jornada ambiental",
                FirmanteNombre = "Carlos Mendoza",
                FirmanteCargo = "Director Ejecutivo - EcoVida Bolivia",
                Estado = EstadoCertificado.Generado
            };

            contexto.Certificados.AddRange(cert1, cert2);
            await contexto.SaveChangesAsync();
        }
    }
}
